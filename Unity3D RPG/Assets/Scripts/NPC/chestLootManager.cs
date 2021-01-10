using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class chestLootManager : MonoBehaviour
{
    public Transform slots;

    [Header("Loot Panel")]
    public Text currency;
    [Header("Info Panel")]
    public Transform itemInfos;
    [Header("Sell Panel")]
    public Transform sellInfos;
    public Slider slider;
    public Text sellCount;
    public Text totalPrice;
    [Space]
    public int selectNumber;

    private void OnEnable()
    {
        LinkData();
    }

    private void Update()
    {
        if(sellInfos.gameObject.activeSelf)
        {
            //sellCount
            sellCount.text = ((int)slider.value).ToString();
            //price
            if(PlayerData.Instance.haveLootItem.Count > 0)
            {
                totalPrice.text = (((int)slider.value) *
                    PlayerData.Instance.haveLootItem[selectNumber].price).ToString();
            }
        }
    }

    private void LinkData()
    {
        Debug.Log("Data Load and Link");
        PlayerData.Instance.LoadData_v2();

        GetLoot();

        currency.text = PlayerData.Instance.myCurrency.ToString();
    }

    private void GetLoot()
    {
        for (int i = 0; i < slots.childCount; i++)
        {
            Transform _slot = slots.GetChild(i);

            if((i+1) > PlayerData.Instance.haveLootItem.Count )
            {
                _slot.GetComponent<Button>().enabled = false;

                _slot.GetChild(0).GetComponent<Image>().color = Color.clear;
                _slot.GetChild(1).GetComponent<Text>().text = "";

                continue;
            }

            int _index = PlayerData.Instance.haveLootItem[i].id;

            _slot.GetComponent<Button>().enabled = true;

            //Debug.Log(PlayerData.Instance.haveLootItem[i].count);

            _slot.GetChild(0).GetComponent<Image>().color = Color.white;
            _slot.GetChild(1).GetComponent<Text>().text =
                PlayerData.Instance.haveLootItem[i].count.ToString();

            _slot.GetChild(0).GetComponent<Image>().sprite = GetPath(_index);
        }
    }

    private void ChangeInfo(ItemInfo item)
    {
        List<string> _infos = new List<string>();

        _infos.Add(item.itemName);
        _infos.Add("가격 : " + item.price.ToString());
        _infos.Add("소지량 : " + item.count.ToString());

        for(int i = 0; i < 3; i++)
        {
            itemInfos.GetChild(i).GetComponent<Text>().text = _infos[i];
        }
    }

    private Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }


    #region BUTTON ACTIONS
    public void GetInfo(int number)
    {
        selectNumber = number;

        ItemInfo _item = PlayerData.Instance.haveLootItem[selectNumber];

        ChangeInfo(_item);
    }

    public void GetSellInfo()
    {
        ItemInfo _item = PlayerData.Instance.haveLootItem[selectNumber];

        //value
        slider.value = 1;

        //max
        sellInfos.GetChild(1).GetComponent<Text>().text = (_item.count).ToString();
        slider.maxValue = _item.count;
        //sellCount
        sellCount.text = ((int)slider.value).ToString();
        //price
        totalPrice.text = (((int)slider.value) * _item.price).ToString();
    }

    public void SellItem()
    {

        int _price = PlayerData.Instance.haveLootItem[selectNumber].price;

        PlayerData.Instance.haveLootItem[selectNumber].count -= (int)slider.value;

        if (PlayerData.Instance.haveLootItem[selectNumber].count == 0)
            PlayerData.Instance.haveLootItem.RemoveAt(selectNumber);

        PlayerData.Instance.myCurrency += _price * ((int)slider.value);

        PlayerData.Instance.SaveData();

        LinkData();
    }
    #endregion
}
