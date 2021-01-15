using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.UI;

public class GetItemInfo : MonoBehaviour
{

    private int currency;
    private List<ItemInfo> getItem = new List<ItemInfo>();
    //private

    public void ShowCurrency(int value)
    {
        currency = value;
        Debug.Log(currency);
    }
    
    public void ShowItem(List<ItemInfo> gainItem)
    {
        getItem = gainItem;

        for (int i = 0; i < getItem.Count; i++)
        {
            Debug.Log(getItem[i].itemName + " 획득");

        }

    }
}
