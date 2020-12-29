using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyMgr, IDamagedState
{
    private ObservingMove observe;
    private FollowTarget follow;
    private ReturnMove returnToHome;
    private ViewingAngle viewAngle;
    private Vector3 startPos;
    private Vector3 ranDirection;

    [Range(3, 7)]
    public float observeRange;
    [Range(0, 180)]
    public float angle;

    int hp;

    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        ranDirection = GetRandomDirection();

        hp = maxHp;
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        //Goblin Attack skill

        weapon.GetComponent<AxColision>().SetDamage(atkPower);
        observe.initVariable(controller, startPos, ranDirection, speed * 0.5f, observeRange);
        follow.initVariable(controller, target, speed);
        returnToHome.initVariable(controller, startPos, speed);

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


        if (Input.GetKeyDown(KeyCode.L))
        {
            Damaged(atkPower);
        }

        if (isDamaged || isDead) return;

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

            }
            //player가 공격 범위에 없으면
            else
            {
                if (_isFind && !returnToHome.getIsReturn())
                {

                    anim.SetInteger("state", 1);

                    if (anim.GetBool("isRest") == false)
                    {
                        FollowTarget();
                    }
                }

                else if (_isFind == false || returnToHome.getIsReturn() == true)
                {
                    anim.SetInteger("state", 1);

                    if (anim.GetBool("isRest") == false)
                    {
                        ReturnToStart();
                    }
                }
            }

        }

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
        ranDirection = GetRandomDirection();
        setMoveType(observe);

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
        Move();
    }

    public void ReturnToStart()
    {
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        setMoveType(returnToHome);
        returnToHome.setIsReturn(true);

        Move();

        if (_homeDistance <= 0.1f)
        {
            returnToHome.setIsReturn(false);
            observe.setIsObserve(true);

            //controller의 speed를 velocity의 값에 넣어준다.
            anim.SetFloat("velocity", controller.velocity.magnitude);
        }
    }


    public void ActiveMeshCol()
    {
        weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void DeActiveMeshCol()
    {
        weapon.GetComponent<MeshCollider>().enabled = false;
    }


    public void GetRest()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        //rest를 켜고
        anim.SetBool("isRest", true);
        yield return new WaitForSeconds(1.5f);

        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;

        //transform.LookAt(_direction);

        transform.forward = (_direction);

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetBool("isRest", false);
    }

    IEnumerator GetDamage()
    {
        isDamaged = true;

        yield return new WaitForSeconds(.7f);

        isDamaged = false;
    }
    public void Damaged(int value)
    {
        if (isDamaged || isDead) return;
        print("Goblin에 " + value + "만큼 데미지를 입힘");

        hp -= value;

        if(hp<=0)
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");
        }
        anim.SetTrigger("isDamage");

        StartCoroutine(GetDamage());


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
