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

    bool isStay;
    bool isReturn;

    #region
    int hp;
    int atk;
    float def;
    int gold;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();
        spawnPos = transform.position;
    }
    private void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector3.Distance(target.transform.position, transform.position);


        if (isStay)
        {
            if (distance < findRange)
            {
                isStay = false;
            }
        }

        else
        {
            if (!isReturn)
            {
                if (distance < findRange)
                {
                    AI.stoppingDistance = attackRange;
                    AI.SetDestination(target.transform.position);
                    
                }
                else if (distance < attackRange)
                {
                    AI.velocity = Vector3.zero;
                 
                }
                else if (distance > findRange) isReturn = true;
                
            }

            if (isReturn)
            {
                AI.stoppingDistance = 0;
                AI.SetDestination(spawnPos);
                

                if (Vector3.Distance(spawnPos, transform.position) < 1f)
                {
                    isReturn = false;
                    isStay = true;
                    AI.velocity = Vector3.zero;
                }
            }
        }

    }

    public void Damaged(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
        }

    }

    public override void Die()
    {

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
}
