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
        StateAwake();

    }
    // Start is called before the first frame update
    void Start()
    {
        AnimStart();

    }
   
    // Update is called once per frame
    void Update()
    {
        Move();
        GetInput();
        Turn();

        PlayerStateUpdate();
        PlayerStatUpdate();

        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerData.Instance.SaveChest(13);  
        }
    }
}
