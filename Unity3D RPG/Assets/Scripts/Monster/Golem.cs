using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyMgr, IDamagedState
{
    
    private FollowTarget follow;
    private ReturnMove back;

    private Vector3 direction;
    private Vector3 spawnPos;

    public GameObject damageCheck;

    private float distance;
    private bool isStay = true;

    private int findCount;
    private float destroyCount;



    protected override void Awake()
    {
        base.Awake();

        findCount = 0;
        hp = maxHp;
        atk = 45;
        def = 10.0f;
        minGold = 50;
        maxGold = 100;
        spawnPos = transform.position;

       
    }
    private void Start()
    {
        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();

        follow.Init(AI, target, speed, attackRange);
        back.init(AI, spawnPos, speed);

        anim.SetInteger("state", 0);
    }

    private void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector3.Distance(target.transform.position, transform.position);

        RaycastHit _hit;
        direction.y = 0;
        Debug.DrawRay(transform.position, direction * findRange, Color.red);

        if (isDead)
        {
            Die();
            return;
        }

        if (isStay)
        {
            anim.SetInteger("state", 0);
            if (distance < findRange)
            {
                if (Physics.Raycast(transform.position, direction, out _hit, findRange))
                {
                    if (_hit.transform.tag == "Player")
                    {
                        AI.isStopped = false;
                        findCount = 1;
                        isStay = false;
                    }
                    else
                    {
                        anim.SetInteger("state", 0);
                        AI.isStopped = true;
                    }

                }
            }
        }

        else
        {
            if (!back.getIsReturn() && findCount == 1)
            {
                if (distance < findRange)
                {


                    if (AI.isStopped == false)
                    {
                        anim.SetInteger("state", 1);
                        Follow();
                    }

                    if (distance <= attackRange)
                    {
                        //공격범위
                        
                        anim.SetInteger("state", 2);

                        if(damageCheck.gameObject.GetComponent<DamageCheck>().GetCount() ==1)
                        {
                            target.gameObject.GetComponent<Player>().GetDamage(atk);

                            damageCheck.gameObject.GetComponent<DamageCheck>().ReadyToDamage(0);
                        }

                    }
                }


                else if (distance > findRange)
                {
                    anim.SetInteger("state", 1);
                    back.setIsReturn(true);
                }

            }

            if (back.getIsReturn())
            {
                Back();

                if (Vector3.Distance(spawnPos, transform.position) < 0.5f)
                {
                    anim.SetInteger("state", 0);
                    back.setIsReturn(false);
                    isStay = true;
                    AI.isStopped = true;
                    findCount = 0;
                }
            }
        }

    }

    public void Follow()
    {
        AI.isStopped = false;
        setMoveType(follow);
        follow.move();
    }

    public void Back()
    {
        if (anim.GetBool("IsRest") == false)
        {
            setMoveType(back);
            back.move();

        }
    }

    IEnumerator AttackCycle()
    {
        AI.isStopped = true;
        AI.updateRotation = false;
        yield return new WaitForSeconds(1.3f);

        transform.rotation = Quaternion.LookRotation(direction);
        anim.SetBool("IsRest", true);
        anim.SetInteger("state", 0);

        yield return new WaitForSeconds(1.5f);
        anim.SetBool("IsRest", false);
        AI.isStopped = false;
        AI.updateRotation = true;
    }

    public void AttackTarget()
    {
        Debug.Log("startCoru");
        StartCoroutine(AttackCycle());
    }

    IEnumerator GetDamage()
    {
        if (target.gameObject.GetComponent<Player>().isCri)
        {
            anim.SetTrigger("Damaged");
        }
        AI.isStopped = true;
        isDamaged = true;

        yield return new WaitForSeconds(1f);

        AI.isStopped = false;
        isDamaged = false;
    }
   
    public void Damaged(int value)
    {
        if (isDamaged || isDead) return;

        hp -= (int)(value * (1.0f - def / 100));

        if (hp > 0) StartCoroutine(GetDamage());

        else
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");

        }

    }

    public override void Die()
    {
        destroyCount += Time.deltaTime;

        if (destroyCount > 3.0f)
        {
            gameObject.GetComponent<DissolveEft>().SetValue(0);
        }

        if (destroyCount > 6.0f)
        { 
            Debug.Log("gone");
            gameObject.SetActive(false);
            destroyCount = 0f;
        }

        StopAllCoroutines();
        AI.enabled = false;
        controller.enabled = false;

    }

    public override void DropCoin(int min, int max)
    {
        currency = Random.Range(min, max + 1);
        Instantiate(coinEffect, transform.position, Quaternion.identity);

        LootManager.Instance.GetPocketMoney(currency);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPos, 1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


    #region Animation event functions
    public void HandActive()
    {
        damageCheck.GetComponent<DamageCheck>().ReadyToDamage(0);
    }
    #endregion


}
