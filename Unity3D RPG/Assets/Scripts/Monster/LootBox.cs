using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootBox : MonoBehaviour
{
    private bool isOpen = false;
    private float dropRate = 0f;
    List<ItemInfo> item = new List<ItemInfo>();
    ItemInfo drop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOpen = true;
        }
    }

 


}
