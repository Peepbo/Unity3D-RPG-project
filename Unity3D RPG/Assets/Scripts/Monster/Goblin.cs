using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr, IDamagedState
{
    private ObservingMove observe;
    private FollowTarget follow;
    private ReturnMove returnToHome;

    private SmashDown smash;
    private ViewingAngle viewAngle;
    private Vector3 startPos;

    [Range(3, 7)]
    public float observeRange;
    [Range(0, 180)]
    public float angle;


    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        //Goblin Attack skill
        smash = gameObject.AddComponent<SmashDown>();

        anim.SetInteger("state", 0);
    }

    void Update()
    {
        Vector3 _transfrom = transform.position;
        if (!controller.isGrounded)
        {
            _transfrom.y += gravity * Time.deltaTime;
        }

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        bool _isFind = viewAngle.FoundTarget(target, findRange, angle);


        //animation idle or run
        if (observe.getAction() == 0)
        {
            anim.SetInteger("state", 0);
        }
        else if (observe.getAction() == 1)
        {
            anim.SetInteger("state", 1);
        }


        //Observe range 안에 있을 때
        if (observe.getIsObserve())
        {
            //타겟을 찾으면
            if (_isFind)
            {
                observe.setIsObserve(false);

            }
            else
            {
                //타겟을 못찾으면 제자리에서 observe
                Observe();
            }

        }
        //Observe range 안에 없을 때
        else
        {
            //player가 공격 범위에 들어오면
            if (_distance < attackRange)
            {
                anim.SetInteger("state", 2);
                AttackTarget();
            }
            //player가 공격 범위에 없으면
            else
            {
                if (_isFind && !returnToHome.getIsReturn())
                {

                    anim.SetInteger("state", 1);
                    FollowTarget();


                }

                else if(_isFind == false || returnToHome.getIsReturn() == true)
                {
                    anim.SetInteger("state", 1);
                    ReturnToStart();
                }
            }

        }


        //setAttackType(smash);
        //Attack();
    }

    private void setIdleState()
    {
        bool _isFind = viewAngle.FoundTarget(target, findRange, angle);

        if (observe.getIsRangeOver())
        {
            anim.SetInteger("state", 0);
        }


        if (_isFind)
        {
            anim.SetInteger("state", 1);
        }

        //Debug.Log("Idle 상태");
    }

    public void Observe()
    {
        Vector3 _RNDDirection = GetRandomDirection();
        setMoveType(observe);
        observe.initVariable(controller, startPos, _RNDDirection, speed * 0.5f, observeRange);
        Move();

        if (observe.getAction() == 0) setIdleState();

    }

    public void FollowTarget()
    {
        //타겟 따라갈때는 observe false
        observe.setIsObserve(false);

        //controller의 speed를 animation velocity 값에 넣어준다.
        anim.SetFloat("velocity", controller.velocity.magnitude);

        setMoveType(follow);
        follow.initVariable(controller, target, speed);
        Move();
    }

    public void ReturnToStart()
    {
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        setMoveType(returnToHome);
        returnToHome.setIsReturn(true);
        returnToHome.initVariable(controller, startPos, speed);
        Move();

        if (_homeDistance <= 0.1f)
        {
            returnToHome.setIsReturn(false);
            observe.setIsObserve(true);

            //controller의 speed를 velocity의 값에 넣어준다.
            anim.SetFloat("velocity", controller.velocity.magnitude);
        }
    }

    public void AttackTarget()
    {
        //print("goblin tries attack");

        setAttackType(smash);
        smash.attack();

    }

    public void Damaged()
    {
        //print("goblin Damaged");

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
