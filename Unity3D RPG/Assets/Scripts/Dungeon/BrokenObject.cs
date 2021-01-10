using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour, IDamagedState
{
    public void Damaged(int value)
    {
        gameObject.SetActive(false);
    }
}
