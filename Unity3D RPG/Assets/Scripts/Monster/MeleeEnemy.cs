﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MeleeState
{
    IDLE, RUN, ATTACK, DAMAGED, DIE
};
public class MeleeEnemy : EnemyManager
{
    MeleeState state;
    ViewingAngle viewingAngle;

    bool isAttackActive = false;


    //del

    public float stay = 0f;
    //float _number = 0;
    //float _count = 2;


    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(1f);
        isDelay = false;
    }
    IEnumerator disappearObject()
    {
        yield return new WaitForSeconds(2f);
        print("die");
        DropItem item;
        item = GetComponent<DropItem>();
        item.dropItem(3);
        gameObject.SetActive(false);
    }
    protected override void Awake()
    {
        base.Awake();
        state = MeleeState.IDLE;
        viewingAngle = GetComponent<ViewingAngle>();
    }

    private void Start()
    {
        currentPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        ChangeState();


        viewingAngle.ableToDamage();
        //del
        //_number += Time.deltaTime;
        //if (_number > _count && hp > 0)
        //{
        //    Damaged(1);
        //    _number = 0;
        //}

    }

    private void ChangeState()
    {
        //감시 중이며
        if (isObserve)
        {
            //선을 넘지 않으면? 
            if (!isRangeOver) Observe();

            //선을 넘으면?
            else RangeOver();
        }

        switch (state)
        {
            case MeleeState.IDLE:
                Idle();
                break;
            case MeleeState.RUN:
                Move();
                break;
            case MeleeState.ATTACK:
                Attack();
                break;
            case MeleeState.DAMAGED:
                break;
            case MeleeState.DIE:
                Die();
                break;
        }

    }

    public void RangeOver()
    {
        if (isDead) return;
        //여기는 휴식 반경을 넘어갔을 때!
        thinkCoolTime -= Time.deltaTime;

        if (thinkCoolTime < 0)
        {
            thinkCoolTime = 3f;
            //체크 할 각도
            float[] _angle = { 0, 60, 120, 180, 240, 300 };

            //0번,3번,5번

            //0 1   2

            //0,60,120

            List<float> _able = new List<float>();

            for (int i = 0; i < 6; i++)
            {
                pivotCenter.rotation = Quaternion.Euler(pivotCenter.rotation.x, _angle[i], pivotCenter.rotation.z);

                //만약 observeRange안에 있으면
                if (Vector3.Distance(radar.position, currentPos) < observeRange)
                {
                    _able.Add(_angle[i]);
                }
            }
            StartCoroutine(delay());
            int _index = Random.Range(0, _able.Count);



            transform.rotation = Quaternion.Euler(0, _able[_index], 0);
            state = MeleeState.RUN;

            isRangeOver = false;
        }

        //Debug.Log("여러분 저 선넘었어요!");
    }

    public void Observe()
    {
        //반경 5m

        //휴식할때만 사용
        thinkCoolTime -= Time.deltaTime;

        if (thinkCoolTime < 0)
        {
            thinkCoolTime = Random.Range(2, 6);
            action = Random.Range(0, 2);

            state = (MeleeState)action;

            if (state == MeleeState.RUN)
            {
                //Vector3 _angle = transform.eulerAngles;
                //_angle.y = UnityEngine.Random.Range(0, 359);
                //transform.eulerAngles = _angle;
                transform.forward = GetRandomDirection();
            }
        }
    }

    public void Idle()
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance < findRange)
        {
            state = MeleeState.RUN;
        }
    }

    public override void Move()
    {
        //observeRange 안에 있을 때
        //float _distance = Vector3.Distance(currentPos, transform.position);
        float _distance = Vector3.Distance(transform.position, target.transform.position);

        if (isObserve)
        {
            //print("감시중");

            if (_distance < findRange)
            {
                isObserve = false;
            }
            controller.Move(transform.forward * (speed * 0.5f) * Time.deltaTime);

            float _center2here = Vector3.Distance(transform.position, currentPos);

            if (isDelay == false)
            {
                if (_center2here > observeRange)
                {
                    isRangeOver = true;
                    thinkCoolTime = 5;

                    state = MeleeState.IDLE;
                }

            }
        }
        else
        {
            //findRange범위에서 Target 방향으로
            Vector3 _direction = target.transform.position - transform.position;
            _direction.Normalize();
            _direction.y = 0;
            transform.rotation = Quaternion.LookRotation(_direction);
            controller.Move(_direction * speed * Time.deltaTime);

            if (_distance > findRange)
            {
                currentPos = transform.position;
                state = MeleeState.IDLE;
                isObserve = true;
            }

            if (_distance < attackRange)
            {
                //print("attack Player!");
                state = MeleeState.ATTACK;
            }
        }

    }
    IEnumerator PatternBasic()
    {
        print("start");

        BasicAttack _attack = BasicAttack.FindObjectOfType<BasicAttack>();

        isAttackActive = true;
        yield return new WaitForSeconds(2f);
        _attack.hit();
        yield return new WaitForSeconds(1f);
        _attack.hit();
        yield return new WaitForSeconds(1f);
        _attack.hit();

        isAttackActive = false;
    }
    public override void Attack()
    {    
        float _distance = Vector3.Distance(transform.position, target.transform.position);

        //부채꼴 범위 안에 있으면
        if (viewingAngle.ableToDamage() && !isAttackActive)
        {
            stay += Time.deltaTime;

            //얘가 공격할 때 저 위에 함수가 true면? 플레이가 맞는거
            if(stay > 1)
            {
                stay = 0;
                StartCoroutine(PatternBasic());
            }
        }

        if (!isAttackActive)
        {
            Vector3 _targetPos = target.transform.position;
            _targetPos.y = transform.position.y;

            transform.forward = Vector3.Slerp(transform.forward, _targetPos - transform.position, Time.deltaTime * 3f);
        }

        if (viewingAngle.ableToDamage() == false) stay = 0;


        if (_distance > attackRange)
        {
            //print("추적중");
            state = MeleeState.RUN;
        }
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
        if (isDead)
        {
            StartCoroutine(disappearObject());

            state = MeleeState.DIE;
        }
    }

    private void Die()
    {

    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);

        if (isObserve)
        {
            Gizmos.color = Color.green;
            //Gizmos.color = Color.Lerp(Color.clear, Color.green, Mathf.PingPong(Time.time, 1.0f));
            Gizmos.DrawWireSphere(currentPos, observeRange);
        }
        else
        {
            Gizmos.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.5f));
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

    }
}