using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    PlayerController playerC;
    public ComboAtk comboAtk;
    private MainPlayer playerInput;

   

    private void Awake()
    {
        comboAtk = GetComponentInChildren<ComboAtk>();
        playerC = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody>();
        isDie = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimStart();
        EquipStat();
        GrowthStat();
        ReturnData();
    }
   
    // Update is called once per frame
    void Update()
    {
        //Move();
        //GetInput();
        //Turn();
        //
        PlayerStateUpdate();
        PlayerStatUpdate();

     
    }
}
