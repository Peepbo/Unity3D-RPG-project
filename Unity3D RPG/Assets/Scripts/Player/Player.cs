using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Player : MonoBehaviour
{
    PlayerController playerC;
    private PlayerMain playerInput;
    private void Awake()
    {

        playerC = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("1");
        }
        
        Move();
        GetInput();
        Turn();
        ChangeState();
    }
}
