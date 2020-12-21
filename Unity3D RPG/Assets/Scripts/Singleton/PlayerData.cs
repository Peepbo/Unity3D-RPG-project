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
        SaveData();
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

        //Debug.Log(statName + index + "의 능력치가 " + info[_statNumber, index] + "가 되었습니다");
    }

    public void LoadData()
    {
        //Debug.Log(CSVData.Instance.playerRootLoad.Count);

        ItemInfo _item = null;
        for(int i = 0; i < CSVData.Instance.playerRootLoad.Count; i++)
        {
            if (i % 2 == 0)
                _item = CSVData.Instance.find(CSVData.Instance.playerRootLoad[i]);
            else
            {
                if (_item.id != 4) _item.count = 1;
                else _item.count = int.Parse(CSVData.Instance.playerRootLoad[i]);

                myItem.Add(_item);
            }
        }
       
        List<string> list = CSVData.Instance.playerAbilityLoad;

        for(int i = 0; i < list.Count; i++)
        {
            info[i / 4, i % 4] = int.Parse(list[i]);
        }

        myCurrency = int.Parse(CSVData.Instance.playerItemLoad[0]);
    }

    public void SaveData()
    {
        List<string> _ability = new List<string>();
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                _ability.Add(info[i, j].ToString());
            }
        }

        CSVData.Instance.PlayerSave(myCurrency, "OldSword", "OldArmour", "OldNeck",
            myItem, _ability, "Resources/playerStateDB.csv");
    }
}
