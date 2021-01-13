using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActive : MonoBehaviour, IDamagedState
{
    public int num;
    bool isActive;
    BoxCollider col1;
    MeshCollider col2;

    private void Start()
    {
        col1 = GetComponent<BoxCollider>();
        col2 = GetComponent<MeshCollider>();
    }

    public void Damaged(int value)
    {
        if(!isActive)
        {
            isActive = true;
            transform.GetComponentInParent<LeverMng>().TurnOn();
            col1.enabled = false;
            col2.enabled = false;
        }
    }
}
