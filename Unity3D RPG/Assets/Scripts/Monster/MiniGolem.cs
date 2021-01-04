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

        if (isDead) Die();
        else
        {
            distanceBtwTarget = (target.transform.position - transform.position).magnitude;
            directionToTarget = (target.transform.position - transform.position).normalized;


            if (isStay)
            {
                anim.SetInteger("state", 0);

                if (distanceBtwTarget < findRange)
                {
                    findDirection = (target.transform.position - transform.position).normalized;
                    isStay = false;
                }
            }

            else
            {
                if (findCount == 0)
                {
                    //처음 플레이어를 발견했을 때
                    findDistance = (spawnPos - transform.position).magnitude;

                    if (findDistance < findRange)
                    {
                        Debug.Log("돌격!");
                        Rush();
                    }

                    else
                    {
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

                        }
                        else
                        {
                            FollowTarget();
                        }
                    }


                    if (!isReturn && distanceBtwTarget > findRange)
                    {

                        Back();
                    }

                    if (isReturn)
                    {
                       
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


    }

    public void Rush()
    {
        Vector3 _temp = directionToTarget;
        _temp.y = 0;
        RaycastHit _rayHit;

        Debug.DrawRay(transform.position, _temp * findRange, Color.red);

        findDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(findDirection);
        controller.SimpleMove(findDirection * dashSpeed);

        if (view.FoundTarget(target, 1.5f, 35f))
        {

            findCount = 1;

            anim.SetBool("IsRush", false);
        }

        else if (Physics.Raycast(transform.position, transform.forward, out _rayHit, 2f))
        {
            if (_rayHit.transform.tag == "Object")
            {
                findCount = 1;

                anim.SetBool("IsRush", false);
            }
        }
    }

    public void FollowTarget()
    {
        anim.SetInteger("state", 1);
        AI.isStopped = false;

        AI.speed = walkSpeed;
        AI.SetDestination(target.transform.position);

    }

    public void Back()
    {
        anim.SetInteger("state", 1);
        isReturn = true;
        AI.SetDestination(spawnPos);
    }

    public void Damaged(int value)
    {
        if (isDamaged || isDead) return;
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

        yield return new WaitForSeconds(1f);

        //rest를 끈다
        anim.SetBool("IsRest", false);
        AI.updateRotation = true;
    }

    public void GetRest()
    {
        StartCoroutine(AttackRoutine());
    }


    public void ChageRotation()
    {
        AI.updateRotation = false;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    IEnumerator RotateTransform()
    {

        yield return new WaitForSeconds(2f);
    }
    #endregion
}
