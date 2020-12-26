using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    //내가 소지한 아이템 (착용 안한 장비 + 전리품)
    public List<ItemInfo> myItem = new List<ItemInfo>();

    //내가 착용한 아이템(아이템 넘버로 저장함)
    // {더미, 무기, 갑옷, 악세사리}  
    public int[] myEquipment = { 123, -1, -1, -1, -1 };
    //0으로 초기화 안 한 이유는 아이템 넘버가 0부터 존재해서..

    //내가 가지고 있는 화폐
    public int myCurrency;

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
    }

    public void SaveData()
    {

        //CSVData.Instance.PlayerSave(myCurrency, "0", "33", "44",
        //    myItem, _ability, "Resources/playerStateDB.csv");
    }
}
