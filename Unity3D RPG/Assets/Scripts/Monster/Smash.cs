using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    GameObject target;
    int atk=45;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            target.transform.GetComponent<Player>().GetDamage(atk);
        }
            
    }
}
