using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

partial class ChestManager
{
    public GameObject infoPanel;
    public GameObject sellPanel;

    public Slider mySlider;
    public Text curCount;
    public Text maxCount;

    public Text sellingPrice;
    public Text playerMoney;

    public void GetLootsData()
    {
        //int num = 0;

        for (int i = 0; i < 16; i++)
        {
            if((i+1) > PlayerData.Instance.haveLootItem.Count)
            {
                lootsData[i].transform.GetChild(0).GetComponent<Image>().color = Color.clear; // 표시 X
                popInfo1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = null;
                continue;
            }

            else
            {
                int _itemNumber = PlayerData.Instance.haveLootItem[i].id;

                lootsData[i].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
                popInfo1.transform.GetChild(i).GetChild(1).GetComponent<Text>().text =
                    PlayerData.Instance.haveLootItem[i].count.ToString();
            }

            //if (num == lootList.Count)
            //{
            //    lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear; // 표시 X
            //    popInfo1.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = null;
            //    continue;
            //}

            //else
            //{
            //    int _itemNumber = lootList[i - 1].id;

            //    lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
            //    popInfo1.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = lootList[i - 1].count.ToString();

            //}
            //num++;
        }
    }

    public void ChangeLootsInfo(int selectedLootNum)
    {
        if (selectedLootNum >= PlayerData.Instance.haveLootItem.Count) return;

        selectNumber = selectedLootNum;

        List<string> lootInfo = new List<string>();

        lootInfo.Add(PlayerData.Instance.haveLootItem[selectedLootNum].itemName);
        lootInfo.Add("가격 : " + PlayerData.Instance.haveLootItem[selectedLootNum].price);
        lootInfo.Add("소지량 : " + PlayerData.Instance.haveLootItem[selectedLootNum].count);

        //sell info의 정보 업데이트
        int _max = PlayerData.Instance.haveLootItem[selectedLootNum].count;

        mySlider.maxValue = _max;
        maxCount.text = _max.ToString();

        for (int i = 0; i < lootInfo.Count; i++)
        {
            infoPanel.transform.GetChild(i).GetComponent<Text>().text = lootInfo[i];
        }

        infoPanel.SetActive(true);
    }

    public void SellItem()
    {
        PlayerData.Instance.myCurrency += PlayerData.Instance.haveLootItem[selectNumber].price * (int)mySlider.value;
        //삭제
        PlayerData.Instance.haveLootItem[selectNumber].count -= (int)mySlider.value;

        if (PlayerData.Instance.haveLootItem[selectNumber].count == 0) 
            PlayerData.Instance.haveLootItem.RemoveAt(selectNumber);

        else mySlider.maxValue = PlayerData.Instance.haveLootItem[selectNumber].count;

        mySlider.value = 1;

        GetLootsData(); // 이건 잘 됨

        //저장
        PlayerData.Instance.SaveData();
    }

    public void RootUpdate()
    {
        if(selectNumber >= PlayerData.Instance.haveLootItem.Count)
        {
            curCount.text = "";
            sellingPrice.text = "";
        }

        else
        {
            curCount.text = ((int)mySlider.value).ToString();
            sellingPrice.text = ((int)mySlider.value * PlayerData.Instance.haveLootItem[selectNumber].price).ToString();
        }

        playerMoney.text = PlayerData.Instance.myCurrency.ToString();
    }
}
