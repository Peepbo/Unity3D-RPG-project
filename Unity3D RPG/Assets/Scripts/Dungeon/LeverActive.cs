using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActive : MonoBehaviour, IDamagedState
{
    public int num;
    bool isActive;

 
    public void Damaged(int value)
    {
        if(!isActive)
        {
            isActive = true;
            transform.GetComponentInParent<LeverMng>().TurnOn();
        }
    }
}
