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

    public float thinkCoolTime = 10f;
    public float observeRange = 5f;

    public int action = 0;
    Vector3 currentPos;

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

    }

    private void ChangeState()
    {
        Observe();

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
    public void Observe()
    {
        //반경 5m

        //휴식할때만 사용
        //if (isObserve == true)
        //{
        //    thinkCoolTime -= Time.deltaTime;

        //    if (thinkCoolTime < 0)
        //    {
        //        thinkCoolTime = UnityEngine.Random.Range(2, 6);
        //        action = UnityEngine.Random.Range(0, 2);

        //        state = (MeleeState)action;

        //        if (state == MeleeState.Run)
        //        {
        //            Vector3 _angle = transform.eulerAngles;
        //            _angle.y = UnityEngine.Random.Range(0, 359);
        //            transform.eulerAngles = _angle;
        //        }
        //    }
        //}


    }
    public void Idle()
    {

        print("Idle");
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

        //if (isObserve)
        //{
        //    print("감시중");
        //}
        //else
        //{
        //findRange범위에서 Target 방향으로
        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;
        transform.rotation = Quaternion.LookRotation(_direction);
        controller.Move(_direction * speed * Time.deltaTime);

        //  if (_distance > findRange) isObserve = true;
        // }

        if (_distance > findRange)
        {
            state = MeleeState.Idle;
        }

        if (_distance < attackRange)
        {
            print("attack Player!");
            state = MeleeState.Attack;
        }


        //로컬 기준
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //if (_distance < walkableRange)
        //{
        //    //vector3.forward = vector(0,0,1) <- world좌표
        //    //0,0,1

        //    //transform.forward <- local좌표의 forward

        //    //월드 기준
        //    controller.Move(transform.forward * speed * Time.deltaTime);
        //}


        //controller.Move(Vector3.forward * speed * Time.deltaTime);
        //Vector3 _lookPos = target.transform.position - transform.position;
        ////target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
        //_lookPos.y = 0;

        //transform.rotation = Quaternion.LookRotation(_lookPos);

        //_lookPos.Normalize();

        //controller.Move(_lookPos * speed * Time.deltaTime);
    }

    public override void Attack()
    {

        Vector3 _lookPos = (target.transform.position - transform.position).normalized;
        //y축 바라보면서 도는 거 방지
        _lookPos.y = 0;
        
        float _distance = Vector3.Distance(transform.position, target.transform.position);

        transform.rotation = Quaternion.LookRotation(_lookPos);
        if (_distance > attackRange)
        {
            print("추적중");
            state = MeleeState.Run;
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
    }
    private void Die()
    {
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
