using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootBox : MonoBehaviour
{
    int dropRate;
    int min, max;
    int currency;
    List<ItemInfo> item = new List<ItemInfo>();
    ItemInfo drop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Drop();
            gameObject.SetActive(false);
        }
    }

    public void setItemInfo(List<ItemInfo> dropItem, int rate,int minGold,int maxGold)
    {
        item = dropItem;
        dropRate = rate;
        min = minGold;
        max = maxGold;
    }


    public void Drop()
    {
        currency = Random.Range(min, max+1);
        LootManager.Instance.GetPocketMoney(currency);

        Debug.Log(currency + " 획득");
        List<ItemInfo> _itemList = new List<ItemInfo>();

        for (int i = 0; i < item.Count; i++)
        {

            int rate = Random.Range(0, 10);

            if (dropRate > rate) continue;

            if (dropRate <= rate)
            {
                Debug.Log(item[i].itemName + "획득!");
                _itemList.Add(item[i]);
            }

        }

        if (_itemList.Count != 0)
        {
            LootManager.Instance.GetPocketData(_itemList);
        }

    }

    //public void Drop(List<ItemInfo> dropItem,int rate)
    //{
    //    List<ItemInfo> _itemList = new List<ItemInfo>();

    //    Debug.Log(dropItem.Count);
    //    for (int i = 0; i < dropItem.Count; i++)
    //    {

    //        dropRate = 10;/*Random.Range(, 10);*/

    //        if (dropRate != 10) continue;

    //        if (dropRate == 10)
    //        {
    //            Debug.Log(dropItem[i].itemName + "획득!");
    //            _itemList.Add(dropItem[i]);
    //        }

    //    }

    //    if (_itemList.Count != 0)
    //    {
    //        LootManager.Instance.GetPocketData(_itemList);
    //    }

    //}

}
