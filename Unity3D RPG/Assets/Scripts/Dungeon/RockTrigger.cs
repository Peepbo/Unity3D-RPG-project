using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public GameObject[] rocks = new GameObject[3];

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for(int i = 0; i < 3; i++)
            {
                rocks[i].GetComponent<Rigidbody>().useGravity = true;
            }        
        }
    }
}
