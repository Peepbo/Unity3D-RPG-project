using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shaman : EnemyMgr, IDamagedState
{
    private Vector3 startPos;

    private ObservingMove observe;
    private ViewingAngle viewAngle;


    #region stat
    private int hp;
    private int atk;
    private float def;
    private int gold;
    #endregion

    private bool isDetected;
    private Transform firePos;


    [Range(1, 5)]
    public float observeRange;
    [Range(30, 360)]
    public float angle;
    [Range(1, 5)]
    public float skillSpawn;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        firePos = transform.Find("FirePos");

        hp = maxHp;

        anim.SetInteger("state", 0);
    }

    void Start()
    {
        observe = gameObject.AddComponent<ObservingMove>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        observe.Init(AI, startPos, speed, observeRange);
      
    }

    void Update()
    {
      
        if (isDamaged || isDead) return;

        if (!isDetected)
        {
            Observe();
        }

        else
        {
            anim.SetInteger("state", 2);

            Fire();
        }

    }

    public void Observe()
    {
        setMoveType(observe);
        observe.setIsObserve(true);
        observe.move();

        int _action = observe.getAction();
        anim.SetInteger("state", _action);

        isDetected = viewAngle.FoundTarget(target, findRange, angle);

        //만약 isDetected가 true가 되면? <before : observe> -> atk
    }

    public void Fire()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.85f)
        {
            //플레이어가 부채꼴 안에 있는지 검사를 한다. (공격 모션 거의 끝남)
            isDetected = viewAngle.FoundTarget(target, findRange, angle);
            Vector3 _dir = (target.transform.position - transform.position).normalized;
            _dir.y = 0;

            transform.rotation = Quaternion.LookRotation(_dir);
            //만약 isDetected가 false가 되면? <before : atk상태> -> observe
            if (isDetected == false)
            {
                int _action = observe.getAction();
                anim.SetInteger("state", _action);
            }
         
        }

    }

    IEnumerator GetDamage()
    {
        isDamaged = true;

        yield return new WaitForSeconds(1.0f);

        isDamaged = false;
    }

    public void Damaged(int value)
    {

        if (isDamaged || isDead) return;

        hp -= (int)(value * (1.0f - def / 100));
        anim.SetTrigger("damage");

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("die");
            controller.enabled = false;
        }

        StartCoroutine(GetDamage());
    }

    public void Flame()
    {
        var _firePrefab = ObjectPool.SharedInstance.GetPooledObject("EnemySkill");

        _firePrefab.transform.position = firePos.position;
        _firePrefab.transform.rotation = firePos.rotation;
        _firePrefab.SetActive(true);
    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;
        if(disappearTime>5f)
        {
            //아이템 떨어트리기
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, observeRange);
    }
}
