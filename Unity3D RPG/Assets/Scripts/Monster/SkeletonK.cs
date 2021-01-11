using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonK : EnemyMgr, IDamagedState
{

    private FollowTarget follow;
    private ReturnMove back;
    private Vector3 direction;
    private float distance;

    private Vector3 startPos;

    public GameObject weapon;

    private bool isStay = true;
    private int findCount;
    private RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();

        startPos = transform.position;

        hp = maxHp = 175;
        atk = 80;
        def = 20.0f;
        minGold = 100;
        maxGold = 200;
        for (int i = 0; i < 3; i++)
        {
            itemKind[i] = 79;
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }
        AI.enabled = true;
    }

    private void Start()
    {
        //Goblin Move pattern
        //observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();

        weapon.GetComponent<WepCol>().setAtk(atk);

        follow.Init(AI, target, speed, 0);
        back.init(AI, startPos, speed);

        anim.SetInteger("state", 0);
        anim.SetInteger("atkType", 0);
    }

    private void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        distance = Vector3.Distance(target.transform.position, transform.position);


        if (isDead)
        {
            Die();
            return;
        }

        if (AI.enabled == false) AI.enabled = true;


        if (isStay)
        {
            if (distance < findRange)
            {
                if (Physics.Raycast(transform.position, direction, out hit, findRange))
                {
                    if (hit.transform.tag == "Player")
                    {
                        isStay = false;
                        AI.isStopped = false;
                        findCount = 1;
                        anim.SetInteger("state", 1);
                    }

                    else
                    {
                        AI.isStopped = true;
                        anim.SetInteger("state", 0);
                    }
                }
            }
        }

        else

        {

            if (findCount == 1)
            {
                if (distance < findRange)
                {
                    back.setIsReturn(false);
                    if (anim.GetInteger("state") != 2)
                    {
                        AI.isStopped = false;
                        Follow();
                    }
                }
                else
                {
                    anim.SetInteger("state", 1);
                    back.setIsReturn(true);
                    Back();
                }
                if (distance <= (attackRange))
                {
                    AI.isStopped = true;
                    anim.SetInteger("state", 2);
                    anim.SetInteger("atkType", 1);

                    AI.velocity = Vector3.zero;
                }

            }

        }


    }


    private void Follow()
    {
        anim.SetInteger("state", 1);
        setMoveType(follow);
        Move();
    }

    private void Back()
    {

        float _home = Vector3.Distance(startPos, transform.position);
        setMoveType(back);
        Move();

        if (_home < 0.5f)
        {
            isStay = true;
            findCount = 0;
            back.setIsReturn(false);
            anim.SetInteger("state", 0);
            AI.isStopped = true;
        }
    }


    IEnumerator AttackRoutine()
    {
        //rest를 켜고
        anim.SetBool("isRest", true);
        AI.isStopped = true;
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(LookBack());

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        if (!isStay)
            AI.isStopped = false;

    }


    public void Damaged(int value)
    {
        if (isDead) return;

        if (hp > 0)
        {
            if (!isDamaged)
            {
                anim.SetTrigger("isDamage");
                hp -= (int)(value * (1.0f - def / 100));

                isDamaged = true;
            }

            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                anim.SetTrigger("Die");
                StopAllCoroutines();
                AI.enabled = true;
            }
        }
    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;
        if (disappearTime > 3f)
        {
            var _item = Instantiate(ItemBox, transform.position, Quaternion.identity);
            _item.GetComponent<LootBox>().setItemInfo(item, 5, minGold, maxGold);

            disappearTime = 0f;
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, findRange);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(startPos, 1f);
    }

    #region

    public void RandomAttack()
    {
        int _type = Random.Range(0, 2);
        anim.SetInteger("atkType", _type);
    }

    public void GetRest()
    {
        StartCoroutine(AttackRoutine());
    }

    public void Active()
    {
        weapon.GetComponent<MeshCollider>().enabled = true;
    }

    public void DeActive()
    {
        weapon.GetComponent<MeshCollider>().enabled = false;
    }
    #endregion
}
