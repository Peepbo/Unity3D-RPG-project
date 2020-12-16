using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    public List<ItemInfo> item = new List<ItemInfo>();

    public string output = "이건되?";

    public void LoadData()
    {
        //item.Add(ItemCSV.Instance.find("OldSword"));
    }

    public void SaveData()
    {

    }
}
