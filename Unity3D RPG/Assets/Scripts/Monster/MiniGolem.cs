using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniGolem : EnemyMgr, IDamagedState
{
    FollowTarget follow;
    ReturnMove back;
    ViewingAngle view;

    private Vector3 spawnPos;
    private Vector3 directionToTarget;
    private Vector3 findDirection;
    private float distanceBtwTarget;
    private float findDistance;
    private int findCount;


    public float dashSpeed;
    public float walkSpeed;

    private bool isStay;
    private bool isReturn;

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
        view = gameObject.AddComponent<ViewingAngle>();
        spawnPos = transform.position;
    }

    void Update()
    {
        distanceBtwTarget = (target.transform.position - transform.position).magnitude;
        directionToTarget = (target.transform.position - transform.position).normalized;

        Vector3 _temp = directionToTarget;
        _temp.y = 0;


        RaycastHit _rayHit;

        //Debug.DrawRay()

        if (isStay)
        {
            Debug.DrawRay(transform.position, _temp * findRange, Color.red);

            if (distanceBtwTarget < findRange)
            {

                //Vector3 _myPos = transform.position;
                //_myPos.y = 0;
              
                if (Physics.Raycast(transform.position, _temp, out _rayHit, findRange))
                {
                    //집에 있음
                    if (_rayHit.transform.tag == "Player")
                    {
                        findDirection = target.transform.position;
                        isStay = false;
                    }

                    //if (distanceBtwTarget < findRange)
                    //{

                    //    isStay = false;

                    //}
                }
            }
        }

        else
        {
            if (findCount == 0)
            {
                //처음 플레이어를 발견했을 때
                findDistance = (spawnPos - transform.position).magnitude;



                if (findDistance < 20f)
                {
                    Debug.Log("돌격!");
                    AI.speed = dashSpeed;

                    AI.stoppingDistance = 0;
                    AI.SetDestination(findDirection.normalized * 20f);

                    //Debug.DrawRay(transform.position, findDirection.normalized, Color.red);


                    if (view.FoundTarget(target, 1.5f, 35f))
                    {
                        Debug.Log(target.transform.tag);
                        //데미지
                        AI.isStopped = true;
                        AI.velocity = Vector3.zero;
                        AI.stoppingDistance = 1;
                        findCount = 1;
                    }

                  
                }

                else
                {
                    AI.stoppingDistance = 1;
                    Debug.Log("멈춰!");

                    AI.velocity = Vector3.zero;
                    findCount = 1;
                }



            }
            else
            {
                if (isReturn == false)
                {
                    if (distanceBtwTarget < attackRange)
                    {
                        AI.isStopped = true;

                        Debug.Log("target을 attack중");
                        //attack
                    }
                    else
                    {
                        AI.isStopped = false;
                        Debug.Log("target으로 가는 중");

                        AI.speed = walkSpeed;
                        AI.SetDestination(target.transform.position);
                    }
                }


                if (!isReturn && distanceBtwTarget > findRange)
                {
                    isReturn = true;
                    AI.SetDestination(spawnPos);
                    //return
                    Debug.Log("집으로 return중");
                }

                if (isReturn)
                {
                 //   Debug.LogError(Vector3.Distance(spawnPos, transform.position));

                    if (Vector3.Distance(spawnPos, transform.position) < 0.5f)
                    {
                        Debug.Log("집 도착");

                        isReturn = false;
                        isStay = true;
                    }
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
