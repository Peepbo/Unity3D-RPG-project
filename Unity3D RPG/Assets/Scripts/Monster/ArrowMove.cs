using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    
    public float speed =3.0f;
    Transform targetPoint;
    Rigidbody rigid;
    private void Awake()
    {
        targetPoint = GameObject.FindWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
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
