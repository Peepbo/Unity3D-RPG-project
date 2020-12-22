using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;

    Vector3 spawnPos;
    
    private void Awake()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnPos) > 10f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            gameObject.SetActive(false);
        }
    }
}
