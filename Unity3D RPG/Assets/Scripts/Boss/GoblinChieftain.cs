using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinChieftain : MonoBehaviour
{
    [SerializeField]
    private BossDB info = new BossDB();
    private BossController bossContrroller = new BossController();
    private Animator anim;
    NavMeshAgent agent;
    public Transform target;
    void Start()
    {
        ChieftainInit();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void ChieftainInit()
    {
        const int itemDropCount = 3;
        info.name = "고블린 치프틴";
        info.hpMax = 200;
        info.hp = 200;
        info.atk = 50;
        info.def = 15;
        info.atkSpeed = 1.0f;
        info.moveSpeed = 2.0f;
        info.goldMin = 200;
        info.goldMax = 300;
        info.itemDropInfo = new ItemDropInfo[itemDropCount];
        info.itemDropInfo[0].itemName = "고블린 수정";
        info.itemDropInfo[0].itemID = 81;
        info.itemDropInfo[1].itemName = "고블린 족장의 증표";
        info.itemDropInfo[1].itemID = 84;
        info.itemDropInfo[2].itemName = "족장의 목걸이";
        info.itemDropInfo[2].itemID = 85;
        for (int i = 0; i < itemDropCount; i++) { info.itemDropInfo[i].itemDropCount = 1; }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bossContrroller.Move(target, ref agent);
        }
    }

}
