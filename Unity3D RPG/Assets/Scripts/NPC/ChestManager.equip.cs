using System;
using System.Collections.Generic;
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
        for(int i = 0; i < 4; i++)
        {
            //1. 아이템을 가지고 있지 않을 때
            if (PlayerData.Instance.myEquipment[i+1] == -1)
                eqData[i].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
            
            else
            {
                int _itemNumber = PlayerData.Instance.myEquipment[i+1];

                eqData[i].transform.GetChild(0).GetComponent<Image>().sprite = GetPath(_itemNumber);
            }
        }

        //rightBg
        //Debug.Log(PlayerData.Instance.haveEquipItem.Count);

        for(int i = 0; i < gmData.Length; i++)
        {
            if (i+1 > PlayerData.Instance.haveEquipItem.Count)
            {
                gmData[i].transform.GetChild(0).GetComponent<Image>().color = Color.clear;
                gmData[i].GetComponent<Button>().enabled = false;
                continue;
            }
            
            //Debug.Log(PlayerData.Instance.haveEquipItem[i].itemName);

            gmData[i].transform.GetChild(0).GetComponent<Image>().sprite
                = GetPath(PlayerData.Instance.haveEquipItem[i].id);

            if (gmData[i].GetComponent<Button>().enabled == false)
                gmData[i].GetComponent<Button>().enabled = true;
        }
    }

    //아이템 정보 출력
    public void ChangeEquipInfo(int number)
    {
        //이 selectNumber는 추후 착용을 할 때 필요하다.
        selectNumber = number;

        List<string> _info = new List<string>();

        //이름
        _info.Add(PlayerData.Instance.haveEquipItem[number].itemName);
        //_info.Add(equipList[number].itemName);

        //장비 종류에따라 다른 info
        switch (PlayerData.Instance.haveEquipItem[number].kindID)
        {
            case 1://무기
                _info.Add("공격력 : " + PlayerData.Instance.haveEquipItem[number].atk);
                _info.Add("공격 속도 : " + PlayerData.Instance.haveEquipItem[number].atkSpeed);
                break;
            case 2://갑옷
                _info.Add("방어력 : " + PlayerData.Instance.haveEquipItem[number].def);
                _info.Add("");
                break;
            case 3://악세사리
                _info.Add("기술 : " + PlayerData.Instance.haveEquipItem[number].skill);
                _info.Add("");
                break;
        }

        //_info에 저장된 개 수 만큼 for문 반복
        for (int i = 0; i < _info.Count; i++)
        {
            //Debug.Log(popInfo0.transform.GetChild(i).name);

            popInfo0.transform.GetChild(i).GetComponent<Text>().text = _info[i];
        }
    }

    public void WearingItem()
    {
        ItemInfo _item = PlayerData.Instance.haveEquipItem[selectNumber];
        int _kindId = _item.kindID;

        //1. 아이템이 없을 때, 2. 아이템을 착용중 일 때

        //착용한 아이템이 없을 때
        if(PlayerData.Instance.myEquipment[_kindId] == -1)
        {
            //아이템 넘버를 장비창에 저장
            PlayerData.Instance.myEquipment[_kindId] = _item.id;


            //플레이어 데이터에서 해당 아이템을 없애준다.
            int _delIndex = PlayerData.Instance.haveEquipItem.IndexOf(_item);
            PlayerData.Instance.haveEquipItem.RemoveAt(_delIndex);

            ItemUpdate();
        }

        //착용중인 아이템이 있으면?
        else
        {
            //원래 내 아이템의 id를 저장하고
            int _saveId = PlayerData.Instance.myEquipment[_kindId];

            //내 무기를 변경
            PlayerData.Instance.myEquipment[_kindId] = _item.id;
            PlayerData.Instance.player.EquipStat();

            //방금 착용한 아이템이 가지고있던 인덱스를 저장해서
            int _index = PlayerData.Instance.haveEquipItem.IndexOf(_item);

            //그 자리에 아까 착용했던 아이템을 넣어준다.
            PlayerData.Instance.haveEquipItem[_index] = CSVData.Instance.find(_saveId);

            ItemUpdate();
        }

        //팝업을 끈다.
        popInfo0.SetActive(false);

        //보유한 장비 리스트를 업데이트한다.
        GetData();

        PlayerData.Instance.SaveData();
    }
}