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
    //추후에 소지장비 이미지와 연동하기위해 사용
    public void GetData()
    {
        for(int i = 1; i < 5; i++)
        {
            //1. 아이템을 가지고 있지 않을 때
            if (PlayerData.Instance.myEquipment[i] == -1)
                eqData[i - 1].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
            
            else
            {
                int _itemNumber = PlayerData.Instance.myEquipment[i];

                eqData[i - 1].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
            }
        }

        //rightBg
        int num = 0;

        foreach (GameObject data in gmData)
        {
            if (num == equipList.Count)
            {
                data.transform.GetChild(0).GetComponent<Image>().color = Color.clear;
                data.GetComponent<Button>().enabled = false;
                continue;
            }

            data.transform.GetChild(0).GetComponent<Image>().sprite = GetPath(equipList[num].kindID);

            if (equipList[num].grade == "normal")
                data.transform.GetChild(0).GetComponent<Image>().color = color[0];
            else
                data.transform.GetChild(0).GetComponent<Image>().color = color[1];

            if (!data.GetComponent<Button>().enabled)
                data.GetComponent<Button>().enabled = true;

            num++;
        }
    }

    //아이템 정보 출력
    public void ChangeEquipInfo(int number)
    {
        //이 selectNumber는 추후 착용을 할 때 필요하다.
        selectNumber = number;

        List<string> _info = new List<string>();

        //이름
        _info.Add(equipList[number].itemName);

        //장비 종류에따라 다른 info
        switch (equipList[number].kindID)
        {
            case 1://무기
                _info.Add("공격력 : " + equipList[number].atk);
                _info.Add("공격 속도 : " + equipList[number].atkSpeed);
                break;
            case 2://갑옷
                _info.Add("방어력 : " + equipList[number].def);
                _info.Add("");
                break;
            case 3://악세사리
                _info.Add("기술 : " + equipList[number].skill);
                _info.Add("");
                break;
        }

        //_info에 저장된 개 수 만큼 for문 반복
        for (int i = 0; i < _info.Count; i++)
        {
            Debug.Log(popInfo0.transform.GetChild(i).name);

            popInfo0.transform.GetChild(i).GetComponent<Text>().text = _info[i];
        }
    }

    public void WearingItem()
    {
        ItemInfo _item = equipList[selectNumber];
        int _kindId = _item.kindID;

        //1. 아이템이 없을 때, 2. 아이템을 착용중 일 때

        //착용한 아이템이 없을 때
        if(PlayerData.Instance.myEquipment[_kindId] == -1)
        {
            //아이템 넘버를 장비창에 저장
            PlayerData.Instance.myEquipment[_kindId] = _item.id;


            //플레이어 데이터에서 해당 아이템을 없애준다.
            int _delIndex = PlayerData.Instance.myItem.IndexOf(_item);
            PlayerData.Instance.myItem.RemoveAt(_delIndex);

            ItemUpdate();
        }

        //착용중인 아이템이 있으면?
        else
        {
            int _saveItem = PlayerData.Instance.myEquipment[_kindId];

            PlayerData.Instance.myEquipment[_kindId] = _item.id;

            equipList[selectNumber] = CSVData.Instance.find(_saveItem);
        }

        //팝업을 끈다.
        popInfo0.SetActive(false);

        //보유한 장비 리스트를 업데이트한다.
        GetData();
    }
}