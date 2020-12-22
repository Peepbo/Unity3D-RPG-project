﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeState
{
    IDLE, RUN, ATTACK, DAMAGED, DIE
};
public class RangeEnemy : EnemyManager
{
    RangeState state;

    GameObject arrowPrefab;
    Transform firePoint;

    float spawnCount = 0;
    protected override void Awake()
    {
        base.Awake();
        state = RangeState.IDLE;
        firePoint = transform.Find("firePoint");
    }
    protected override void Update()
    {
        base.Update();
        ChangeState();
    }

    private void ChangeState()
    {
        switch (state)
        {
            case RangeState.IDLE:
                break;
            case RangeState.RUN:
                Move();
                break;
            case RangeState.ATTACK:
                Attack();
                break;
            case RangeState.DAMAGED:
                break;
            case RangeState.DIE:
                break;
        }
    }
    public void Idle()
    {
        //
        // 
        //
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            state = RangeState.RUN;
        }
    }
    public override void Move()
    {
        Vector3 _lookPos = target.transform.position - transform.position;
        //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
        _lookPos.y = 0;

        transform.rotation = Quaternion.LookRotation(_lookPos);

        _lookPos.Normalize();

        controller.Move(_lookPos * speed * Time.deltaTime);

    }

    public override void Attack()
    {
        //fire 
        arrowPrefab = ObjectPool.SharedInstance.GetPooledObject("arrow");

        float _dis = Vector3.Distance(target.transform.position, transform.position);
        spawnCount += Time.deltaTime;
        if (_dis < attackRange)
        {
            if (spawnCount > 3.0f)
            {
                arrowPrefab.transform.position = firePoint.position;
                arrowPrefab.transform.rotation = transform.rotation;
                arrowPrefab.SetActive(true);
                // Instantiate(arrowPrefab,firePoint.position,transform.rotation);

                spawnCount = 0;
            }
        }
        else state = RangeState.RUN;
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.cyan, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.Lerp(Color.red, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}

