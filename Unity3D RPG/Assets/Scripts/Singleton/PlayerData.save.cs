﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

partial class PlayerData
{
    public void SaveChest(int itemNumber)
    {
        ItemInfo _item = CSVData.Instance.find(itemNumber);

        //전리품이 아니면
        if (_item.kindID != 4) SaveItemType(ref haveEquipItem, _item);

        //전리품 이면
        else SaveItemType(ref haveLootItem, _item);

        SaveData();
    }

    public void SaveAbility(List<StatInfo> list)
    {
        for (int i = 0; i < 35; i++)
        {
            if (list[i].isLearn)
                myAbility[i] = 1;
            else
                myAbility[i] = 0;
        }

        SaveData();
    }

    void SaveItemType(ref List<ItemInfo> list, ItemInfo item)
    {
        if (list.Contains(item) == false) list.Add(item);

        else
        {
            int _index = list.IndexOf(item);
            list[_index].count++;
        }
    }
}