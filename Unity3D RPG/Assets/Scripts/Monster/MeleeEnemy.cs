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

    public float thinkTime = 10f;
    public float walkableRange=5f;
    //idle -> ?
    public int nowState = 0;
    Vector3 tempAngle;
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

        if(isObserve == true) 
        {
            if (thinkTime < 0)
            {
                thinkTime = UnityEngine.Random.Range(2, 6);
                nowState = UnityEngine.Random.Range(0, 2);

                state = (MeleeState)nowState;

                float _distance = Vector3.Distance(transform.position, currentPos);
                if (state == MeleeState.Run)
                {
                    //print(currentPos);
                    //print(_distance);
                    if(_distance < walkableRange)
                    {
                        Vector3 _angle = transform.eulerAngles;
                        _angle.y = UnityEngine.Random.Range(0, 359);
                        transform.eulerAngles = _angle;
                    }
                }
            }
            else thinkTime -= Time.deltaTime;
            //여기서 대충 주변을 돌아다닌다
        }
    }

    private void ChangeState()
    {
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

    public void Idle()
    {
        //반경 5m
        
        //휴식할때만 사용

        //플레이어 찾기
        //if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        //{
        //    state = MeleeState.Run;
        //}
    }
    public override void Move()
    {
        transform.Translate(Vector3.forward * speed* Time.deltaTime);
        //Vector3 _lookPos = target.transform.position - transform.position;
        ////target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
        //_lookPos.y = 0;

        //transform.rotation = Quaternion.LookRotation(_lookPos);

        //_lookPos.Normalize();

        //controller.Move(_lookPos * speed * Time.deltaTime);
    }

    public override void Attack()
    {
        //float _dis = Vector3.Distance(transform.position, target.transform.position);

        //if (_dis <= findRange)
        //{
        //    print("attack");
        //}
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
        //Gizmos.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 0.5f));
        //Gizmos.DrawWireSphere(transform.position, findRange);
    }

}
