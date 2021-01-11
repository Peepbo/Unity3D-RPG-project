using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonS : EnemyMgr, IDamagedState
{
    private FollowTarget follow;
    private Vector3 direction;
    private float distance;

    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();

        hp = maxHp = 60;
        atk = 55;
        def = 10.0f;
        AI.enabled = false;
        follow = gameObject.AddComponent<FollowTarget>();
        follow.Init(AI, target, speed, 0);
    }

    void Update()
    {
        if (isDead)
        {
            Die();
            return;
        }

        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        distance = Vector3.Distance(target.transform.position, transform.position);
        if (AI.enabled == false) AI.enabled = true;

        if (distance < findRange)
        {
            Follow();

            if (distance <= attackRange)
            {
                AI.stoppingDistance = attackRange;
                //attack;
            }
        }
        else
        {
            //stop;
        }
    }

    private void Follow()
    {
        setMoveType(follow);
        Move();
    }

    public void Damaged(int value)
    {
        if (isDead) return;

        if (hp > 0)
        {
            if (!isDamaged)
            {
                hp -= (int)(value * (1.0f - def / 100));

            }

            if (hp<=0)
            {
                hp = 0;
                controller.enabled = false;
                AI.enabled = false;
                StopAllCoroutines();
            }
        }
    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if(disappearTime>2.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
