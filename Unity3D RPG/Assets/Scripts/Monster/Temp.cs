using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Temp : EnemyMgr
{
    float radius = 5f;
    Vector3 spawnPos;
    float time = 5f;
    int action;
    protected override void Awake()
    {
        base.Awake();

        spawnPos = transform.position;
    }

    private void Update()
    {
        Observe();
    }

    void Observe()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        else
        {
            time = 5f;

            action = Random.Range(0, 2);
        }

        switch (action)
        {
            case 0: //idle
                AI.isStopped = true;

                if (AI.hasPath == true)
                {
                    //경로를 가지고있을 때 reset해라!
                    AI.ResetPath();
                }

                break;
            case 1: //move
                AI.isStopped = false;

                if (AI.hasPath == false)
                {
                    time = 3f;
                    Vector3 ranVec = Random.onUnitSphere * radius;
                    ranVec.y = 0;
                    AI.SetDestination(spawnPos + ranVec);
                }

                break;
        }
    }

    /*
     *      
    Vector3 ObservePath()
    {
        //u를 눌렀을 때 spawnPos에서 (z)-5, +5 범위안에 드는 pos를
        //이 오브젝트 pos에 지정
        Vector3 newPos = spawnPos;

        newPos.x = Random.Range(spawnPos.x - radius, spawnPos.x + radius);
        newPos.z = Random.Range(spawnPos.z - radius, spawnPos.z + radius);

        float _distance = (spawnPos - newPos).magnitude;

        while (_distance > radius)
        {
            //Debug.LogWarning("범위에 나감");

            newPos.z = Random.Range(spawnPos.z - radius, spawnPos.z + radius);

            //현재 새로운 위치와 스폰 위치의 거리를 저장
            _distance = (spawnPos - newPos).magnitude;
        }

        //Debug.LogWarning("범위에 들어옴");

        //Debug.Log("거리 : " + (spawnPos - newPos).magnitude);

        return newPos;
    }
     */

    public override void Die()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(spawnPos, radius);
    }
}
