using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public string itemName;

    public GameObject popUp;
    public GameObject uiBarrier;

    //chest manager getdata
    public ChestManager cm;

    public void SaveData(string data)
    {
        itemName = data;
    }

    public void BuyItem()
    {
        //ItemInfo _item = CSVData.Instance.find(itemName);

        //PlayerData.Instance.SaveChest(_item);
        
        //Debug.Log(_item.itemName + "을 얻었습니다!");

        //cm.GetData();
    }

    public void PopSetting(bool value)
    {
        popUp.SetActive(value);
        uiBarrier.SetActive(value);
    }
}
