using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObservingMove : MonoBehaviour, IMoveAble
{
    private NavMeshAgent agent;

    private Vector3 ranDestination;         //범위 내 랜덤 목적지
    private Vector3 spawnPos;
    private float radius;                   //spawn point에서부터의 범위
    private float time = 5f;                  //action을 바꿔 줄 time
    private int action;                     //0 :Idle ,1 :Move

    private bool isObserve = true;

    public void Init(NavMeshAgent ai, Vector3 spawn, float followSpeed, float range)
    {
        spawnPos = spawn;
        agent = ai;
        agent.speed = followSpeed;
        radius = range;
    }
    public int getAction() { return action; }
    public bool getIsObserve() { return isObserve; }

    public void setIsObserve(bool isObserving) { isObserve = isObserving; }


    public void move()
    {
        if (!isObserve) return;


        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        else
        {
            time = Random.Range(4, 7);
            action = Random.Range(0, 2);
        }

        switch (action)
        {
            case 0:
                agent.isStopped = true;
                if (agent.hasPath == true)
                {
                    agent.ResetPath();
                }

                break;

            case 1:
                if (agent.hasPath == false)
                {
                    ranDestination = Random.onUnitSphere * radius;
                    ranDestination.y = 0;
                    time = 3f;
                    agent.isStopped = false;
                    agent.SetDestination(spawnPos + ranDestination);
                }

                else
                {
                    //Vector3 _dir = (ranDestination - transform.position).normalized;
                    //_dir.y = 0;
                    //RaycastHit _hit;

                    //if (Vector3.Distance(ranDestination, transform.position) < 1.5f)
                    //{
                    //    if (Physics.Raycast(transform.position, _dir, out _hit, 1.5f))
                    //    {
                    //        if (_hit.transform.tag == "Object")
                    //        {
                    //            agent.isStopped = true;
                              

                    //        }

                    //    }
                    //}

                }
                break;
        }
    }
}