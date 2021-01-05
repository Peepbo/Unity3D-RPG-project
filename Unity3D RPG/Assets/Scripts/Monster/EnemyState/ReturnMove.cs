using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnMove : MonoBehaviour, IMoveAble
{
    NavMeshAgent nav;
    //CharacterController controller;
    Vector3 spawnPos;
    float speed;
    bool isReturn;

    public void init(NavMeshAgent ai, Vector3 spawn, float walkSpeed)
    {
        this.nav = ai;
        this.spawnPos = spawn;
        this.speed = walkSpeed;

    }

    public bool getIsReturn()
    {
        return isReturn;
    }
    public void setIsReturn(bool isReturning)
    {
        isReturn = isReturning;
    }

    public void move()
    {
        if (!isReturn) return;

        nav.speed = speed;
        nav.stoppingDistance = 0f;
        nav.SetDestination(spawnPos);
       
    }
}
