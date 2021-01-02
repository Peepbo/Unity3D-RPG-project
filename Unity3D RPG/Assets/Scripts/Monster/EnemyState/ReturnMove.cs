using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnMove : MonoBehaviour, IMoveAble
{
    NavMeshAgent nav;
    CharacterController controller;
    Vector3 spawnPos;
    float speed;
    bool isReturn;
    public void initVariable(CharacterController cc, Vector3 spawnPosition, float followSpeed)
    {
        this.controller = cc;
        this.spawnPos = spawnPosition;
        this.speed = followSpeed;
    }
    public void initVariable(NavMeshAgent ai, Vector3 spawnPosition, float followSpeed)
    {
        this.nav = ai;
        this.spawnPos = spawnPosition;
        this.speed = followSpeed;
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

        Vector3 _return = spawnPos - transform.position;
        Vector3 _direction = _return.normalized;

        _direction.y = 0;

        if (transform.position != spawnPos)
            transform.rotation = Quaternion.LookRotation(_direction);

        controller.Move(_direction * speed * Time.deltaTime);
        //nav.speed = speed;
        //nav.SetDestination(spawnPos);

    }
}
