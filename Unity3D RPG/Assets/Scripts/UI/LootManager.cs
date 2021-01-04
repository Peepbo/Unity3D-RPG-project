using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class LootManager : Singleton<LootManager>
{
    protected LootManager () { }

    public GameObject poccketPanel;
    pocketData pd = new pocketData();

    public class pocketData
    {
        public List<ItemInfo> pocketItem = new List<ItemInfo>();  // 던전에서 획득 한 전리품 (아이템)
        public int pocketMoney = 0;                               // 던전에서 획득 한 전리품 (돈)
        public ItemInfo myPocketItem = null;
    }

    public void GetPocketData(int itemId)
    {
        ItemInfo _item = CSVData.Instance.find(itemId);

        if (pd.pocketItem.Contains(_item) == false) pd.pocketItem.Add(_item);

        else
        {
            int _index = pd.pocketItem.IndexOf(_item);
            pd.pocketItem[_index].count++;
        }
    }

    public void GetPocketMoney(int currency)
    {
        pd.pocketMoney += currency;
    }

    public void Delivery()
    {
        for (int i = 0; i < pd.pocketItem.Count; i++)
        {
            for(int j = 0; j < pd.pocketItem[i].count; j++)
            {
                PlayerData.Instance.SaveChest(pd.pocketItem[i].id);
            }
        }

        PlayerData.Instance.myCurrency += pd.pocketMoney;

        pd.pocketItem.Clear();
        pd.pocketMoney = 0;
    }

    public void ShowPocketData()
    {
        int num = 0;

        for (int i = 1; i < 11; i++)
        {
            if (num == pd.myPocketItem.count)
            {
            }
        }
    }
}
