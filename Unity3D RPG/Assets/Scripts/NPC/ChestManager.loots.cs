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
        int num = 0;

        for (int i = 1; i < 17; i++)
        {
            if (num == lootList.Count)
            {
                lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear; // 표시 X
                popInfo1.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = null;
                continue;
            }

            else
            {
                int _itemNumber = lootList[i - 1].id;

                lootsData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
                popInfo1.transform.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = lootList[i - 1].count.ToString();

            }
            num++;
        }
    }

    public void ChangeLootsInfo(int selectedLootNum)
    {
        if (selectedLootNum >= lootList.Count) return;

        selectNumber = selectedLootNum;

        List<string> lootInfo = new List<string>();

        lootInfo.Add(lootList[selectedLootNum].itemName);
        lootInfo.Add("가격 : " + lootList[selectedLootNum].price);
        lootInfo.Add("소지량 : " + lootList[selectedLootNum].count);

        //sell info의 정보 업데이트
        int _max = lootList[selectedLootNum].count;

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
        PlayerData.Instance.myCurrency += lootList[selectNumber].price * (int)mySlider.value;
        //삭제
        lootList[selectNumber].count -= (int)mySlider.value;

        if (lootList[selectNumber].count == 0) lootList.RemoveAt(selectNumber);
        else mySlider.maxValue = lootList[selectNumber].count;

        mySlider.value = 1;

        GetLootsData(); // 이건 잘 됨

        //저장
        PlayerData.Instance.SaveData();
    }

    public void RootUpdate()
    {
        if(selectNumber >= lootList.Count)
        {
            curCount.text = "";
            sellingPrice.text = "";
        }

        else
        {
            curCount.text = ((int)mySlider.value).ToString();
            sellingPrice.text = ((int)mySlider.value * lootList[selectNumber].price).ToString();
        }

        playerMoney.text = PlayerData.Instance.myCurrency.ToString();
    }
}
