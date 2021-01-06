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
    private BossController bossContrroller = new BossController();
    private Animator anim;
    NavMeshAgent agent;
    public Transform target;
    void Start()
    {
        ChieftainDBInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        bossContrroller.Init(target);
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
        bossState = BossState.IDLE;
        for (int i = 0; i < itemDropCount; i++) { itemDropInfo[i].itemDropCount = 1; }

    }

    private void Update()
    {
        if (!start) return;
        Debug.Log("Start");
        switch (bossState)
        {
            case BossState.IDLE:
                bossContrroller.Idle(ref agent, ref anim, ref bossState);
                break;
            case BossState.COMBATIDLE:
                bossContrroller.CombatIdle(ref anim);
                break;
            case BossState.ATK:
                break;
            case BossState.RUN:
                bossContrroller.Move(ref agent,ref anim);
                break;
            case BossState.HIT:

                break;
            case BossState.DIE:
                break;
        }
    }

    public void Move()
    {
       
       
    }
    public void Damaged(int value)
    {
        hp -= value;
        bossState = BossState.HIT;
        bossContrroller.Hit(ref anim);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            Player player = collision.transform.GetComponent<Player>();

            if (player.isCri && bossState == BossState.ATK)
                anim.SetInteger("HitKind", 3);
            else if(!player.isCri && bossState == BossState.ATK)
                anim.SetInteger("HitKind", 2);
            else if (player.isCri)
                anim.SetInteger("HitKind", 1);
            else
                anim.SetInteger("HitKind", 0);
        }
    }
}
