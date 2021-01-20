using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBGoblin : EnemyMgr, IDamagedState
{
    private ObservingMove observe;
    private FollowTarget follow;
    private ReturnMove returnToHome;
    private ViewingAngle viewAngle;

    private Vector3 startPos;
    private Vector3 direction;


    private int findCount;
    private bool isFind;


    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;

        hp = maxHp = 30;
        atk = 35;
        def = 5.0f;
        minGold = 20;
        maxGold = 30;
        findCount = 0;

        itemKind[0] = 79;
        itemKind[1] = 80;
        itemKind[2] = 82;
        for (int i = 0; i < 3; i++)
        {
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }

        AI.enabled = false;
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        weapon.GetComponent<AxColision>().SetDamage(atk);

        observe.Init(AI, startPos, 1.5f, observeRange);
        follow.Init(AI, target, speed, 0);
        returnToHome.init(AI, startPos, speed);

        anim.SetInteger("state", 0);
    }

    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;


        RaycastHit _hit;

        // Debug.DrawRay(transform.position, direction * findRange, Color.blue);

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        isFind = viewAngle.FoundTarget(target, findRange, angle);


        if (isDead)
        {
            Die();
            return;
        }

        if (AI.enabled == false) AI.enabled = true;

        if (observe.getIsObserve())
        {
            if (!isFind)
            {
                Observe();
            }

            else
            {
                if (Physics.Raycast(transform.position, direction, out _hit, findRange))
                {

                    if (_hit.transform.tag.Equals("Player"))
                    {
                        observe.setIsObserve(false);
                        anim.SetInteger("state", 1);
                        //observe 상태일 때 action이 0 이면 움직이지 않는 경우를 대비
                        AI.isStopped = false;
                        findCount = 1;
                    }
                    else
                    {
                        Observe();
                    }
                }
            }
        }
        else
        {
            if (!returnToHome.getIsReturn() && findCount == 1)
            {
                if (isFind)
                {
                    FollowTarget();

                    if (_distance <= attackRange)
                    {
                        //AI.velocity = Vector3.zero;
                        AI.stoppingDistance = attackRange;
                        anim.SetInteger("state", 2);
                    }
                }
                else
                {
                    returnToHome.setIsReturn(true);
                }
            }

            if (returnToHome.getIsReturn())
            {
                Back();
            }

        }

    }


    public void Observe()
    {

        setMoveType(observe);
        Move();

        if (observe.getAction() == 0)
        {
            anim.SetInteger("state", 0);
        }
        if (observe.getAction() == 1)
        {
            anim.SetInteger("state", 1);
        }

    }

    public void FollowTarget()
    {
        //navMesh의 speed를 animation velocity 값에 넣어준다.     

        anim.SetFloat("velocity", AI.speed);
        setMoveType(follow);
        Move();
    }

    public void Back()
    {
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        if (!isHit)
        {
            setMoveType(returnToHome);
            Move();

            if (_homeDistance <= 0.5f)
            {
                findCount = 0;
                returnToHome.setIsReturn(false);
                observe.setIsObserve(true);

                //controller의 speed를 velocity의 값에 넣어준다.
                anim.SetInteger("state", 0);
                anim.SetFloat("velocity", controller.velocity.magnitude);
            }

        }

        else
        {
            transform.rotation = Quaternion.LookRotation(direction);
            returnToHome.setIsReturn(false);

        }

    }
    public void ActiveCollider()
    {
        if (isDead) return;
        weapon.GetComponent<BoxCollider>().enabled = true;
    }
    public void DeActiveCollider()
    {
        if (isDead) return;
        weapon.GetComponent<BoxCollider>().enabled = false;
    }


    public void GetRest()
    {
        if (isDead) return;
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        //rest를 켜고
        anim.SetBool("isRest", true);
        AI.isStopped = true;
        SoundManager.Instance.SFXPlay("Worrier_ATK", weapon.transform.position);
        yield return new WaitForSeconds(1.5f);

        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;

        //transform.LookAt(_direction);

        transform.forward = (_direction);

        yield return new WaitForSeconds(1.8f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        if (!observe.getIsObserve())
            AI.isStopped = false;

    }

    IEnumerator GetCriDamage()
    {
        isDamaged = true;
        isHit = true;
        AI.isStopped = true;
        SoundManager.Instance.SFXPlay("Worrier_Hit", transform.position);
        yield return new WaitForSeconds(.7f);

        AI.isStopped = false;
        isHit = false;
        isDamaged = false;
    }
    IEnumerator GetDamage()
    {

        isHit = true;

        yield return new WaitForSeconds(.7f);

        isHit = false;
    }


    public void Damaged(int value)
    {
        if (isDead) return;

        if (hp > 0)
        {
            if (!isHit)
            {
                hp -= (int)(value * (1.0f - def / 100));

                if (player.isCri) StartCoroutine(GetCriDamage());
                else StartCoroutine(GetDamage());
            }

            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                anim.SetTrigger("Die");
                controller.enabled = false;

                AI.enabled = false;
                weapon.GetComponent<BoxCollider>().enabled = false;
                StopAllCoroutines();

                DungeonMng.Instance.killMelee++;
                SoundManager.Instance.SFXPlay("Worrier_DieVO", transform.position);
            }

        }

        if (player.isCri)
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
            anim.SetTrigger("isDamage");
        }

    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if (disappearTime > 2.5f)
        {
            //아이템 떨어트리기
            var Item = Instantiate(ItemBox, transform.position, Quaternion.identity);
            Item.GetComponent<LootBox>().setItemInfo(item, 5, minGold, maxGold);

            gameObject.SetActive(false);
            disappearTime = 0f;
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(startPos, 5f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}

