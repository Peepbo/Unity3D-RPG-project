using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum ArmourKind
{
    N, R,Base
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
    int maxArmour;
    public ArmourBaseID baseArmourID;
    public ArmourMaxLevel armourMaxLevel;
    Text[] armourListText;
    bool isArmour = false;
    public void OnArmourButton() //방어종류 버튼
    {
        ClickSound();
        isWeapon = false;
        isArmour = true;
        isAcc = false;
        ArmourListSetActive();
        
        
    }

    private void ArmourListSetActive() //아이템리스트중 방어갯수만큼만 남겨두고 게임오브젝트를 꺼둔다.
    {
        for (int i = 0; i < maxArmour; i++)
        {
            itemList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemList[i].SetActive(true);
            if (!itemList[i].GetComponent<Button>().interactable) ListDisable(i);
            
            ArmourListSetting(i);

            int _i = i;
            itemList[i].GetComponent<Button>().onClick.AddListener(delegate { OnArmourClick(_i); });
        }
        if (itemList[maxArmour].activeSelf)
        {
            for (int i = maxArmour; i < maxAcc; i++)
            {
                itemList[i].SetActive(false);
            }
        }
    }

    private void ArmourListSetting(int num) //아이템리스트에 방어구관련 정보를 띄운다.
    {
        armourListText = itemList[num].GetComponentsInChildren<Text>();
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
  
    private void ArmourListInsert(int id) // 플레이어 장비를 방어구리스트에 담는다.
    {
        ItemInfo _temp = CSVData.Instance.find(id);
        ItemInfo _itemDB = CSVData.Instance.find(id + 1);
        if (_temp.grade != _itemDB.grade) { _itemDB = _temp; }


        if (_itemDB.grade == normal)
        {
            if (armourList.ContainsKey(ArmourKind.N))
                armourList.Add(ArmourKind.Base, _itemDB);
            else
                armourList.Add(ArmourKind.N, _itemDB);
        }
        else
        {
            armourList.Add(ArmourKind.R, _itemDB);
        }

    }

    private void ArmourListSerch() //방어구리스트를 파악후 없는 종류에 아머는 베이스장비로 채운다.
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
                        if(!armourList.ContainsKey(ArmourKind.Base))
                            maxArmour++;
                        break;
                    case ArmourKind.Base:
                        armourList.Add(_temp, CSVData.Instance.find(baseArmourID.normal));
                        break;
                }
            }

        }
    }


    private void OnArmourClick(int num) // 아이템리스트 번호와 맞는 방어구리스트 정보를 현재 보여줘야할 아이템정보로 교체.
    {
        if (!isArmour) return;
        ClickSound();
        curruntInfo = armourList[(ArmourKind)num];

        MaterialTextSetting();
    }

}
