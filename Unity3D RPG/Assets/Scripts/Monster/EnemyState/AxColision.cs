using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxColision : MonoBehaviour
{
    public bool isOn;

    MeshCollider meshCol;
    int damage = 0;
    private void Awake()
    {
        meshCol = GetComponent<MeshCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().GetDamage(damage);
            GetComponent<MeshCollider>().enabled = false;

            isOn = false;
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
