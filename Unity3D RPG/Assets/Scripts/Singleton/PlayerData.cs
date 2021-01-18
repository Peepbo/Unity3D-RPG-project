using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class PlayerData : Singleton<PlayerData>
{
    protected PlayerData() { }
    
    //01/06 두 가지로 분류 후 나중에 save할 때 하나의 list로 합쳐서 저장
    public List<ItemInfo> haveEquipItem = new List<ItemInfo>();
    public List<ItemInfo> haveLootItem = new List<ItemInfo>();

    public int[] myEquipment = { 123, -1, -1, -1, -1 }; // 1. 무기 2. 방어구 3. 악세사리 4.보스스킬?

    //내 특성
    public int[] myAbility = new int[35];

    //내가 가지고 있는 화폐
    public int myCurrency;

    //내 성장 수치
    public int hpLv;
    public int stmLv;

    public int[] myStature = new int[2];

    public int myPotion;
    public int myCurrentPotion;

    public int nowHp; // 플레이어 현재체력
    

    public Player player;

    public ItemInfo myWeapon = new ItemInfo();
    public ItemInfo myArmor = new ItemInfo();
    public ItemInfo myAccessory = new ItemInfo();
    public ItemInfo myBossItem = new ItemInfo();
    public int currentWeapon = -1;
    public int currentArmor = -1;
    public int currentAccessory = -1;
    public int currentBossItem = -1;

    //인트로 실행 하였는지
    public bool isIntro;

    //카메라 쿼터인지 백인지
    public bool isCameraBack;
    #region INSURANCE
    public bool normalInsurance;
    public bool rareInsurance;

    public void ResetInsurance()
    {
        normalInsurance = false;
        rareInsurance = false;
    }
    #endregion

    // 마을로 돌아가는지
    public bool isReturn;
    public void LoadData_v2()
    {
        //화폐
        myCurrency = JsonData.Instance.LoadCurrency();

        //성장
        hpLv = JsonData.Instance.LoadStaturePoint()[0];
        stmLv = JsonData.Instance.LoadStaturePoint()[1];


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
        //myItem.Clear();
        List<SubItem> _item = new List<SubItem>(JsonData.Instance.LoadItem());

        haveEquipItem.Clear();
        haveLootItem.Clear();
        
        for(int i = 0; i < _item.Count; i++)
        {
            ItemInfo _info = new ItemInfo();
            _info = CSVData.Instance.find(_item[i].Id);
            _info.count = _item[i].Number;

            //장비는 장비 리스트에
            if (_info.kindID != 4) haveEquipItem.Add(_info);

            //전리품은 전리품 리스트에
            else haveLootItem.Add(_info);
        }

        //Debug.Log("플레이어 데이터 연동 완료");
    }

    public void SaveData()
    {
        List<SubItem> _subItem = new List<SubItem>();

        for(int i = 0; i < haveEquipItem.Count; i++)
        {
            SubItem _sub = new SubItem(haveEquipItem[i].id, haveEquipItem[i].count);
            _subItem.Add(_sub);
        }

        for(int i = 0; i < haveLootItem.Count; i++)
        {
            SubItem _sub = new SubItem(haveLootItem[i].id, haveLootItem[i].count);
            _subItem.Add(_sub);
        }

        int[] _equip = new int[4];
        for (int i = 0; i < 4; i++) _equip[i] = myEquipment[i + 1];

        JsonData.Instance.Save(myCurrency,new int[] { hpLv,stmLv}, _equip, myAbility, _subItem);
    }
}
