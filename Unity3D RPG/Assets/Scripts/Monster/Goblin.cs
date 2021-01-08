using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr, IDamagedState
{
    private FollowTarget follow;
    private ReturnMove returnToHome;
    private ViewingAngle viewAngle;



    private Vector3 startPos;
    private Vector3 direction;


    private int findCount;
    private bool isFind;

    bool isObserve = true;

    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        hp = maxHp;
        atk = 25;
        def = 5.0f;
        minGold = 20;
        maxGold = 30;
        findCount = 0;

    }
    void Start()
    {
        //Goblin Move pattern

        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        weapon.GetComponent<AxColision>().SetDamage(atk);

        follow.Init(AI, target, speed, attackRange);
        returnToHome.init(AI, startPos, speed);


        anim.SetInteger("state", 0);
    }

    void Update()
    {


        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;


        RaycastHit _hit;

        Debug.DrawRay(transform.position, direction * findRange, Color.blue);

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        isFind = viewAngle.FoundTarget(target, findRange, angle);


        if (isDead)
        {
            Die();
            return;
        }

        if (isObserve)
        {
            if (!isFind)
            {
                ProtectHome();

            }

            else
            {
                if (Physics.Raycast(transform.position, direction, out _hit, findRange))
                {

                    if (_hit.transform.tag == "Player")
                    {
                        anim.SetInteger("state", 1);
                        findCount = 1;
                        isObserve = false;
                        AI.isStopped = false;
                    }
                    else
                    {
                        AI.isStopped = true;
                    }
                }
            }
        }
        else
        {
            if (!returnToHome.getIsReturn() /*&& findCount == 1*/)
            {
                if (findCount == 1)
                {
                    if (isFind)
                    {
                        Debug.Log("find!");
                        FollowTarget();
                    }
                    else
                    {
                        if (_distance >= attackRange)
                        {
                            Debug.Log("atk 범위 벗어남");
                            returnToHome.setIsReturn(true);
                        }
                    }


                    if (_distance <= attackRange)
                    {
                        Debug.Log("atk");
                        anim.SetInteger("state", 2);
                    }
                }
                else
                {
                    Debug.Log("findCount ==0");
                    //returnToHome.setIsReturn(true);
                }


            }

            if (returnToHome.getIsReturn())
            {

                Debug.Log("return");
                Back();
            }

        }

    }

    public void ProtectHome()
    {
        if (isHit)
        {
            StartCoroutine(LookBack());

        }
        else
        {
            anim.SetInteger("state", 0);

        }
    }

    public void FollowTarget()
    {
        //controller의 speed를 animation velocity 값에 넣어준다.
        //anim.SetFloat("velocity", controller.velocity.magnitude);
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

            if (_homeDistance <= 0.3f)
            {
                Debug.Log("home");
                returnToHome.setIsReturn(false);
                AI.isStopped = true;

                isObserve = true;
                findCount = 0;

                //controller의 speed를 velocity의 값에 넣어준다.;
                anim.SetInteger("state", 0);
                anim.SetFloat("velocity", controller.velocity.magnitude);
            }

        }

        else
        {
            Debug.Log("look");
            transform.rotation = Quaternion.LookRotation(direction);
            returnToHome.setIsReturn(false);
        }

    }
    public void ActiveMeshCol()
    {
        if (isDead) return;
        weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void DeActiveMeshCol()
    {
        if (isDead) return;
        weapon.GetComponent<MeshCollider>().enabled = false;
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
        yield return new WaitForSeconds(1.5f);

        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;

        //transform.LookAt(_direction);

        transform.forward = (_direction);

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        if (!isObserve)
        AI.isStopped = false;

    }

    IEnumerator GetCriDamage()
    {
        isDamaged = true;
        isHit = true;
        AI.isStopped = true;

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

        if (player.isCri)
        {
            weapon.GetComponent<MeshCollider>().enabled = false;
        }

        if (isHit || isDead) return;

        if (hp > 0)
        {
            hp -= (int)(value * (1.0f - def / 100));

            if (player.isCri) StartCoroutine(GetCriDamage());
            else StartCoroutine(GetDamage());
        }
        else
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");
            controller.enabled = false;

            AI.enabled = false;

            StopAllCoroutines();

        }

        if (player.isCri)
            anim.SetTrigger("isDamage");

    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if (disappearTime > 3.5f)
        {
            //아이템 떨어트리기
            DropCoin(minGold, maxGold);
            gameObject.SetActive(false);
            disappearTime = 0f;
        }
    }

    public override void DropCoin(int min, int max)
    {
        currency = Random.Range(min, max + 1);
        Instantiate(coinEffect, transform.position, Quaternion.identity);

        LootManager.Instance.GetPocketMoney(currency);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }



}
