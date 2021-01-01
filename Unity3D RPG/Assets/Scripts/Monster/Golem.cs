using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyMgr, IDamagedState
{
    public float dashSpeed;
    private int findCount;
    private bool isStay = true;
    
    private Vector3 spawnPos;
    private FollowTarget follow;
    private ReturnMove returnTohome;


    protected override void Awake()
    {
        base.Awake();
        spawnPos = transform.position;
    }
    private void Start()
    {
        follow = gameObject.AddComponent<FollowTarget>();
        returnTohome = gameObject.AddComponent<ReturnMove>();

        follow.initVariable(controller, target, speed);
        returnTohome.initVariable(controller, spawnPos, speed);

        returnTohome.setIsReturn(false);

    }
    private void Update()
    {
        Vector3 _between = target.transform.position - transform.position;
        // Vector3 _direction = _between.normalized;
        float _distance = _between.magnitude;

        if(isStay)
        {
            //집에 있다가 타겟을 찾으면
            if(_distance <findRange)
            {
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
            //집에 없을 때 타겟을 찾으면
            if (_distance<findRange && !returnTohome.getIsReturn())
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
    public void Idle()
    {
        //집 앞에서 가만히 있는다.
        Debug.Log("집이닿");


    }
    public void FollowTarget()
    {
        setMoveType(follow);
        follow.move();

    }

    public void ReturnHome()
    {
        float _homeDistance = (spawnPos - transform.position).magnitude;
        setMoveType(returnTohome);
        returnTohome.move();

        if(_homeDistance<0.5f)
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
        
    }
    public override void Die()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);
    }
}
