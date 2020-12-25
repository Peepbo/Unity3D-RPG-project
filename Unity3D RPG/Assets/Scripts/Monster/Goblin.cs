using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr
{
    private SmashDown smash;
    private FollowTarget follow;
    private ReturnMove returnToHome;

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
        follow = gameObject.AddComponent<FollowTarget>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        //Goblin Attack skill
        smash = gameObject.AddComponent<SmashDown>();
    }

    void Update()
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        if (!returnToHome.getIsReturn())
        {
            if (_distance < findRange)
            {
                followTarget();

            }
            else
            {
                //observe move and idle   
            }
        }

        else
        {
            if (_homeDistance < 1.0f)
            {
                returnToHome.setIsReturn(false);
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

    }

    public void followTarget()
    {
        setMoveType(follow);
        follow.setVariable(controller, target, speed);
        Move();

    }

    public void ReturnToStart()
    {
        setMoveType(returnToHome);
        returnToHome.setIsReturn(true);
        returnToHome.initVariable(controller, startPos, speed);
        Move();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);



        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

    }
}
