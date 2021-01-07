using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCheck : MonoBehaviour
{
    int damageCount;

    private void Awake()
    {
        damageCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HandR")
        {
            damageCount = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HandR"|| other.tag == "Player")
        {
            damageCount = 0;
        }
    }
    public void ReadyToDamage(int value)
    {
        damageCount = value;
    }

    public int GetCount() { return damageCount; }
}
