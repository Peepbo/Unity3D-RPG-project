using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class SmathManager
{
    List<ItemInfo> accList = new List<ItemInfo>();
    const int maxAcc = 35;
    public void OnAccButton()
    {
        for (int i = 0; i < maxAcc; i++)
        {
            itemList[i].SetActive(true);
        }
    }

}
