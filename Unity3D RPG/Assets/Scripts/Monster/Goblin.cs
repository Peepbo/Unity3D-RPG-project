using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr
{
    private ObservingMove observe;
    private FollowTarget follow;
    private ReturnMove returnToHome;
    private SmashDown smash;

    private Vector3 startPos;

    [Range(3, 7)]
    public float observeRange;
    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        //Goblin Attack skill
        smash = gameObject.AddComponent<SmashDown>();
    }

    void Update()
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);
      
        if (observe.getIsObserve())
        {
            if (_distance < findRange)
            {
                followTarget();
            }
            else
            {
                Observe();
            }

        }
        else
        {
            ReturnToStart();
        }


        //setAttackType(smash);
        //Attack();
    }

    private void setIdleState()
    {
        Debug.Log("Idle 상태");
    }

    public void Observe()
    {
        Vector3 _RNDDirection = GetRandomDirection();
        setMoveType(observe);
        observe.initVariable(controller, startPos, _RNDDirection, speed * 0.5f, observeRange);
        Move();
    }

    public void followTarget()
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

        if (_homeDistance < 0.1f)
        {
            returnToHome.setIsReturn(false);
            observe.setIsObserve(true);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

    }
}
