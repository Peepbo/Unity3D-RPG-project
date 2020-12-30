using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum ArmourKind
{
    N, R
}
[System.Serializable]
public struct ArmourBaseID
{
    public int normal;
    public int rare;
}
[System.Serializable]
public struct ArmourMaxLevel
{
    public int normal;
    public int rare;
    
}
partial class SmathManager
{
    //List<ItemInfo> armourList = new List<ItemInfo>();
    Dictionary<ArmourKind, ItemInfo> armourList = new Dictionary<ArmourKind, ItemInfo>();
    const int maxArmour = 2;
    public ArmourBaseID baseArmourID;
    public ArmourMaxLevel armourMaxLevel;
    TextMeshProUGUI[] armourListText;

    public void OnArmourButton()
    {
        ArmourListSetActive();
        
        
    }

    private void ArmourListSetActive()
    {
        for (int i = 0; i < maxArmour; i++)
        {
            itemList[i].SetActive(true);
            if (!itemList[i].GetComponent<Button>().interactable) ListDisable(i);
            ArmourListSetting(i);
        }
        if (itemList[maxArmour].activeSelf)
        {
            for (int i = maxArmour; i < maxAcc; i++)
            {
                itemList[i].SetActive(false);
            }
        }
    }

    private void ArmourListSetting(int num)
    {
        armourListText = itemList[num].GetComponentsInChildren<TextMeshProUGUI>();
        ArmourKind _temp = (ArmourKind)num;
        ItemInfo _item = armourList[_temp];
        armourListText[0].text = _item.skillIncrease + "티어 " + _item.itemName;
        armourListText[1].text = _item.kind;
        armourListText[2].text = _item.grade;
        
        switch (_temp)
        {
            case ArmourKind.N:
                if (_item.skillIncrease == armourMaxLevel.normal) ListDisable(num);
                break;
            case ArmourKind.R:
                if (_item.skillIncrease == armourMaxLevel.rare) ListDisable(num);
                break;
            

        }
    }
  
    private void ArmourListInsert(int id)
    {
        ItemInfo _temp = CSVData.Instance.find(id);
        ItemInfo _itemDB = CSVData.Instance.find(id + 1);
        if (_temp.grade != _itemDB.grade) { _itemDB = _temp; }


        if (_itemDB.grade == normal)
        {
            armourList.Add(ArmourKind.N, _itemDB);
        }
        else
        {
            armourList.Add(ArmourKind.R, _itemDB);
        }

    }

    private void ArmourListSerch()
    {
        for (int i = 0; i < maxArmour; i++)
        {
            ArmourKind _temp = (ArmourKind)i;
            if (armourList.ContainsKey(_temp) == false)
            {
                switch (_temp)
                {
                    case ArmourKind.N:
                        armourList.Add(_temp, CSVData.Instance.find(baseArmourID.normal));
                        break;
                    case ArmourKind.R:
                        armourList.Add(_temp, CSVData.Instance.find(baseArmourID.rare));
                        break;
                }
            }

        }
    }

}
