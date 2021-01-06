using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

class ChestEquipManager : MonoBehaviour
{
    public int[] myEquip = new int[4]; // ID

    private void Start()
    {
        LinkData();
    }

    private void LinkData()
    {
        PlayerData.Instance.LoadData_v2();

        GetWearing();
        GetEquipInven();
    }

    void GetWearing()
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

    void GetEquipInven()
    {
        GameObject _rightBg = transform.GetChild(1).gameObject;
        GameObject _slots = _rightBg.transform.GetChild(0).gameObject;

        for (int i = 0; i < _slots.transform.childCount; i++)
        {
            if ((i + 1) > PlayerData.Instance.haveEquipItem.Count)
            {
                _slots.transform.GetChild(i).GetChild(0).GetComponent<Image>().color
                    = Color.clear;

                continue;
            }

            _slots.transform.GetChild(i).GetChild(0).GetComponent<Image>().color
                    = Color.white;

            _slots.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite
                = GetPath(PlayerData.Instance.haveEquipItem[i].id);
        }
    }

    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.find(id).iconPath);
    }
}