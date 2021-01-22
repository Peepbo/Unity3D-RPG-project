using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActive : MonoBehaviour, IDamagedState
{
    public int num;
    bool isActive;

    Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Damaged(int value)
    {
        if(!isActive)
        {
            isActive = true;
            transform.GetComponentInParent<LeverMng>().TurnOn();
            gameObject.tag = "Untagged";
            transform.GetChild(0).gameObject.SetActive(false);

            outline.enabled = false;
        }
    }
}
