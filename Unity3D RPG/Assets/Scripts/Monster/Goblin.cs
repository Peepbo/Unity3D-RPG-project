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
    private Vector3 direction;
    private Vector3 ranDirection;

    #region stat
    private int hp;
    private int atk = 25;
    private float def = 5.0f;
    private int gold;
    #endregion

    private int findCount;
    private bool isFind;

    //temp
    bool isObserve = true;


    [Range(3, 7)]
    public float observeRange;
    [Range(90, 300)]
    public float angle;


    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        ranDirection = GetRandomDirection();

        hp = maxHp;
        findCount = 0;
    }
    void Start()
    {
        //Goblin Move pattern
        observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        returnToHome = gameObject.AddComponent<ReturnMove>();

        weapon.GetComponent<AxColision>().SetDamage(atkPower);


        observe.initVariable(controller, startPos, ranDirection, speed * 0.5f, observeRange);
        follow.Init(AI, target, speed, attackRange);
        returnToHome.init(AI, startPos, speed);

        anim.SetInteger("state", 0);
    }

    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;


        RaycastHit _hit;

        Debug.DrawRay(transform.position, direction * findRange, Color.blue);

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        isFind = viewAngle.FoundTarget(target, findRange, angle);


        if (isDead)
        {
            Die();
            return;
        }

        if (isObserve)
        {
            if (!isFind)
            {
                anim.SetInteger("state", 0);
                // setIdleState();
            }

            else
            {
                if (Physics.Raycast(transform.position, direction, out _hit, findRange))
                {
                    // Debug.Log(_hit.transform.name);

                    if (_hit.transform.tag == "Player")
                    {
                        anim.SetInteger("state", 1);
                        findCount = 1;
                        AI.isStopped = false;
                        isObserve = false;
                    }
                    else
                    {
                        AI.isStopped = true;
                    }
                }
            }
        }
        else
        {
            if (!returnToHome.getIsReturn() && findCount == 1)
            {
                if (isFind)
                {
                    FollowTarget();

                    if (_distance <= attackRange)
                    {

                        anim.SetInteger("state", 2);
                    }
                }
                else
                {
                    returnToHome.setIsReturn(true);
                }
            }

            if (returnToHome.getIsReturn())
            {
                Back();
            }

        }

        #region
        //if (isDead) Die();

        //else
        //{

        //    if (isDamaged) return;

        //    //animation idle or run
        //    if (observe.getAction() == 0)
        //    {
        //        anim.SetInteger("state", 0);
        //    }
        //    else if (observe.getAction() == 1)
        //    {
        //        anim.SetInteger("state", 1);
        //    }


        //    //Observe 상태일 때
        //    if (observe.getIsObserve())
        //    {
        //        //타겟을 찾으면
        //        if (_isFind)
        //        {

        //            observe.setIsObserve(false);

        //        }
        //        else
        //        {
        //            //타겟을 못찾으면 제자리에서 observe
        //            Observe();
        //        }

        //    }
        //    //Observe 상태가 아닐 때
        //    else
        //    {
        //        //player가 공격 범위에 들어오면
        //        if (_distance < attackRange)
        //        {

        //            anim.SetInteger("state", 2);

        //        }
        //        //player가 공격 범위에 없으면
        //        else
        //        {
        //            if (_isFind && !returnToHome.getIsReturn())
        //            {

        //                anim.SetInteger("state", 1);

        //                if (anim.GetBool("isRest") == false)
        //                {
        //                    FollowTarget();
        //                }
        //            }

        //            else if (_isFind == false || returnToHome.getIsReturn() == true)
        //            {
        //                anim.SetInteger("state", 1);

        //                if (anim.GetBool("isRest") == false)
        //                {
        //                   // AI.stoppingDistance = 0.5f;
        //                    ReturnToStart();
        //                }
        //            }
        //        }

        //    }

        //}
        #endregion

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
        //observe.setIsObserve(false);

        //controller의 speed를 animation velocity 값에 넣어준다.
        //anim.SetFloat("velocity", controller.velocity.magnitude);
        anim.SetFloat("velocity", AI.speed);
        setMoveType(follow);
        Move();
    }

    public void Back()
    {
        float _homeDistance = Vector3.Distance(startPos, transform.position);

        if (!isDamaged)
        {
            setMoveType(returnToHome);
            Move();

            if (_homeDistance <= 0.5f)
            {
                findCount = 0;
                returnToHome.setIsReturn(false);
                AI.isStopped = true;
                isObserve = true;
                //observe.setIsObserve(true);

                //controller의 speed를 velocity의 값에 넣어준다.;
                anim.SetInteger("state", 0);
                anim.SetFloat("velocity", controller.velocity.magnitude);
            }

        }

        else
        {
            transform.rotation = Quaternion.LookRotation(direction);
            returnToHome.setIsReturn(false);

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
        AI.isStopped = true;
        yield return new WaitForSeconds(1.5f);

        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;

        //transform.LookAt(_direction);

        transform.forward = (_direction);

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        AI.isStopped = false;
    }

    IEnumerator GetDamage()
    {
        isDamaged = true;
        AI.isStopped = true;

        yield return new WaitForSeconds(.7f);

        AI.isStopped = false;
        isDamaged = false;
    }

    public void Damaged(int value)
    {
        if (isDamaged || isDead) return;

        if (hp > 0)
        {
            hp -= (int)(value * (1.0f - def / 100));
            StartCoroutine(GetDamage());
        }
        else
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");

            controller.enabled = false;
            AI.enabled = false;
            StopAllCoroutines();
            

        }
        anim.SetTrigger("isDamage");


    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;
        if (disappearTime > 5f)
        {
            //아이템 떨어트리기
            gameObject.SetActive(false);

        }
        AI.isStopped = true;

    }


    public void DropMoney(int maxGold, int minGold, Transform money, string name)
    {
        gold = Random.Range(maxGold, minGold);


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
