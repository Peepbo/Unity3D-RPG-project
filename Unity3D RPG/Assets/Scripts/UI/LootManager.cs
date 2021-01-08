using System.Collections.Generic;
using UnityEngine;

public partial class LootManager : Singleton<LootManager>
{
    protected LootManager() { }

    public GameObject poccketPanel;
    pocketData pd = new pocketData();

    public class pocketData
    {
        public List<ItemInfo> pocketItem = new List<ItemInfo>();  // 던전에서 획득 한 전리품 (아이템)
        public int pocketMoney = 0;                               // 던전에서 획득 한 전리품 (돈)
        //public ItemInfo myPocketItem = null;
    }

    public void GetPocketData(List <ItemInfo> pocketItemInfo)
    {
        //List<ItemInfo> _itemList = new List<ItemInfo>();

        //ItemInfo _item = CSVData.Instance.find(64); // count = 1
        //_item.count = 3;

        //_itemList.Add(_item);

        //ItemInfo _item0 = CSVData.Instance.find(52);
        
        //_itemList.Add(_item);

        //GetPocketData(_itemList);


        //itemA, itemB, itemC, itemD...
        for(int i = 0; i < pocketItemInfo.Count; i++)
        {
            ItemInfo _item = pocketItemInfo[i];


            if (pd.pocketItem.Contains(_item) == false) pd.pocketItem.Add(_item);

            else
            {
                int _index = pd.pocketItem.IndexOf(_item);
                pd.pocketItem[_index].count += _item.count;
            }
        }

        
    }

    public void GetPocketMoney(int currency)
    {
        pd.pocketMoney += currency;
    }

    public void Delivery()
    {
        //for (int i = 0; i < pd.pocketItem.Count; i++)
        //{
        //    for (int j = 0; j < pd.pocketItem[i].count; j++)
        //    {
        //        PlayerData.Instance.SaveChest(pd.pocketItem[i].id);
        //    }
        //}
        for (int i = 0; i < pd.pocketItem.Count; i++)
        {
            PlayerData.Instance.SaveChest(pd.pocketItem[i].id, pd.pocketItem[i].count);
        }

        PlayerData.Instance.myCurrency += pd.pocketMoney;
        PlayerData.Instance.SaveData();
    }

    public void ClearPocketData()
    {
        pd.pocketItem.Clear();
        pd.pocketMoney = 0;
    }
}
