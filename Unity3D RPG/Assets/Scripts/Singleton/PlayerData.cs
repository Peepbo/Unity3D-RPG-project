using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }

    //내가 소지한 아이템 (착용 안한 장비 + 전리품)
    public List<ItemInfo> myItem = new List<ItemInfo>();

    //내가 착용한 아이템(아이템 넘버로 저장함)
    // {더미, 무기, 갑옷, 악세사리}  
    public int[] myEquipment = { 123, -1, -1, -1, -1 };
    //0으로 초기화 안 한 이유는 아이템 넘버가 0부터 존재해서..

    //내 특성
    public int[] myAbility = new int[35];

    //내가 가지고 있는 화폐
    public int myCurrency;

    //내 성장 수치
    public int myStature;

    public int myPotion;

    public void LoadData_v2()
    {
        //화폐
        myCurrency = JsonTest.Instance.LoadCurrency();

        //Debug.Log("화폐 로드 완료 : " + myCurrency);

        //성장
        myStature = JsonTest.Instance.LoadStaturePoint();

        //Debug.Log("성장 로드 완료 : " + myStature);

        //장비
        List<int> _equip = new List<int>(JsonTest.Instance.LoadEquip());
        for (int i = 0; i < _equip.Count; i++)
        {
            myEquipment[i + 1] = _equip[i];
            //Debug.Log(myEquipment[i + 1]);
        }

        //Debug.Log("장비 로드 완료");

        //특성
        List<int> _ability = new List<int>(JsonTest.Instance.LoadCharacteristic());
        for (int i = 0; i < _ability.Count; i++)
        {
            myAbility[i] = _ability[i];
            //Debug.Log(myAbility[i]);
        }

        //Debug.Log("스텟 로드 완료");

        //아이템
        List<SubItem> _item = new List<SubItem>(JsonTest.Instance.LoadItem());
        for(int i = 0; i < _item.Count; i++)
        {
            ItemInfo _Info = CSVData.Instance.find(_item[i].Id);
            _Info.count = _item[i].Number;

            myItem.Add(_Info);

            //Debug.Log(_Info.id);
        }

        Debug.Log("플레이어 데이터 연동 완료");
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

        for (int i = 0; i < CSVData.Instance.playerAbilityLoad.Count; i++)
        {
            myAbility[i] = int.Parse(CSVData.Instance.playerAbilityLoad[i]);

            //Debug.Log(myAbility[i]);
        }

        myCurrency = int.Parse(CSVData.Instance.playerItemLoad[0]);
    }

    public void SaveData()
    {
        List<SubItem> _subItem = new List<SubItem>();
        for(int i = 0; i < myItem.Count; i++)
        {
            SubItem _sub = new SubItem(myItem[i].id, myItem[i].count);
            _subItem.Add(_sub);
        }

        int[] _equip = new int[4];
        for (int i = 0; i < 4; i++)
            _equip[i] = myEquipment[i + 1];

        JsonTest.Instance.Save(myCurrency, myStature, _equip, myAbility, _subItem);
        //List<int> _equip = new List<int>();

        //for(int i = 1; i < 4; i++)
        //    _equip.Add(myEquipment[i]);

        //List<ItemInfo> _storage = new List<ItemInfo>();

        //for (int i = 0; i < myItem.Count; i++)
        //{
        //    ItemInfo _item = CSVData.Instance.find(myItem[i].id);

        //    int _itemCount = myItem[myItem.IndexOf(_item)].count;

        //    if (_itemCount == 0)
        //    {
        //        myItem.RemoveAt(myItem.IndexOf(_item));
        //        continue;
        //    }

        //    _item.count = _itemCount;

        //    _storage.Add(_item);
        //}

        //List<string> _ability = new List<string>();

        //Debug.Log(myAbility[0].ToString());
        //for (int i = 0; i < 35; i++)
        //{
        //    _ability.Add(myAbility[i].ToString());
        //}

        //CSVData.Instance.PlayerSave(myCurrency, _equip, _storage, _ability, "Resources/playerStateDB.csv");
    }
}
