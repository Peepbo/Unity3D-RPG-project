using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MeleeState
{
    Idle, Run, Attack, Damaged, Die
};
public class MeleeEnemy : EnemyManager
{
    MeleeState state;
    bool isObserve = true;
    bool isRangeOver = false;
    bool isDelay = false;

    public float thinkCoolTime = 10f;
    public float observeRange = 5f;

    public int action = 0;
    Vector3 currentPos;

    //del
    float _number = 0;
    float _count =2;


    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(1f);
        isDelay = false;
    }
    IEnumerator disappearObject()
    {
        yield return new WaitForSeconds(2f); 
        this.enabled = false;
    }
    protected override void Awake()
    {
        base.Awake();
        state = MeleeState.Idle;
    }

    private void Start()
    {
        currentPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        ChangeState();

        //del
        _number += Time.deltaTime;
        if(_number > _count && hp>0)
        {
            Damage(1);
            _number = 0;
        }

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
            case MeleeState.Idle:
                Idle();
                break;
            case MeleeState.Run:
                Move();
                break;
            case MeleeState.Attack:
                Attack();
                break;
            case MeleeState.Damaged:
                break;
            case MeleeState.Die:
                Die();
                break;
        }


    }

    public void RangeOver()
    {
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
                if (Vector3.Distance(Radar.position, currentPos) < observeRange)
                {
                    _able.Add(_angle[i]);
                }
            }
            StartCoroutine(delay());
            int _index = UnityEngine.Random.Range(0, _able.Count);

            transform.rotation = Quaternion.Euler(0, _able[_index], 0);
            state = MeleeState.Run;

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
            thinkCoolTime = UnityEngine.Random.Range(2, 6);
            action = UnityEngine.Random.Range(0, 2);

            state = (MeleeState)action;

            if (state == MeleeState.Run)
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

        //print("Idle");
        //Stay the position
        //Find the Target
        float _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance < findRange)
        {
            //isObserve = false;
            state = MeleeState.Run;
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

                    state = MeleeState.Idle;
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
                state = MeleeState.Idle;
                isObserve = true;
            }

            if (_distance < attackRange)
            {
                //print("attack Player!");
                state = MeleeState.Attack;
            }
        }

    }

    public override void Attack()
    {
        Vector3 _lookPos = (target.transform.position - transform.position).normalized;

        //y축 바라보면서 회전 방지
        //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치라고 인식시켜준다.
        _lookPos.y = 0;

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        transform.rotation = Quaternion.LookRotation(_lookPos);



        if (_distance > attackRange)
        {
            //print("추적중");
            state = MeleeState.Run;
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        if (isDead)
        {
            state = MeleeState.Die;
        }
    }

    private void Die()
    {
        print("die");
        if(this.enabled)
        {
            StartCoroutine(disappearObject());
        }
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