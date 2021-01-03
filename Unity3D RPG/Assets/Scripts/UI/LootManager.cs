using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    public GameObject poccketPanel;
    public List<ItemInfo> pocketItem = new List<ItemInfo>();      // 던전에서 획득 한 전리품 (아이텡)
    int pocketMoney;                                              // 던전에서 획득 한 전리품 (돈)

    public void GetPocketData()
    {

    }
}
