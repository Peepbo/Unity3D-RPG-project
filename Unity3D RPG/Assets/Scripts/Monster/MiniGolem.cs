using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniGolem : EnemyMgr, IDamagedState
{
    FollowTarget follow;
    ReturnMove back;

    private Vector3 spawnPos;
    private Vector3 directionToTarget;
    private float distanceBtwTarget;
    private float findDistance;
    private int findCount;


    public float dashSpeed;
    public float walkSpeed;

    private bool isStay;

    protected override void Awake()
    {
        base.Awake();
        findCount = 0;
        isStay = true;
    }
    void Start()
    {
        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();
        spawnPos = transform.position;
    }

    void Update()
    {
        distanceBtwTarget = (target.transform.position - transform.position).magnitude;
        directionToTarget = (target.transform.position - transform.position).normalized;


        NavMeshHit _hit;
        //Debug.DrawRay()

        if (isStay)
        {
            //집에 있음
            if (distanceBtwTarget < findRange)
            {
                isStay = false;
            }
        }
        else
        {
            if (findCount == 0)
            {
                //처음 플레이어를 발견했을 때

                if (!AI.Raycast(target.transform.position, out _hit))
                {

                    findDistance = (spawnPos - transform.position).magnitude;
                  
                    
                    if (findDistance < 20f)
                    {
                        AI.Move(directionToTarget * dashSpeed * Time.deltaTime);

                    }
                    else
                    {
                        findCount = 1;
                    }

                }
                else
                {
                    findCount = 1;
                }    
            }
            else
            {
                if (distanceBtwTarget > attackRange)
                {
                    AI.Move(directionToTarget * walkSpeed * Time.deltaTime);
                }
                else
                {
                    Debug.Log("공격");
                }
            }

        }

    }

    public void Damaged(int value)
    {

    }
    public override void Die()
    {

    }
}
