using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonW : EnemyMgr, IDamagedState
{
    private FollowTarget follow;
    private ReturnMove back;

    private Vector3 startPos;
    private Vector3 direction;
    private float distance;

    private bool isStay = true;
    private int findCount = 0;
    private RaycastHit hit;


    public GameObject weapon;
    protected override void Awake()
    {
        base.Awake();
        hp = maxHp;
        atk = 55;
        def = 10.0f;
        minGold = 40;
        maxGold = 60;

        itemKind[0] = 79;
        itemKind[1] = 86;
        itemKind[2] = 88;

        for (int i = 0; i < 3; i++)
        {
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }

    }

    private void Start()
    {
        startPos = transform.position;

        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();

        follow.Init(AI, target, speed, 0);
        back.init(AI, startPos, speed);
        weapon.GetComponent<WepCol>().setAtk(atk);
      
    }

    private void Update()
    {
        if (isDead)
        {
            Die();
            return;
        }

        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        distance = Vector3.Distance(target.transform.position, transform.position);

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
            if (!back.getIsReturn())
            {
                if (findCount == 1)
                {

                    if (distance < findRange)
                    {
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
                    }
                    if (distance <= (attackRange))
                    {
                        AI.isStopped = true;
                        anim.SetInteger("state", 2);
                       
                        AI.velocity = Vector3.zero;
                    }



                }

            }
            else
            {
                Back();
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

        transform.forward = direction;

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
                hp -= value;

                isDamaged = true;
            }
        }
        else
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");
            StopAllCoroutines();
            AI.enabled = true;

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPos, 1f);
    }

    #region EVENT FUNCTIONS
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
