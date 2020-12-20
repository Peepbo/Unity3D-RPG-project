using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemInfo> chestItem = new List<ItemInfo>();

    public void LinkData()
    {
        chestItem = PlayerData.Instance.myItem;
    }
}
