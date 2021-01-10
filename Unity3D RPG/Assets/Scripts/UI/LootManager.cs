using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class pocketData
//{
//    public List<ItemInfo> pocketItem = new List<ItemInfo>();  // 던전에서 획득 한 전리품 (아이템)
//    public int pocketMoney = 0;                               // 던전에서 획득 한 전리품 (돈)
//                                                              //public ItemInfo myPocketItem = null;
//}

public partial class LootManager : Singleton<LootManager>
{
    protected LootManager() { }

    //public GameObject poccketPanel;
    //pocketData pd = new pocketData();
    public List<ItemInfo> pocketItem = new List<ItemInfo>();
    public int pocketMoney = 0;
    //public GameObject checkMoney;

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
            ItemInfo _item = new ItemInfo();
            _item = pocketItemInfo[i];

            if (pocketItem.Contains(_item) == false) pocketItem.Add(_item);

            else
            {
                int _index = pocketItem.IndexOf(_item);
                pocketItem[_index].count += _item.count;
            }
        }

        //checkMoney.transform.GetChild(1).GetComponent<Text>().text = pocketMoney.ToString();
    }

    public void GetPocketMoney(int currency)
    {
        pocketMoney += currency;
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
        for (int i = 0; i < pocketItem.Count; i++)
        {
            PlayerData.Instance.SaveChest(pocketItem[i].id, pocketItem[i].count);
        }

        PlayerData.Instance.myCurrency += pocketMoney;
        PlayerData.Instance.SaveData();
    }

    public void ClearPocketData()
    {
        pocketItem.Clear();
        pocketMoney = 0;
    }
}
