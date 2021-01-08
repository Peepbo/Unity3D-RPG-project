
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class GoblinChieftain : BossDB, IDamagedState
{

    //[SerializeField]
    //private BossDB info = new BossDB();
    //private BossState state = BossState.IDLE;
    //private BossController bossContrroller = new BossController();
    private Animator anim;
    NavMeshAgent agent;
    public Transform target;
    const float distance = 3f;
    bool isRoar = false;
    bool isSpawn = false;
    bool isPlayerCri = false;
    bool isHit = false;
    bool isDie = false;
    public CapsuleCollider weapon;
    BossATKPattern pattern;
    const int spawnAreaMaxCount = 5;
    List<GameObject> minions = new List<GameObject>();
    void Start()
    {
        ChieftainDBInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //bossContrroller.Init(target,anim,agent);
    }

    private void ChieftainDBInit()
    {
        const int itemDropCount = 3;
        bossName = "고블린 치프틴";
        hpMax = 200;
        hp = 200;
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
        dieCount = 5.5f;
        for (int i = 0; i < itemDropCount; i++) { itemDropInfo[i].itemDropCount = 1; }
     


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
                
                break;
            case BossState.DIE:
                break;
        }

        if (state != BossState.ATK)
            atkTime += Time.deltaTime;
        if (isSpawn)
            MinionsCheck();

       
    }

   

    private void CombatIdle()
    {
        anim.SetTrigger("CombatIdle");

        if ((int)(transform.position - target.position).magnitude > (int)distance)
        {
            state = BossState.RUN;
            
        }
        else if(hp <= (hpMax/2)&& !isRoar)
        {
            isRoar = true;
            atkTime = atkDelay / 2;
            state = BossState.ATK;
            anim.ResetTrigger("CombatIdle");
            anim.SetTrigger("Roar");
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
        else if( isRoar )
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
                //Spawn();
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
        }
    }
    
    public void ComBatIdleState() 
    { 
        state = BossState.COMBATIDLE; 
    }
    public void Damaged(int value)
    {
        isHit = true;
        hp -= value;
        //hp -= value-def>=0? value-def:0;
        Debug.Log(hp);
        if (hp > 0)
        {
            Debug.Log(hp);
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
                _atkDam  = atk / 3;
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
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine() 
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(dieCount);
        Destroy(gameObject);

    }
    private void Hit()
    {
        isHit = false;
        anim.SetTrigger("Hit");

        if (isPlayerCri && state == BossState.ATK)
            anim.SetInteger("HitKind", 3);
        else if (!isPlayerCri && state == BossState.ATK)
            anim.SetInteger("HitKind", 2);
        else if (isPlayerCri)
            anim.SetInteger("HitKind", 1);
        else
            anim.SetInteger("HitKind", 0);

        state = BossState.HIT;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.GetComponent<Player>() != null && isHit)
    //    {
    //        isPlayerCri = collision.transform.GetComponent<Player>().isCri;
    //        Hit();
    //        Debug.Log("hitColl");
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player>() != null && isHit)
        {
            isPlayerCri = other.transform.GetComponent<Player>().isCri;
            Hit();
            Debug.Log("hitColl");
        }
    }


    public void ActiveCollision()
    {
        weapon.enabled = weapon.enabled ? false : true;
        
    }
}
