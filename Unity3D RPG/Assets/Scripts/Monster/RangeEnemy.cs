using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeState
{
    IDLE, RUN, ATTACK, DAMAGED, DIE
};
enum AttackStateR
{
    GAUGING, ATTACK, REST
}
enum R_MoveState
{
    OBSERVE, TRACK, RETURN
}

public class RangeEnemy : EnemyManager
{
    RangeState state;
    AttackStateR atkState;
    R_MoveState moveState;

    Transform firePoint;
    public string prefabTag;
    public GameObject tempEffect;

    float atkTime = 0;
    bool oneShot = false;


    protected override void Awake()
    {
        base.Awake();
        state = RangeState.IDLE;
        firePoint = transform.Find("firePoint");

    }
    private void Start()
    {
        spawnPos = transform.position;
       
    }
    protected override void Update()
    {
        base.Update();
        ChangeState();
    }

    IEnumerator delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(1f);
        isDelay = false;
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
            case RangeState.IDLE:
                Idle();
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
                if (Vector3.Distance(radar.position, spawnPos) < observeRange)
                {
                    _able.Add(_angle[i]);
                }
            }
            StartCoroutine(delay());
            int _index = Random.Range(0, _able.Count);



            transform.rotation = Quaternion.Euler(0, _able[_index], 0);
            state = RangeState.RUN;

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

            state = (RangeState)action;

            if (state == RangeState.RUN)
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
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            state = RangeState.RUN;
        }
    }
    public override void Move()
    {
        float _distance = Vector3.Distance(target.transform.position, transform.position);



        if (isObserve)
        {
            if (_distance < findRange)
            {
                //플레이어 쫓아감
                isObserve = false;
                moveState = R_MoveState.TRACK;
            }
            else
            {
                moveState = R_MoveState.OBSERVE;
            }
        }
        else
        {
            if (_distance > findRange)
            {
                moveState = R_MoveState.RETURN;
            }
        }
        switch (moveState)
        {
            case R_MoveState.OBSERVE:
                {
                    if (_distance < findRange)
                    {
                        isObserve = false;
                    }
                    controller.Move(transform.forward * (speed * 0.5f) * Time.deltaTime);

                    float _center2here = Vector3.Distance(transform.position, spawnPos);

                    if (isDelay == false)
                    {
                        if (_center2here > observeRange)
                        {
                            isRangeOver = true;
                            thinkCoolTime = 5;

                            state = RangeState.IDLE;
                        }

                    }
                }
                break;

            case R_MoveState.TRACK:
                {
                    Vector3 _lookPos = target.transform.position - transform.position;
                    //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
                    _lookPos.y = 0;
                    _lookPos.Normalize();

                    transform.rotation = Quaternion.LookRotation(_lookPos);

                    controller.Move(_lookPos * speed * Time.deltaTime);


                    if (_distance < attackRange)
                    {
                        state = RangeState.ATTACK;
                    }

                }
                break;

            case R_MoveState.RETURN:
                {
                    Vector3 _return = spawnPos - transform.position;
                    float _return2Spawn = _return.magnitude;
                    Vector3 _returnDirection = _return.normalized;

                    _returnDirection.y = 0;
                    transform.rotation = Quaternion.LookRotation(_returnDirection);
                    controller.Move(_returnDirection * speed * Time.deltaTime);

                    if (_return2Spawn <1.0f)
                    {
                        state = RangeState.IDLE;

                        //ai resetting
                        thinkCoolTime = 2f;
                        isObserve = true;
                    }
                }
                break;
        }

  
    }

    IEnumerator AttackAction()
    {
        //something = true;

        atkState = AttackStateR.GAUGING;
        yield return new WaitForSeconds(2.0f);
        atkState = AttackStateR.ATTACK;
        yield return new WaitForSeconds(0.5f);
        atkState = AttackStateR.REST;
        yield return new WaitForSeconds(1.0f);

        float _distance = (target.transform.position - transform.position).magnitude;

        if (_distance > attackRange)
        {
            atkState = AttackStateR.GAUGING;
            state = RangeState.RUN;
        }

        //something = false;
    }

    public override void Attack()
    {
        //fire 

        //float _dis = Vector3.Distance(target.transform.position, transform.position);

        ////del
        Vector3 _target2here = target.transform.position - transform.position; //return 거리 + 방향
        float _distance = _target2here.magnitude; //return 거리
        Vector3 _direction = _target2here.normalized;
        //return 방향

        ////
        //STATE
        //1.플레이어 노려보기
        //2.공격
        //3.휴식

        //1. 코루틴
        //2. float += time.deltatime

        atkTime += Time.deltaTime;

        if (atkTime < 2f) atkState = AttackStateR.GAUGING;
        else if (atkTime < 2.5f) atkState = AttackStateR.ATTACK;
        else if (atkTime < 3.5f) atkState = AttackStateR.REST;

        else //time >= 3.5
        {
            atkTime = 0;
            //상대가 나갔는지 안나갔는지 상태 변환
            if (_distance > attackRange)
            {
                atkState = AttackStateR.GAUGING;
                state = RangeState.RUN;
            }

        }

        switch (atkState)
        {
            case AttackStateR.GAUGING:
                //1.플레이어 노려보기
                {

                    if (atkTime < 1.8f)
                    {
                        _direction.y = 0;
                        transform.rotation = Quaternion.LookRotation(_direction);

                        oneShot = false;
                    }

                }
                break;
            case AttackStateR.ATTACK:
                //2.공격
                {
                    if (!oneShot)
                    {
                        oneShot = true;

                        var _prefabFactory = ObjectPool.SharedInstance.GetPooledObject(prefabTag);

                        if (_prefabFactory != null)
                        {
                            _prefabFactory.transform.position = firePoint.position;
                            _prefabFactory.transform.rotation = transform.rotation;
                            _prefabFactory.SetActive(true);
                        }
                    }
                }
                break;
            case AttackStateR.REST:
                //3.휴식
                {

                }
                break;
        }
    }

    public override void Damaged(int damage)
    {
        base.Damaged(damage);
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(spawnPos, observeRange);

    }
}
