using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonW : EnemyMgr, IDamagedState
{
    private FollowTarget follow;
    private ReturnMove back;

    private Vector3 startPos;
    private Vector3 direction;
    private float distance;

    private bool isStay = true;
    private int findCount = 0;
    private RaycastHit hit;


    protected override void Awake()
    {
        base.Awake();
        hp = maxHp;
        atk = 55;
        def = 10.0f;
        minGold = 40;
        maxGold = 60;

        itemKind[0] = 79;
        itemKind[1] = 86;
        itemKind[2] = 88;

        for (int i = 0; i < 3; i++)
        {
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }

    }

    private void Start()
    {
        startPos = transform.position;

        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();

        follow.Init(AI, target, speed, attackRange);
        back.init(AI, startPos, speed);
    }

    private void Update()
    {
        if (isDead)
        {
            Die();
            return;
        }

        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (isStay)
        {
            if (distance < findRange)
            {
                if (Physics.Raycast(transform.position, direction, out hit, findRange))
                {
                    if (hit.transform.tag == "Player")
                    {
                        isStay = false;
                        findCount = 1;
                    }
                    else
                    {
                        //집 지키기
                    }
                }

            }
        }

        else

        {
            if (!back.getIsReturn())
            {
                if (findCount == 1)
                {
                    Follow();

                    if (distance < attackRange)
                    {
                        //어택
                    }
                    if (distance > findRange)
                    {
                        back.setIsReturn(true);
                    }
                }

            }
            else
            {
                Back();
            }
        }

    }

    private void Follow()
    {
        setMoveType(follow);
        Move();
    }

    private void Back()
    {
        float _home = Vector3.Distance(startPos, transform.position);
        setMoveType(back);
        Move();

        if (_home < 0.5f)
        {
            isStay = true;
            findCount = 0;
        }
    }

    public void Damaged(int value)
    {
        if (hp > 0)
        {
            if (!isDamaged)
            {
                hp -= value;
             
            }
        }
        else
        {
            hp = 0;
            isDead = true;
        }


    }

    public override void Die()
    {

    }
}
