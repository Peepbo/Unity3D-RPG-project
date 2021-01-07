using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

class ChestEquipManager : MonoBehaviour
{
    private int[] myEquip = new int[4]; // ID

    [Header("rightPanel")]
    public Transform rightSlots;
    public Transform selectPanel;
    [Space]
    public int selectNumber;

    private void OnEnable()
    {
        LinkData();
    }

    private void LinkData()
    {
        Debug.Log("Data Load and Link");
        PlayerData.Instance.LoadData_v2();

        GetWearing();
        GetEquipInven();
    }

    private void GetWearing()
    {
        for (int i = 0; i < 4; i++)
        {
            myEquip[i] = PlayerData.Instance.myEquipment[i + 1];
        }

        GameObject _leftBg = transform.GetChild(0).gameObject;
        GameObject[] _equipObj = new GameObject[4];

        for(int i = 0; i < _leftBg.transform.childCount; i++)
        {
            _equipObj[i] = _leftBg.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < 4; i++)
        {
            if(myEquip[i] == -1)
            {
                _equipObj[i].transform.GetChild(0).GetComponent<Image>().color 
                    = Color.clear;
            }

            else
            {
                _equipObj[i].transform.GetChild(0).GetComponent<Image>().color 
                    = Color.white;

                _equipObj[i].transform.GetChild(0).GetComponent<Image>().sprite 
                    = GetPath(myEquip[i]);
            }
        }
    }

    private void GetEquipInven()
    {
        //GameObject _rightBg = transform.GetChild(1).gameObject;
        //GameObject _slots = _rightBg.transform.GetChild(0).gameObject;

        for (int i = 0; i < rightSlots.childCount; i++)
        {
            if ((i + 1) > PlayerData.Instance.haveEquipItem.Count)
            {
                rightSlots.GetChild(i).GetChild(0).GetComponent<Image>().color
                    = Color.clear;
                rightSlots.GetChild(i).GetComponent<Button>().enabled = false;

                continue;
            }

            rightSlots.GetChild(i).GetChild(0).GetComponent<Image>().color
                    = Color.white;
            rightSlots.GetChild(i).GetComponent<Button>().enabled = true;

            rightSlots.GetChild(i).GetChild(0).GetComponent<Image>().sprite
                = GetPath(PlayerData.Instance.haveEquipItem[i].id);
        }
    }

    private Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }

    private void ChangeInfo(int id)
    {
        ItemInfo _item = CSVData.Instance.find(id);

        List<string> _infos = new List<string>();
        _infos.Add(_item.itemName);

        switch (_item.kindID)
        {
            case 1://무기
                _infos.Add("공격력 : " + _item.atk);
                _infos.Add("공격 속도 : " + _item.atkSpeed);
                break;
            case 2://갑옷
                _infos.Add("방어력 : " + _item.def);
                _infos.Add("");
                break;
            case 3://장신구
                _infos.Add("기술 : " + _item.skill);
                _infos.Add("");
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            selectPanel.GetChild(i).GetComponent<Text>().text = _infos[i];
        }
    }

    #region BUTTON ACTIONS
    public void SelectNumber(int number)
    {
        selectNumber = number;

        int _selectId = PlayerData.Instance.haveEquipItem[selectNumber].id;

        ChangeInfo(_selectId);
    }

    public void ItemWear()
    {
        ItemInfo _item = PlayerData.Instance.haveEquipItem[selectNumber];
        int _id = _item.id;
        int _kind = _item.kindID;

        int _index = PlayerData.Instance.haveEquipItem.IndexOf(_item);

        if(myEquip[_kind-1] == -1) //착용중인 아이템이 없을 때
        {
            PlayerData.Instance.myEquipment[_kind] = _id;
            PlayerData.Instance.haveEquipItem.RemoveAt(_index);
        }

        else
        {
            int _saveId = PlayerData.Instance.myEquipment[_kind];
            ItemInfo _saveItem = CSVData.Instance.find(_saveId);

            PlayerData.Instance.myEquipment[_kind] = _id;
            PlayerData.Instance.haveEquipItem[_index] = _saveItem;
        }

        PlayerData.Instance.player.EquipStat();

        PlayerData.Instance.SaveData();

        LinkData();
    }
    #endregion
}