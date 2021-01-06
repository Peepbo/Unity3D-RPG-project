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
    public int[] myEquipment = { 123, -1, -1, -1, -1 }; // 1. 무기 2. 방어구 3. 악세사리 4.보스스킬?
    //0으로 초기화 안 한 이유는 아이템 넘버가 0부터 존재해서..

    //내 특성

    public int[] myAbility = new int[35];

    //내가 가지고 있는 화폐
    public int myCurrency;

    //내 성장 수치
    public int hpLv;
    public int stmLv;

    public int[] myStature = new int[2];

    public int myPotion;

    public Player player;

    public ItemInfo myWeapon = new ItemInfo();
    public ItemInfo myArmor = new ItemInfo();
    public ItemInfo myAccessory = new ItemInfo();
    public ItemInfo myBossItem = new ItemInfo();
    public int currentWeapon = -1;
    public int currentArmor = -1;
    public int currentAccessory = -1;
    public int currentBossItem = -1;

    public void LoadData_v2()
    {
        //화폐
        myCurrency = JsonData.Instance.LoadCurrency();

        //성장
        for(int i = 0; i < 2; i++)
        {
            myStature[i] = JsonData.Instance.LoadStaturePoint()[i];
        }

        //장비
        List<int> _equip = new List<int>(JsonData.Instance.LoadEquip());
        //currentWeapon = _equip[0];
        //currentArmor = _equip[1];
        //currentAccessory = _equip[2];
        //currentBossItem = _equip[3];
        for (int i = 0; i < _equip.Count; i++)
        {
            myEquipment[i + 1] = _equip[i];
        }

        //특성
        List<int> _ability = new List<int>(JsonData.Instance.LoadCharacteristic());
        for (int i = 0; i < _ability.Count; i++)
        {
            myAbility[i] = _ability[i];
        }

        //아이템
        myItem.Clear();
        List<SubItem> _item = new List<SubItem>(JsonData.Instance.LoadItem());
        for(int i = 0; i < _item.Count; i++)
        {
            ItemInfo _Info = CSVData.Instance.find(_item[i].Id);
            _Info.count = _item[i].Number;

            myItem.Add(_Info);
        }

        //Debug.Log("플레이어 데이터 연동 완료");
    }

    public void SaveData()
    {
        List<SubItem> _subItem = new List<SubItem>();
        for(int i = 0; i < myItem.Count; i++)
        {
            SubItem _sub = new SubItem(myItem[i].id, myItem[i].count);
            Debug.Log(myItem[i].id);
            _subItem.Add(_sub);
        }

        int[] _equip = new int[4];
        for (int i = 0; i < 4; i++)
        {
            _equip[i] = myEquipment[i + 1];

            Debug.Log(_equip[i]);

        }

        JsonData.Instance.Save(myCurrency,new int[] { hpLv,stmLv}, _equip, myAbility, _subItem);
    }
}
