using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    private bool isOpen = false;
    ItemInfo item;
    private void Start()
    {
    }

    private void Update()
    {
        if(isOpen)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            isOpen = true;   
        }
    }
}
