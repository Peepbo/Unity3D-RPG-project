﻿using System.Collections.Generic;
using UnityEngine;

public partial class LootManager : Singleton<LootManager>
{
    protected LootManager() { }

    public List<ItemInfo> pocketItem = new List<ItemInfo>();
    public int pocketMoney = 0;
    public int dungeonMoney = 0;

    public void GetPocketData(List <ItemInfo> pocketItemInfo)
    {
        List<ItemInfo> _list = new List<ItemInfo>();
        _list = pocketItemInfo;

        for(int i = 0; i < _list.Count; i++)
        {
            ItemInfo _item = pocketItem.Find(x => (x.id == _list[i].id));

            if (_item == null) pocketItem.Add(_list[i]);

            else
            {
                int _index = pocketItem.IndexOf(_item);
                pocketItem[_index].count += _list[i].count;
            }
        }
    }

    public void GetPocketMoney(int currency)
    {
        pocketMoney += currency;

        dungeonMoney += currency;
    }

    public void Delivery(bool die = false)
    {
        if(die)
        {
            PlayerData.Instance.myCurrency += pocketMoney;

            if(PlayerData.Instance.normalInsurance)
            {
                for (int i = 0; i < pocketItem.Count; i++)
                {
                    if (pocketItem[i].grade == "normal")
                    {
                        PlayerData.Instance.SaveChest(pocketItem[i].id, pocketItem[i].count);
                    }
                }
            }

            if(PlayerData.Instance.rareInsurance)
            {
                for (int i = 0; i < pocketItem.Count; i++)
                {
                    if (pocketItem[i].grade == "rare")
                    {
                        PlayerData.Instance.SaveChest(pocketItem[i].id, pocketItem[i].count);
                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < pocketItem.Count; i++)
            {
                PlayerData.Instance.SaveChest(pocketItem[i].id, pocketItem[i].count);
            }

            PlayerData.Instance.myCurrency += pocketMoney;
        }

        PlayerData.Instance.ResetInsurance();

        PlayerData.Instance.SaveData();

        ClearPocketData();
    }

    public void ClearPocketData()
    {
        pocketItem.Clear();
        pocketMoney = 0;
        dungeonMoney = 0;
    }
}
