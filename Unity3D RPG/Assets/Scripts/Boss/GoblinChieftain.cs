
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class GoblinChieftain : BossDB, IDamagedState
{
    enum ChiefTainFXPrefab
    {
        ATK1,
        ATK2,
        THUMP,
        ROAR,
        SPAWNM,
        SPAWNS,

    }
    enum BossATKPattern
    {
        THREEATK,
        THUMP,
        SPAWN,
        END
    }

    //[SerializeField]
    //private BossDB info = new BossDB();
    //private BossState state = BossState.IDLE;
    //private BossController bossContrroller = new BossController();
    private Animator anim;
    NavMeshAgent agent;
    const float distance = 3f;
    
    //bool isDie = false;
    float hitTime = 0f;
    BossATKPattern pattern;
    const int spawnAreaMaxCount = 5;

    
    
    
    void Start()
    {
        ChieftainDBInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void ChieftainDBInit()
    {
        const int itemDropCount = 3;
        bossName = "고블린 치프틴";
        hpMax = 200;
        hp = hpMax;
        atk = 50;
        def = 15;
        atkSpeed = 1.0f;
        atkDelay = 4.0f;
        moveSpeed = 2.0f;
        goldMin = 200;
        goldMax = 300;
        itemDropInfo = new ItemDropInfo[itemDropCount];
        itemDropInfo[0].itemName = "고블린 수정";
        itemDropInfo[0].itemID = 81;
        itemDropInfo[1].itemName = "고블린 족장의 증표";
        itemDropInfo[1].itemID = 84;
        itemDropInfo[2].itemName = "족장의 목걸이";
        itemDropInfo[2].itemID = 85;
        state = BossState.IDLE;
        dieTime = 5.5f;

        for (int i = 0; i < itemDropCount; i++)
        {
            itemDropInfo[i].itemDropCount = 1;
            
            //아이템 정보  list<itemInfo>로 전달
            info = CSVData.Instance.find(itemDropInfo[i].itemID);
            item.Add(info);
        }

    }

    private void Update()
    {
        if (!start) return;

        switch (state)
        {
            case BossState.COMBATIDLE:
                CombatIdle();
                break;
            case BossState.ATK:
                break;
            case BossState.RUN:
                Move();
                break;
            case BossState.HIT:
                hitTime += Time.deltaTime;
                if (hitTime > 5f)
                { state = BossState.COMBATIDLE; hitTime = 0f; }
                break;
            case BossState.DIE:
                break;
        }

        if (state != BossState.ATK)
            atkTime += Time.deltaTime;
        if (isSpawn)
            MinionsCheck();
        Debug.Log(state);

    }



    private void CombatIdle()
    {
        anim.SetTrigger("CombatIdle");
        weapon.enabled = false;

        if ((int)(transform.position - target.position).magnitude > (int)distance)
        {
            state = BossState.RUN;

        }
        else if (hp <= (hpMax / 2) && !isRoar)
        {
            isRoar = true;
            atkTime = atkDelay / 2;
            state = BossState.ATK;
            anim.ResetTrigger("CombatIdle");
            anim.SetTrigger("Roar");
            GameObject _fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.ROAR]);
            Vector3 _temp = transform.position;
            _temp.y -= 1.629f;
            _fx.transform.position = _temp;
        }
        else if (atkTime > atkDelay)
        {
            state = BossState.ATK;
            ATK();
        }
    }

    public void Move()
    {
        anim.SetTrigger("Run");
        agent.SetDestination(target.position);
        agent.stoppingDistance = distance;
        if ((int)(transform.position - target.position).magnitude <= (int)distance)
        {
            state = BossState.COMBATIDLE;
        }

    }
    public void ATK()
    {
        anim.ResetTrigger("CombatIdle");

        atkTime = 0f;
        transform.forward = (target.position - transform.position).normalized;

        if (isRoar && !isSpawn)
            pattern = (BossATKPattern)UnityEngine.Random.Range(0, (int)BossATKPattern.END);
        else if (isRoar)
            pattern = (BossATKPattern)UnityEngine.Random.Range(0, (int)BossATKPattern.SPAWN);
        else
            pattern = BossATKPattern.THREEATK;

        switch (pattern)
        {
            case BossATKPattern.THREEATK:
                //IBossATKPattern atkPattern = new ThreeATKPattern();
                //ATKPattern(atkPattern);
                anim.SetTrigger("ThreeATK");
                anim.SetInteger("ThreeATKKind", 0);
                break;
            case BossATKPattern.THUMP:
                anim.SetTrigger("Thump");
                break;
            case BossATKPattern.SPAWN:
                anim.SetTrigger("Spawn");
                GameObject _fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.SPAWNM]);
                Vector3 _temp = transform.position;
                _temp.y -= 1.629f;
                _fx.transform.position = _temp;

                break;
        }
    }


    public void ThreeATKCombo()
    {
        if ((int)(transform.position - target.position).magnitude > (int)distance)
            ComBatIdleState();
        else
        {
            anim.SetInteger("ThreeATKKind", 1);
            transform.forward = (target.position - transform.position).normalized;
        }
    }
    public void ThumpFX()
    {
        GameObject fx;
        fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.THUMP]);
        Vector3 temp = transform.position + transform.forward*2.3f;
        //temp = transform.forward * 1.5f;
        fx.transform.position = temp;
    }
    public void Spawn()
    {
        isSpawn = true;

        if (minions.Count != 0)
        {
            for (int i = 0; i < minionMaxCount; i++) { Destroy(minions[i]); }
            minions.Clear();
        }

        atkTime += atkDelay * 0.5f;
        shuffle();
        for (int i = 0; i < minionMaxCount; i++)
        {
            minions.Add(Instantiate(minionFactory));
            minions[i].transform.position = spawnArea[i].position;
            GameObject _fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.SPAWNS]);
            _fx.transform.position = spawnArea[i].position;
        }
    }

    public void ComBatIdleState()
    {
        state = BossState.COMBATIDLE;
        isHit = false;
    }
    public void Damaged(int value)
    {
        if (isHit) return;

        isHit = true;
        hp -= GetDamage(def, value);
        if (hp > 0)
        {
            isPlayerCri = target.GetComponent<Player>().isCri;
            Hit();
        }
        else
        {
            hp = 0;
            start = false;
            Die();
        }

    }

    public void SetDamage()
    {
        int _atkDam = 0;
        
        switch (pattern)
        {
            case BossATKPattern.THREEATK:
                _atkDam = atk / 3;
                GameObject fx;
                fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.ATK1]);
                fx.transform.position = target.position;
                break;
            case BossATKPattern.THUMP:
                _atkDam = atk;
               
                break;
        }
        //target.GetComponent<TESTATTACK>().GetDamage(temp);
        target.GetComponent<Player>().GetDamage(_atkDam);
    }

    //IEnumerator ThreeATKFX()
    //{



    //    //yield return new WaitForSeconds(fx.get)
    //}
    private void shuffle()
    {
        for (int i = 0; i < 5; i++)
        {
            int num1 = UnityEngine.Random.Range(0, spawnAreaMaxCount);
            int num2 = UnityEngine.Random.Range(0, spawnAreaMaxCount);
            Transform temp = spawnArea[num1];
            spawnArea[num1] = spawnArea[num2];
            spawnArea[num2] = temp;

        }
    }

    private void MinionsCheck()
    {
        for (int i = 0; i < minionMaxCount; i++)
        {
            if (minions[i].activeSelf)
            {
                isSpawn = true;
                break;
            }
            isSpawn = false;
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(dieTime);
        //아이템 드롭 
        var bossItem = Instantiate(itemBox, transform.position, Quaternion.identity);
        bossItem.GetComponent<LootBox>().setItemInfo(item, 3, goldMin, goldMax);

        Destroy(gameObject);

    }
    private void Hit()
    {
        if (state != BossState.ATK)
        {
            if (isPlayerCri) { anim.SetInteger("HitKind", UnityEngine.Random.Range(2, 4)); }
            else { anim.SetInteger("HitKind", UnityEngine.Random.Range(0, 2)); }

            anim.SetTrigger("Hit");
            state = BossState.HIT;
        }
    }
    public void ActiveCollision()
    {
        weapon.enabled = weapon.enabled ? false : true;
    }
}
