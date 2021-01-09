using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shaman : EnemyMgr, IDamagedState
{
    private Vector3 startPos;

    private ObservingMove observe;
    private ViewingAngle viewAngle;

    private bool isDetected;
    private Transform firePos;

    private bool isFire;

    private bool isLooking;

    [Range(1, 5)]
    public float skillSpawn;

    protected override void Awake()
    {
        base.Awake();
        startPos = transform.position;
        firePos = transform.Find("FirePos");
        hp = maxHp =30 / 2;    //체크용
        hp = maxHp =30;
        atk = 40;
        def = 0f;
        minGold = 25;
        maxGold = 45;
        anim.SetInteger("state", 0);

        itemKind[0] = 79;
        itemKind[1] = 80;
        itemKind[2] = 83;
        for (int i = 0; i < 3; i++)
        {
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }
    }

    void Start()
    {
        observe = gameObject.AddComponent<ObservingMove>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
        observe.Init(AI, startPos, speed, observeRange);
    }

    void Update()
    {

        if (isDead) Die();

        if (!isDetected)
        {
            Observe();
        }

        else
        {
            if (isLooking)
            {
                anim.SetInteger("state", 2);
                Fire();
            }
        }

    }

    public void Observe()
    {
        if (isDead) return;

        setMoveType(observe);
        observe.setIsObserve(true);
        observe.move();

        int _action = observe.getAction();
        anim.SetInteger("state", _action);

        isDetected = viewAngle.FoundTarget(target, findRange, angle);

        if (isDetected == true)
        {
            Vector3 _dir = (target.transform.position - transform.position).normalized;
            _dir.y = 0;

            transform.rotation = Quaternion.LookRotation(_dir);

            StartCoroutine(Look());
            //코루틴돌고
            //1.5초뒤에 isLooking가 트루로 바꿈
            //그리고 그안에 아까 사용한 회전함수 하면된다.

        }
        //만약 isDetected가 true가 되면? <before : observe> -> atk

        //ItemInfo _item;
        //_item.count = 6;

        //find => count 1개..
    }

    IEnumerator Look()
    {
        isLooking = false;
        float t = 0f;
        while (t < 1.5f)
        {
            t += Time.deltaTime;

            Vector3 _direction = (target.transform.position - transform.position).normalized;
            _direction.y = 0;
            Quaternion _targetRotation = Quaternion.LookRotation(_direction);

            //Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime);
            Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, t / 1.5f);

            transform.localRotation = _nextRotation;

            yield return null;
        }

        isLooking = true;
    }

    IEnumerator Rest()
    {
        isFire = true;
        anim.SetBool("isRest", true);

        float t = 0f;
        while (t < 1.5f)
        {
            t += Time.deltaTime;

            Vector3 _direction = (target.transform.position - transform.position).normalized;
            _direction.y = 0;
            Quaternion _targetRotation = Quaternion.LookRotation(_direction);

            //Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime);
            Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, t / 1.5f);

            transform.localRotation = _nextRotation;

            yield return null;
        }


        anim.SetBool("isRest", false);

        isFire = false;
    }

    public void Fire()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.85f)
        {
            //플레이어가 부채꼴 안에 있는지 검사를 한다. (공격 모션 거의 끝남)
            isDetected = viewAngle.FoundTarget(target, findRange, angle);
            Vector3 _dir = (target.transform.position - transform.position).normalized;
            _dir.y = 0;

            if (!isFire) StartCoroutine(Rest());

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

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("die");
            controller.enabled = false;
            AI.enabled = false;
            StopAllCoroutines();
        }
        else
        {
            if (player.isCri)
                anim.SetTrigger("damage");
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
        if (disappearTime > 5f)
        {
            //아이템 떨어트리기
            var Item = Instantiate(ItemBox, transform.position, Quaternion.identity);
            Item.GetComponent<LootBox>().setItemInfo(item, 4, minGold, maxGold);

            gameObject.SetActive(false);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, observeRange);
    }
}
