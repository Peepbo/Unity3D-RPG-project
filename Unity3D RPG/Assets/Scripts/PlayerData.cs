using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    public List<ItemInfo> myItem = new List<ItemInfo>();
    public int myCurrency;

    public int[,] info = new int[4, 4]
    { { 0,0,0,0 }, { 0,0,0,0 }, { 0,0,0,0 }, { 0,0,0,0 } };

    public void SaveChest(ItemInfo item)
    {
        myItem.Add(item);
    }

    public void ChangeStat(string statName, int index)
    {
        if (index > 3) return;

        int _statNumber = -1;

        string[] _arr = { "atk", "def", "spc", "abl" };
        for (int i = 0; i < _arr.Length; i++ )
        {
            if (statName == _arr[i]) _statNumber = i;
        }

        //stat의 이름을 못찾았을 때
        if (_statNumber < 0) return;

        info[_statNumber, index]++;

        Debug.Log(statName + index + "의 능력치가 " + info[_statNumber, index] + "가 되었습니다");
    }

    public void LoadData()
    {
        //item.Add(ItemCSV.Instance.find("OldSword"));
        //myItem에 csv data 받아오기
        //csv에서 stat 받아오기
    }

    public void SaveData()
    {

    }
}
