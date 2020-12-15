using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Player : MonoBehaviour
{
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetInput();
        Turn();
    }
}
