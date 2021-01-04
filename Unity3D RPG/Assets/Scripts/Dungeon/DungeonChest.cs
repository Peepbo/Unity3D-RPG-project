using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChest : MonoBehaviour, IDamagedState
{
    public void Damaged(int value)
    {
        gameObject.SetActive(false);
    }
}
