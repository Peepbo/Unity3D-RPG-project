
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class GoblinChieftain : BossDB, IDamagedState
{
    enum BossATKPattern
    {
        THREEATK,
        THUMP,
        SPAWN,
        END
    }

    
  
    private Animator anim;
    NavMeshAgent agent;
    const float distance = 3f;
    
    //bool isDie = false;
    float hitTime = 0f;
    BossATKPattern pattern;
    const int spawnAreaMaxCount = 5;
    public GameObject returnButton;
    
    
    
    void Start()
    {
        ChieftainDBInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void ChieftainDBInit()
    {
        const int itemDropCount = 3;
        //bossName = "고블린 치프틴";
        //hpMax = 500;
        hp = hpMax;
        //atk = 50;
        //def = 15;
        //atkSpeed = 1.0f;
        //atkDelay = 4.0f;
        //moveSpeed = 2.0f;
        //goldMin = 200;
        //goldMax = 300;
        //itemDropInfo = new ItemDropInfo[itemDropCount];
        //itemDropInfo[0].itemName = "고블린 수정";
        //itemDropInfo[0].itemID = 81;
        //itemDropInfo[1].itemName = "고블린 족장의 증표";
        //itemDropInfo[1].itemID = 84;
        //itemDropInfo[2].itemName = "족장의 목걸이";
        //itemDropInfo[2].itemID = 85;
        //state = BossState.IDLE;
        dieTime = 5.5f;

        hpBar.maxValue = hpMax;
        hpBar.value = hp;

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
        if (hpBar.value != (float)hp)
            hpBar.value -= 1.0f;
        if (!start) return;

        switch (state)
        {
            case BossState.COMBATIDLE:
                CombatIdle();
                break;
            case BossState.ATK:
                hitTime += Time.deltaTime;
                if (hitTime > 5f)
                { state = BossState.COMBATIDLE; hitTime = 0f; }
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
        
        //Debug.Log(state);

    }

    public void IsRoaring() { anim.SetBool("isRoaring", false); }

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
            anim.SetBool("isRoaring", true);
            atkTime = atkDelay / 2;
            state = BossState.ATK;
            anim.ResetTrigger("CombatIdle");
            anim.SetTrigger("Roar");
            def = def * 2;
            Vector3 _temp = transform.position;
            _temp.y -= 1.629f;
            EffectManager.Instance.EffectActive(9, _temp,Quaternion.identity);
            SoundManager.Instance.SFXPlay("Chief_Roar", transform.position);
            SoundManager.Instance.SFXPlay("Chief_RoarVO", transform.position);
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
                SoundManager.Instance.SFXPlay("Chief_ThumpVO", transform.position);//
                break;
            case BossATKPattern.SPAWN:
                anim.SetTrigger("Spawn");
                //GameObject _fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.SPAWNM]);
                Vector3 _temp = transform.position;
                _temp.y -= 1.629f;
                //_fx.transform.position = _temp;
                EffectManager.Instance.EffectActive(10, _temp, Quaternion.identity);
                SoundManager.Instance.SFXPlay("Chief_Spawn", transform.position);
                SoundManager.Instance.SFXPlay("Chief_SpawnVO", transform.position);

                break;
        }
    }

    public void NormalATKSound()
    {
        SoundManager.Instance.SFXPlay("Chief_ATK", transform.position);
        string _atkVo = "Chief_ATKVO0" + Random.Range(1, 4).ToString();
        SoundManager.Instance.SFXPlay(_atkVo, transform.position);
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
        //GameObject fx;
        //fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.THUMP]);
        Vector3 _temp = transform.position + transform.forward*2.6f;
        _temp.y -= 1.629f;
        //fx.transform.position = _temp;
        EffectManager.Instance.EffectActive(12, _temp, Quaternion.identity);
        SoundManager.Instance.SFXPlay("Chief_Thump", transform.position + transform.forward * 2.6f);
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
            //GameObject _fx = Instantiate(FXfactory[(int)ChiefTainFXPrefab.SPAWNS]);
            //_fx.transform.position = spawnArea[i].position;
            Debug.Log("Spawn_S");
            EffectManager.Instance.EffectActive(11, spawnArea[i].position, Quaternion.identity);
            
        }
    }

    public void ComBatIdleState()
    {
        state = BossState.COMBATIDLE;
    }
    public void Damaged(int value)
    {
        if (isHit||isDead) return;
        isHit = true;
        StartCoroutine(IsHitTimer());
        hp -= GetDamage(def, value);
       
        if (hp > 0)
        {
            isPlayerCri = target.GetComponent<Player>().isCri;
            if(state != BossState.ATK)
                Hit();
        }
        else
        {
            hp = 0;
            start = false;
            isDead = true;
            Die();
        }
    }
    IEnumerator IsHitTimer() { yield return new WaitForSeconds(0.3f);isHit = false; }

    public void SetDamage()
    {
        int _atkDam = 0;
        Vector3 _temp = target.position;
        _temp.y += 1.5f;
        EffectManager.Instance.EffectActive(6, _temp, Quaternion.identity);
        switch (pattern)
        {
            case BossATKPattern.THREEATK:
                _atkDam = atk / 3;
               
                break;
            case BossATKPattern.THUMP:
                _atkDam = atk;
                break;
        }
        //target.GetComponent<TESTATTACK>().GetDamage(temp);
        target.GetComponent<Player>().GetDamage(_atkDam);
    }

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
        SoundManager.Instance.SFXPlay("Chief_DieVO", transform.position);
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(dieTime);
        //아이템 드롭 
        var bossItem = Instantiate(itemBox, transform.position, Quaternion.identity);
        bossItem.GetComponent<LootBox>().setItemInfo(item, 3, goldMin, goldMax);

        yield return new WaitForSeconds(1.0f);
        returnButton.SetActive(true);

        Destroy(gameObject);

        DungeonResultPanel.Instance.GameResult(true);

    }
    private void Hit()
    {
            if (isPlayerCri) { anim.SetInteger("HitKind", UnityEngine.Random.Range(2, 4)); }
            else { anim.SetInteger("HitKind", UnityEngine.Random.Range(0, 2)); }

            if(Random.Range(0,3)==0)
                SoundManager.Instance.SFXPlay("Chief_Hit", transform.position);
            anim.SetTrigger("Hit");
            state = BossState.HIT;
    }
    public void ActiveCollision()
    {
        weapon.enabled = weapon.enabled ? false : true;
    }
}
