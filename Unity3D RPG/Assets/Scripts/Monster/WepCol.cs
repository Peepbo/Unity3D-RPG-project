using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepCol : MonoBehaviour
{
    MeshCollider mesh;
    int atk;
    private void Start()
    {
        mesh = transform.GetComponent<MeshCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().GetDamage(atk);

            mesh.enabled = false;
            
        }
    }

    public void setAtk(int value)
    {
        atk = value;
    }    
}
