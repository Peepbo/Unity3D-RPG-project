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
    private Vector3 rushDes;

    private int hp;


    public float dashSpeed;
    private float walkSpeed;

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
        walkSpeed = speed;

        anim.SetInteger("attackType", Random.Range(0, 3));
    }

    void Update()
    {
        distanceBtwTarget = (target.transform.position - transform.position).magnitude;
        directionToTarget = (target.transform.position - transform.position).normalized;

        Vector3 _temp = directionToTarget;
        _temp.y = 0;


        RaycastHit _rayHit;
        NavMeshHit _navHit;

        //Debug.DrawRay()


        //character controller로 변경하기(돌진만)

        if (isStay)
        {
            anim.SetInteger("state", 0);
            Debug.DrawRay(transform.position, _temp * findRange, Color.red);

            if (distanceBtwTarget < findRange)
            {
                if(AI.Raycast(target.transform.position, out _navHit) == false) // 장애물이 없을때!
                {
                    anim.SetBool("IsRush", true);

                    findDirection = (target.transform.position - transform.position).normalized;
                    isStay = false;

                    if (AI.Raycast(findDirection * 20f, out _navHit))
                    {
                        rushDes = findDirection * _navHit.distance;
                    }
                    else rushDes = findDirection * 20f;
                }

                //if (Physics.Raycast(transform.position, _temp, out _rayHit, findRange))
                //{
                //    //집에 있음
                //    if (_rayHit.transform.tag == "Player")
                //    {
                //        anim.SetBool("IsRush",true);
                //        findDirection = (target.transform.position - transform.position).normalized;
                //        isStay = false;
                //    }

                //}
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

                    
                    AI.SetDestination(rushDes);


                    if (view.FoundTarget(target, 1.5f, 35f))
                    {
                        Debug.Log(target.transform.tag);
                        //데미지
                        AI.velocity = Vector3.zero;
                        AI.stoppingDistance = 1;
                        AI.isStopped = true;
                        findCount = 1;

                        anim.SetBool("IsRush", false);
                    }

                    else if (Physics.Raycast(transform.position, transform.forward, out _rayHit, 2f))
                    {
                        if (_rayHit.transform.tag == "Object")
                        {
                            AI.velocity = Vector3.zero;
                            AI.stoppingDistance = 1;
                            AI.isStopped = true;
                            findCount = 1;

                            anim.SetBool("IsRush", false);
                        }
                    }

                }

                else
                {
                    AI.stoppingDistance = 1;
                    Debug.Log("멈춰!");

                    AI.velocity = Vector3.zero;
                    findCount = 1;

                    anim.SetBool("IsRush", false);
                }



            }
            else
            {
                if (isReturn == false)
                {
                    if (distanceBtwTarget < attackRange)
                    {
                        anim.SetInteger("state", 2);
                        AI.isStopped = true;

                        Debug.Log("target을 attack중");
                        //attack
                    }
                    else
                    {
                        anim.SetInteger("state", 1);
                        AI.isStopped = false;
                        //Debug.Log("target으로 가는 중");

                        AI.speed = walkSpeed;
                        AI.SetDestination(target.transform.position);
                    }
                }


                if (!isReturn && distanceBtwTarget > findRange)
                {
                    anim.SetInteger("state", 1);
                    isReturn = true;
                    AI.SetDestination(spawnPos);
                    //return
                    //Debug.Log("집으로 return중");
                }

                if (isReturn)
                {
                    //Debug.LogError(Vector3.Distance(spawnPos, transform.position));

                    if (Vector3.Distance(spawnPos, transform.position) < 1f)
                    {
                        //Debug.Log("집 도착");
                        anim.SetInteger("state", 0);
                        isReturn = false;
                        isStay = true;
                    }
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
    }

    #region animation event functions
    public void GetRandomNum()
    {
        anim.SetInteger("attackType", Random.Range(0, 3));
    }

    IEnumerator AttackRoutine()
    {
        //rest를 켜고
        anim.SetBool("IsRest", true);
        yield return new WaitForSeconds(1.5f);

        //Vector3 _direction = target.transform.position - transform.position;
        //_direction.Normalize();
        //_direction.y = 0;

        ////transform.LookAt(_direction);

        //transform.forward = (_direction);

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetBool("IsRest", false);
    }

    public void GetRest()
    {
        StartCoroutine(AttackRoutine());
    }
    #endregion
}
