using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Golem : EnemyMgr, IDamagedState
{
    private int hp;
    private int findCount;
    private float walkSpeed;
    public float dashSpeed;
    private bool isStay = true;


    private Vector3 spawnPos;
    private Vector3 findTargetDir;
    private FollowTarget follow;
    private ReturnMove returnTohome;



    protected override void Awake()
    {
        base.Awake();
        spawnPos = transform.position;
    }
    private void Start()
    {
        hp = maxHp;
        findCount = 0;
        walkSpeed = speed;
        follow = gameObject.AddComponent<FollowTarget>();
        returnTohome = gameObject.AddComponent<ReturnMove>();

        follow.initVariable(controller, target, walkSpeed);
        returnTohome.initVariable(controller, spawnPos, walkSpeed);

        returnTohome.setIsReturn(false);

    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);
        Vector3 _between = target.transform.position - transform.position;
        // Vector3 _direction = _between.normalized;
        float _distance = _between.magnitude;

        if (isStay)
        {
            //집에 있다가 타겟을 찾으면
            if (_distance < findRange)
            {
                Debug.Log("찾았다!");

                findTargetDir = (target.transform.position - spawnPos).normalized;
                isStay = false;
            }
            //집에 있다가 타겟을 못찾으면
            else
            {
                Idle();
            }
        }
        else
        {


            if (findCount == 0)
            {
                //Debug.Log("거리 : " + _distance);
                //Debug.Log("공격 범위 : " + attackRange);

                //if (_distance < findRange)
                //{

                //}
                rushAttack();

                //플레이어에 닿으면 setfindCount1 , damage넣기 

                //if (_distance < 1.4f)
                //{
                //    Debug.Log(_distance + ", " + attackRange);
                //    findCount = 1;
                //}

            }
            else
            {

                //집에 없을 때 타겟을 찾으면
                if (_distance < findRange && !returnTohome.getIsReturn())
                {

                    //공격 범위에서 공격
                    if (_distance < attackRange)
                    {
                        AttackTarget();
                    }
                    //공격 범위 전까지 따라감
                    else
                    {
                        FollowTarget();
                    }

                }
                //집에 없을 때 타겟을 못찾으면
                else
                {
                    //집으로 가는 중
                    returnTohome.setIsReturn(true);
                    ReturnHome();

                }

            }


        }

    }

    public void rushAttack()
    {
        float _distance = (transform.position - spawnPos).magnitude;

        if (_distance < 20f)
        {
            transform.rotation = Quaternion.LookRotation(findTargetDir);
            controller.Move(findTargetDir * dashSpeed * Time.deltaTime);

            //만약에 벽에 닿으면?
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, transform.forward, out _hit, 1.5f))
            {
                if(_hit.transform.tag == "Player")
                {
                    findCount = 1;

                    _hit.transform.GetComponent<Player>().GetDamage(atkPower);
                }

                else if (_hit.transform.tag == "Object")
                {
                    findCount = 1;
                }
            }
        }
        else
        {
            findCount = 1;
        }

    }

    public void Idle()
    {
        //집 앞에서 가만히 있는다.
        Debug.Log("집이닿");


    }

    public void FollowTarget()
    {
        setMoveType(follow);
        follow.setSpeed(walkSpeed);
        follow.move();

    }

    public void ReturnHome()
    {
        float _homeDistance = (spawnPos - transform.position).magnitude;
        setMoveType(returnTohome);
        returnTohome.move();

        if (_homeDistance < 0.5f)
        {
            returnTohome.setIsReturn(false);
        }

    }

    public void AttackTarget()
    {
        Debug.Log("공격");
    }

    public void Damaged(int value)
    {
        if (isDead) return;

        hp -= value;

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
        }
    }
    public override void Die()
    {
        //DissolveEft eft = GetComponent<DissolveEft>();
        //eft.SetValue(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);

    }
}
