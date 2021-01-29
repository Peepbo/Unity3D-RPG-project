
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
    private NavMeshAgent agent;
    private const float distance = 3f;
    private float hitTime = 0f;
    private BossATKPattern pattern;
    private const int spawnAreaMaxCount = 5;
    
    void Start()
    {
        ChieftainDBInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void ChieftainDBInit()
    {
        const int itemDropCount = 3;
        
        hp = hpMax;
        
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

        if (hp <= (hpMax / 2) && !isRoar)
        {
            Roar();
        }
        else
        {
            if (state != BossState.ATK && state != BossState.ROAR)
            {
                atkTime += Time.deltaTime;

                if (atkTime >= atkDelay)
                {
                    anim.SetBool("isATK", true);
                    state = BossState.COMBATIDLE;
                }
            }
            
            switch (state)
            {
                case BossState.COMBATIDLE:
                    CombatIdle();
                    break;
                case BossState.RUN:
                    Move();
                    break;
                case BossState.HIT:
                    hitTime += Time.deltaTime;
                    if (hitTime >= 0.5f)
                        state = BossState.COMBATIDLE;
                    break;
            }
        }

        if (isSpawn)
            MinionsCheck();
    }

    private void Roar()
    {
        isRoar = true;
        atkDelay = atkDelay * 0.5f;
        state = BossState.ROAR;
        anim.ResetTrigger("Hit");
        anim.SetTrigger("Roar");
        def = def * 2;
        Vector3 _temp = transform.position;
        _temp.y -= 1.629f;
        EffectManager.Instance.EffectActive(9, _temp, Quaternion.identity);
        SoundManager.Instance.SFXPlay("Chief_Roar", transform.position);
        SoundManager.Instance.SFXPlay("Chief_RoarVO", transform.position);
    }

    private void CombatIdle()
    {
        anim.SetTrigger("CombatIdle");
        weapon.enabled = false;

        if ((int)(transform.position - target.position).magnitude > (int)distance)
        {
            state = BossState.RUN;
            Move();
        }
        
        else if (anim.GetBool("isATK"))
        {
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
        state = BossState.ATK;
        anim.ResetTrigger("CombatIdle");
        atkTime = 0f;
        hitTime = 0f;
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
                anim.SetTrigger("ThreeATK");
                anim.SetInteger("ThreeATKKind", 0);
                break;
            case BossATKPattern.THUMP:
                anim.SetTrigger("Thump");
                SoundManager.Instance.SFXPlay("Chief_ThumpVO", transform.position);//
                break;
            case BossATKPattern.SPAWN:
                anim.SetTrigger("Spawn");
                
                Vector3 _temp = transform.position;
                _temp.y -= 1.629f;

                EffectManager.Instance.EffectActive(10, _temp, Quaternion.identity);
                SoundManager.Instance.SFXPlay("Chief_Spawn", transform.position);
                SoundManager.Instance.SFXPlay("Chief_SpawnVO", transform.position);

                break;
        }
    }

    public void NormalATKSound() //애니메이션 이벤트에서 사용중
    {
        SoundManager.Instance.SFXPlay("Chief_ATK", transform.position);
        string _atkVo = "Chief_ATKVO0" + Random.Range(1, 4).ToString();
        SoundManager.Instance.SFXPlay(_atkVo, transform.position);
    }
    public void ThreeATKCombo() //애니메이션 이벤트에서 사용중
    {
        if ((int)(transform.position - target.position).magnitude > (int)distance)
            ComBatIdleState();
        else
        {
            anim.SetInteger("ThreeATKKind", 1);
            transform.forward = (target.position - transform.position).normalized;
        }
    }
    public void ThumpFX()  //애니메이션 이벤트에서 사용중
    {
        Vector3 _temp = transform.position + transform.forward*2.6f;
        _temp.y -= 1.629f;
        EffectManager.Instance.EffectActive(12, _temp, Quaternion.identity);
        SoundManager.Instance.SFXPlay("Chief_Thump", transform.position + transform.forward * 2.6f);
    }
    public void Spawn()  //애니메이션 이벤트에서 사용중
    {
        isSpawn = true;

        if (minions.Count != 0)
        {
            for (int i = 0; i < minionMaxCount; i++) { Destroy(minions[i]); }
            minions.Clear();
        }

        atkTime += atkDelay * 0.5f;
        Shuffle();
        for (int i = 0; i < minionMaxCount; i++)
        {
            minions.Add(Instantiate(minionFactory));
            minions[i].transform.position = spawnArea[i].position;
            
            EffectManager.Instance.EffectActive(11, spawnArea[i].position, Quaternion.identity);
        }
    }

    public void ComBatIdleState()  //애니메이션 이벤트에서 사용중
    {
        if (state == BossState.ATK)
            anim.SetBool("isATK", false);
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
            Hit();
        }
        else
        {
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
        target.GetComponent<Player>().GetDamage(_atkDam);
    }

    private void Shuffle()
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
        hp = 0;
        start = false;
        isDead = true;
        SoundManager.Instance.SFXPlay("Chief_DieVO", transform.position);
        anim.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(dieTime);
        //아이템 드롭 
        var bossItem = Instantiate(itemBox, transform.position, Quaternion.identity);
        bossItem.GetComponent<LootBox>().setItemInfo(item, 3, goldMin, goldMax);

        yield return new WaitForSeconds(1.0f);

        hpBar.gameObject.SetActive(false);
        ResultController.Instance.GameResult(true);

        Destroy(gameObject);

    }
    private void Hit()
    {
        if (state == BossState.ATK || isRoar || state==BossState.HIT) return; 

        isPlayerCri = target.GetComponent<Player>().isCri;

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
