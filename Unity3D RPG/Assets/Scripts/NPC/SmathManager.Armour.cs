using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum ArmourKind
//{
//    N,R
//}
[System.Serializable]
public struct ArmourBaseID
{
    public int normal;
    public int rare;
}
partial class SmathManager
{
    List<ItemInfo> armourList = new List<ItemInfo>();
    const int maxArmour = 2;
    ArmourBaseID baseArmourID;

    public void OnArmourButton()
    {
        ArmourListSetActive();
        
        
    }

    private void ArmourListSetActive()
    {
        if (!itemList[0].activeSelf)
        {
            for (int i = 0; i < maxArmour; i++)
            {
                itemList[i].SetActive(true);
                ArmourListSetting();
            }
        }
        else
        {
            for (int i = maxArmour; i < maxAcc; i++)
            {
                itemList[i].SetActive(false);
            }
        }
    }

    private void ArmourListSetting()
    {
        throw new NotImplementedException();
    }

    private void ArmourListInsert(int id)
    {
        //ItemInfo _temp = CSVData.Instance.find(id);
        //ItemInfo _itemDB = CSVData.Instance.find(id + 1);
        //bool _isMax = false;
        //if (_temp.kind != _itemDB.kind) { _itemDB = _temp; }
        //
        //
        //if (_itemDB.grade == normal)
        //{
        //    if (_isMax) { ListDisable(WeaponKind.HAND_N); }
        //    weaponList.Add(WeaponKind.HAND_N, _itemDB);
        //}
        //else
        //{
        //    if (_isMax) { ListDisable(WeaponKind.HAND_R); }
        //    weaponList.Add(WeaponKind.HAND_R, _itemDB);
        //}
        
    }

}
