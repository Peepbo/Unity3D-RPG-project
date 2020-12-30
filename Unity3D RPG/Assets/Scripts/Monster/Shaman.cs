using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : EnemyMgr, IDamagedState
{
    private Vector3 startPos;
    private Vector3 ranDirection;

    private ObservingMove observe;
    private ViewingAngle viewAngle;
    private FlameBall flame;

    private int hp;
    private bool isDetected;

    private Transform firePos;


    [Range(1, 5)]
    public float observeRange;
    [Range(30, 180)]
    public float angle;
    [Range(1, 5)]
    public float skillSpawn;


    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        firePos = transform.Find("FirePos");

        ranDirection = GetRandomDirection();
        hp = maxHp;

        anim.SetInteger("state", 0);
    }

    void Start()
    {
        observe = gameObject.AddComponent<ObservingMove>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        flame = gameObject.AddComponent<FlameBall>();


        observe.initVariable(controller, startPos, ranDirection, speed, observeRange);
        flame.init(target, firePos, skillSpawn);
    }

    void Update()
    {
        isDetected = viewAngle.FoundTarget(target, findRange, angle);
        if (Input.GetKeyDown(KeyCode.L)) Damaged(3);

        if (anim.GetBool("isDamage")) return;

        if (observe.getAction() == 0)
        {
            //idle 모션
            anim.SetInteger("state", 0);
        }
        else
        {
            //run모션
            anim.SetInteger("state", 1);
        }


        if (observe.getIsObserve())
        {
            if (!isDetected)
            {
                Observe();

                if (observe.getIsRangeOver())
                {
                    Idle();
                }

            }
            else
            {
                observe.setIsObserve(false);
            }

        }
        else
        {
            Detected();
        }

    }

    public void Idle()
    {
        ranDirection = GetRandomDirection();
        if (observe.getIsRangeOver())
        {
            //idle 모션
            anim.SetInteger("state", 0);
        }
        else
        {
            //run모션
            if (observe.getAction() == 0)
            {
                anim.SetInteger("state", 0);
            }
            else

                anim.SetInteger("state", 1);
        }
    }
    public void Observe()
    {
        setMoveType(observe);
        observe.setIsObserve(true);
        observe.move();

        if (observe.getAction() == 0 || observe.getIsRangeOver()) Idle();
    }

    public void Detected()
    {
        anim.SetInteger("state", 2);
        setAttackType(flame);
        flame.attack();
        if (!isDetected) observe.setIsObserve(true);
    }
    public void Damaged(int value)
    {
       
        if (anim.GetBool("isDamage")|| isDamaged || isDead) return;
        anim.SetBool("isDamage", true);

        
        
   
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("die");
        }
        print("Shaman에 " + value + "만큼 데미지를 입힘");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, observeRange);
    }
}
