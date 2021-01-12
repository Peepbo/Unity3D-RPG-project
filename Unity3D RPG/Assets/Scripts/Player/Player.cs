using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    PlayerController playerC;
    public ComboAtk comboAtk;
    private MainPlayer playerInput;

    Potion potion;


    private void Awake()
    {

        JsonData.Instance.CheckJsonData();
        PlayerData.Instance.LoadData_v2();

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
        ReturnData();
        potion = GameObject.Find("PotionButton").GetComponent<Potion>();
        potion.potionNumTxt.text = PlayerData.Instance.myCurrentPotion.ToString();
        UiManager0.Instance.PanelOpen = false;
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
