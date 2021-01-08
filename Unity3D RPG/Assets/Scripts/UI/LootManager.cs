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
        public ItemInfo myPocketItem = null;
    }

    public void GetPocketData(List <ItemInfo> pocketItemInfo)
    {
        ItemInfo _item = CSVData.Instance.find(pd.myPocketItem.id);

        if (pocketItemInfo.Contains(_item) == false) pd.pocketItem.Add(_item);

        else
        {
            int _index = pd.pocketItem.IndexOf(_item);
            pocketItemInfo[_index].count++;
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
            for (int j = 0; j < pd.pocketItem[i].count; j++)
            {
                PlayerData.Instance.SaveChest(pd.pocketItem[i].id);
            }
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
