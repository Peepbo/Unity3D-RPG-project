using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    public List<ItemInfo> myItem = new List<ItemInfo>();
    public int myCurrency;

    public void SaveChest(ItemInfo item)
    {
        myItem.Add(item);
    }

    public void LoadData()
    {
        //item.Add(ItemCSV.Instance.find("OldSword"));
        //myItem에 csv data 받아오기

    }

    public void SaveData()
    {

    }
}
