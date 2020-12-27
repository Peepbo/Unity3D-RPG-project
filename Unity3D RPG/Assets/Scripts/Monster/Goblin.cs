﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr, IDamagedState
{
    private ObservingMove observe;
    private FollowTarget follow;
    private ReturnMove returnToHome;

    private SmashDown smash;
    private ViewingAngle viewAngle;
    private Vector3 startPos;

    [Range(3, 7)]
    public float observeRange;
    [Range(0, 180)]
    public float angle;


    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        viewAngle = GetComponent<ViewingAngle>();
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        //Goblin Attack skill
        smash = gameObject.AddComponent<SmashDown>();

        anim.SetInteger("state", 0);
    }

    void Update()
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);

        bool _isFind = viewAngle.FoundTarget(target, findRange, angle);


        if (observe.getAction() == 0)
        {
            anim.SetInteger("state", 0);
        }
        else if (observe.getAction() == 1)
        {
            anim.SetInteger("state", 1);
        }

        if (observe.getIsObserve())
        {
            if (_isFind)
            {
                observe.setIsObserve(false);
                
            }

            else
            {
                Observe();
            }

        }
        else

        {

            if (_isFind && !returnToHome.getIsReturn())
            {

                if (_distance < attackRange)
                {
                    anim.SetInteger("state", 2);
                    AttackTarget();
                }
                else
                {
                    anim.SetInteger("state", 1);
                    FollowTarget();
                }

            }
            else
            {
                ReturnToStart();
            }
        }

        //setAttackType(smash);
        //Attack();
    }

    private void setIdleState()
    {
        bool _isFind = viewAngle.FoundTarget(target, findRange, angle);

        if (observe.getIsRangeOver())
        {
            anim.SetInteger("state", 0);
        }

        
        if (_isFind)
        {
            anim.SetInteger("state", 1);
        }
      
        //Debug.Log("Idle 상태");
    }

    public void Observe()
    {
        Vector3 _RNDDirection = GetRandomDirection();
        setMoveType(observe);
        observe.initVariable(controller, startPos, _RNDDirection, speed * 0.5f, observeRange);
        Move();

        if (observe.getAction() == 0) setIdleState();


    }

    public void FollowTarget()
    {
        //타겟 따라갈때는 observe false
        observe.setIsObserve(false);

        setMoveType(follow);
        follow.initVariable(controller, target, speed);
        Move();
    }

    public void ReturnToStart()
    {
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        setMoveType(returnToHome);
        returnToHome.setIsReturn(true);
        returnToHome.initVariable(controller, startPos, speed);
        Move();

        if (_homeDistance <= 0.1f)
        {
            returnToHome.setIsReturn(false);
            observe.setIsObserve(true);
        }
    }

    public void AttackTarget()
    {
        //print("goblin tries attack");

        setAttackType(smash);
        smash.attack();

    }

    public void Damaged()
    {
        //print("goblin Damaged");

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
