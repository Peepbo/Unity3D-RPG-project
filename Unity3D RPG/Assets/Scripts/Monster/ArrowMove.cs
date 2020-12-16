using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    
    public float speed =3.0f;
    Transform targetPoint;
    Rigidbody rigid;
    Vector3 dir;
    private void Awake()
    {
        targetPoint = GameObject.FindWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        dir = targetPoint.position - transform.position;
        dir.Normalize();
    }
    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
    }

    

}
