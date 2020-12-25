using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    public List<ItemInfo> myItem = new List<ItemInfo>();
    //public List<ItemInfo> EquipmentItem = new List<ItemInfo>();
    //public Dictionary<int, int> myItem = new Dictionary<int, int>();

    public int myCurrency;

    public int[,] info = new int[4, 4]
    { { 0,0,0,0 }, { 0,0,0,0 }, { 0,0,0,0 }, { 0,0,0,0 } };

    public void SaveChest(int itemNumber)
    {
        ItemInfo _item = CSVData.Instance.find(itemNumber);
        //없으면 ?
        if (myItem.Contains(_item) == false) myItem.Add(_item);
        //if (myItem.ContainsKey(itemNumber) == false) myItem[itemNumber] = 1;

        //있으면?
        else
        {
            int _index = myItem.IndexOf(_item);
            myItem[_index].count++;
        }
        //else myItem[itemNumber] += 1;

        //SaveData();
    }

    public void ChangeStat(string statName, int index)
    {
        if (index > 3) return;

        int _statNumber = -1;

        string[] _arr = { "atk", "def", "spc", "abl" };
        for (int i = 0; i < _arr.Length; i++)
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
        ItemInfo _item = null;

        int _itemNumber = 0;

        for (int i = 0; i < CSVData.Instance.playerRootLoad.Count; i++)
        {
            //0,2,4,6.. (아이템 고유 넘버)

            if (i % 2 == 0)
            {
                //아이템 번호
                _itemNumber = int.Parse(CSVData.Instance.playerRootLoad[i]);

                _item = CSVData.Instance.find(_itemNumber);
            }

            else
            {
                _item.count = int.Parse(CSVData.Instance.playerRootLoad[i]);
                myItem.Add(_item);

                //Debug.Log(_item.itemName);
                //장비든 전리품이든 중복 아이템이 있을 수 있으니
                //나중에 ui에 표시할 때 장비는 갯수만큼 한칸한칸에 보여주고
                //전리품은 한칸에 갯수를 표시하는 식으로 연동한다.
            }
        }

        //ItemInfo _item = null;
        //for(int i = 0; i < CSVData.Instance.playerRootLoad.Count; i++)
        //{
        //    if (i % 2 == 0)
        //        _item = CSVData.Instance.find(CSVData.Instance.playerRootLoad[i]);
        //    else
        //    {
        //        if (_item.id != 4) _item.count = 1;
        //        else _item.count = int.Parse(CSVData.Instance.playerRootLoad[i]);

        //        myItem.Add(_item);
        //    }
        //}

        //List<string> list = CSVData.Instance.playerAbilityLoad;

        //for(int i = 0; i < list.Count; i++)
        //{
        //    info[i / 4, i % 4] = int.Parse(list[i]);
        //}

        //myCurrency = int.Parse(CSVData.Instance.playerItemLoad[0]);
    }

    public void SaveData()
    {
        //List<string> _ability = new List<string>();
        //for (int i = 0; i < 4; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        _ability.Add(info[i, j].ToString());
        //    }
        //}

        //CSVData.Instance.PlayerSave(myCurrency, "0", "33", "44",
        //    myItem, _ability, "Resources/playerStateDB.csv");
    }
}
