using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour, IMoveAble
{
    GameObject target;
    NavMeshAgent nav;
    float speed;
    float stopDis;

    public void setSpeed(float newSpeed) { speed = newSpeed; }

    public void Init(NavMeshAgent ai, GameObject destination, float walkSpeed, float stopRange)
    {
        this.nav = ai;
        this.target = destination;
        this.speed = walkSpeed;
        this.stopDis = stopRange;
    }

    public void move()
    {
        if (target != null)
        {
            nav.speed = speed;
            nav.stoppingDistance = stopDis;
            nav.SetDestination(target.transform.position);
        }

    }
}
