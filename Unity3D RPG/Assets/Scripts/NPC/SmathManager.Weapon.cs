using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct WeaponBaseID
{
    public int Hand_N ; 
    public int Hand_R ; 
    public int THand_N ;
    public int THand_R ;
    public int BaseWeapon;
    public int Dagger_R;
}
[System.Serializable]
public struct WeaponMaxLevel
{
    public int Hand_N;
    public int Hand_R;
    public int THand_N;
    public int THand_R;
    public int Dagger_N;
    public int Dagger_R;
}
enum WeaponKind
{
    HAND_N,HAND_R,THAND_N,THAND_R,BaseWeapon, BaseTWeapon
}
partial class SmathManager
{
    int maxWeapon;
    Text[] weaponListText;
    Dictionary<WeaponKind,ItemInfo> weaponList = new Dictionary<WeaponKind,ItemInfo>();
    const string hand = "한손검";
    const string twoHand = "대검";
    const int maxWeaponID = 21;
    //const string dagger = "단검";
    bool isWeapon =false;
    bool isTHandBase = false;
    //int baseHandCount = 0;
    int baseWeaponKind;
    public WeaponBaseID baseWeaponID;
    public WeaponMaxLevel weaponMaxLevel;
   

    public void OnWeaponButton() //무기종류버튼
    {
        ClickSound();
        isWeapon = true;
        isArmour = false;
        isAcc = false;
        WeaponListSetActive();
    }
    private void WeaponListSerch() //무기리스트중 없는 종류는 베이스 무기를 리스트에 넣어놓는다.
    {
        for(int i=0; i<maxWeapon; i++)
        {
            WeaponKind _temp = (WeaponKind)i;
            if (weaponList.ContainsKey(_temp) == false)
            {
                switch (_temp)
                {
                    case WeaponKind.HAND_N:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.Hand_N));
                        break;
                    case WeaponKind.HAND_R:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.Hand_R));
                        if (!weaponList.ContainsKey(WeaponKind.BaseWeapon))
                        { maxWeapon++; baseWeaponKind=1; } //baseWeaponKind는 베이스 무기가 필요한 종류를 나눈것
                        break;
                    case WeaponKind.THAND_N:                     
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.THand_N));
                        isTHandBase = true;
                        break;
                    case WeaponKind.THAND_R:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.THand_R));
                        if (!weaponList.ContainsKey(WeaponKind.BaseTWeapon)&&!isTHandBase)
                        { ++maxWeapon; if (maxWeapon < 6) baseWeaponKind = 3; else baseWeaponKind = 2; }
                        break;
                    case WeaponKind.BaseWeapon:
                        switch (baseWeaponKind)
                        {
                            case 1: // 1= 한손검만
                            case 2: // 2 = 둘다
                                weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.Hand_N));
                                break;
                        }
                        break;
                    case WeaponKind.BaseTWeapon:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.THand_N));
                        break;
                }
            }
        }
        if(maxWeapon ==5 && baseWeaponKind==3)
            weaponList.Add(WeaponKind.BaseTWeapon, CSVData.Instance.find(baseWeaponID.THand_N));
    }
    private void WeaponListSetting(int num) //무기리스트를 파악하여 아이템리스트창에 정보를 채운다.
    {
        WeaponKind _temp = (WeaponKind)num;
        if (num ==4 )
        {
            if (baseWeaponKind == 3)
                _temp = WeaponKind.BaseTWeapon; 
        }
        weaponListText = itemList[num].GetComponentsInChildren<Text>();
        
        ItemInfo _item = weaponList[_temp];
        weaponListText[0].text = _item.skillIncrease + "티어 "+ _item.itemName;
        weaponListText[1].text = _item.kind;
        weaponListText[2].text = _item.grade;
        switch (_temp)
        {
            case WeaponKind.HAND_N:
                if (_item.skillIncrease == weaponMaxLevel.Hand_N) ListDisable(num);
                    break;
            case WeaponKind.HAND_R:
                if (_item.skillIncrease == weaponMaxLevel.Hand_R) ListDisable(num);
                break;
            case WeaponKind.THAND_N:
                if (_item.skillIncrease == weaponMaxLevel.THand_N) ListDisable(num);
                break;
            case WeaponKind.THAND_R:
                if (_item.skillIncrease == weaponMaxLevel.THand_R) ListDisable(num);
                break;
            case WeaponKind.BaseWeapon:
                if (_item.skillIncrease == weaponMaxLevel.Dagger_N) ListDisable(num);
                break;
            //case WeaponKind.DAGGER_R:
            //    if (_item.skillIncrease == weaponMaxLevel.Dagger_R) ListDisable(num);
            //    break;
           
        }
        
    }
    private void WeaponListSetActive() //아이템리스트중 무기갯수만큼만 남겨두고 게임오브젝트를 꺼둔다.
    {

        for (int i = 0; i < maxWeapon; i++)
        {
            itemList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemList[i].SetActive(true);
            if (!itemList[i].GetComponent<Button>().interactable) ListDisable(i);

            WeaponListSetting(i);

            int _i = i;// i는 계속 변하지만 _i는 한번생성되고 사라진다. 즉 1회용으로 이벤트에 넣어주기위해 사용 (이벤트에 레퍼런스타입으로 매개변수가 전달됌)
            itemList[i].GetComponent<Button>().onClick.AddListener(delegate { OnWeaponClick(_i); });
        }
       
        if (itemList[maxWeapon].activeSelf)
        {
            for (int i = maxWeapon; i < maxAcc; i++)
            {
                itemList[i].SetActive(false);
            }
        }
    }

    private void OnWeaponClick(int num) //아이템리스트 번호에 맞는 무기리스트정보를 현재 보여줘야하는 정보로 사용한다.
    {
        if (!isWeapon) return;
        ClickSound();
        curruntInfo = weaponList[(WeaponKind)num];
        
        MaterialTextSetting();
    }

   

    private void WeaponListInsert(int id) //플레이어 아이템정보를 무기리스트에 담는다.
    {
        ItemInfo _temp = CSVData.Instance.find(id);
        ItemInfo _itemDB;
        if (id != maxWeaponID) 
        {
            _itemDB = CSVData.Instance.find(id + 1);
            if (_temp.grade != _itemDB.grade) { _itemDB = _temp; }
        }
        else
        {
            _itemDB = _temp;
        }
        
        switch (_itemDB.kind)
        {
            case hand:
                if (_itemDB.grade == normal)
                {
                    if (weaponList.ContainsKey(WeaponKind.HAND_N))
                        weaponList.Add(WeaponKind.BaseWeapon, _itemDB);
                    else
                        weaponList.Add(WeaponKind.HAND_N, _itemDB);
                }
                else weaponList.Add(WeaponKind.HAND_R, _itemDB);
                break;
            case twoHand:
                if (_itemDB.grade == normal)
                {
                    if (weaponList.ContainsKey(WeaponKind.THAND_N))
                        weaponList.Add(WeaponKind.BaseTWeapon, _itemDB);
                    else
                        weaponList.Add(WeaponKind.THAND_N, _itemDB);
                }
                else weaponList.Add(WeaponKind.THAND_R, _itemDB);
                break;
                
                //baseTHandCount++;
            //case dagger:
            //    if (_itemDB.grade == normal) {
            //        Debug.Log("대거로 진입");
            //        weaponList.Add(WeaponKind.DAGGER_R, _itemDB); 
            //    }
            //    else {
            //        Debug.Log("대거로 진입");
            //        weaponList.Add(WeaponKind.DAGGER_R, _itemDB); 
            //    }
            //    break;
        }
    }
   
}
