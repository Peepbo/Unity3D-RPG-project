using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyMgr, IDamagedState
{

    FollowTarget follow;
    ReturnMove back;

    Vector3 direction;
    Vector3 spawnPos;
    float distance;
    bool isStay = true;

    int findCount;
    float destroyCount;

    #region
    int hp;
    int atk;
    float def;
    int gold;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        spawnPos = transform.position;

    }
    private void Start()
    {
        findCount = 0;
        hp = maxHp;
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
                        anim.SetInteger("state", 2);


                        Debug.Log("플레이어를 공격중");
                        //AttackTarget();
                    }
                    //if (Physics.Raycast(transform.position, direction, out _hit, findRange))
                    //{
                    //    if (_hit.transform.tag == "Player")
                    //    {
                    //        Debug.Log("Player");
                    //        anim.SetInteger("state", 1);
                    //        Follow();

                    //    }

                    //    else Debug.Log("못찾겠다..");

                    //    //1. 못찾을때 집에간다

                    //    //2. 여기에 raycast를 안쓴다
                    //}


                    //공격이 끝나고 휴식이 끝나면 AI.isStopped = false;

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
        //AI.isStopped = false;
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
        anim.SetTrigger("Damaged");

        AI.isStopped = true;
        isDamaged = true;

        yield return new WaitForSeconds(1f);

        AI.isStopped = false;
        isDamaged = false;
    }

    public void Damaged(int value)
    {
        if (isDamaged || isDead ) return;

        hp -= value;

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

        if (destroyCount > 7.0f)
        {
            Debug.Log("gone");
            gameObject.SetActive(false);
        }

        StopAllCoroutines();
        AI.enabled = false;
        controller.enabled = false;

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


    #endregion


}
