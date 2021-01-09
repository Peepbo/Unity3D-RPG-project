using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct WeaponBaseID
{
    public int Hand_N ; 
    public int Hand_R ; 
    public int THand_N ;
    public int THand_R ;
    public int Dagger_N;
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
    HAND_N,HAND_R,THAND_N,THAND_R,DAGGER_N,DAGGER_R
}
partial class SmathManager
{
    const int maxWeapon = 4;
    TextMeshProUGUI[] weaponListText;
    Dictionary<WeaponKind,ItemInfo> weaponList = new Dictionary<WeaponKind,ItemInfo>();
    const string hand = "한손검";
    const string twoHand = "대검";
    const string dagger = "단검";
    bool isWeapon =false;
    
    public WeaponBaseID baseWeaponID;
    public WeaponMaxLevel weaponMaxLevel;
   

    public void OnWeaponButton()
    {
        isWeapon = true;
        isArmour = false;
        isAcc = false;
        WeaponListSetActive();
    }
    private void WeaponListSerch()
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
                        break;
                    case WeaponKind.THAND_N:                     
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.THand_N));
                        break;
                    case WeaponKind.THAND_R:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.THand_R));
                        break;
                    case WeaponKind.DAGGER_N:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.Dagger_N));
                        break;
                    case WeaponKind.DAGGER_R:
                        weaponList.Add((WeaponKind)i, CSVData.Instance.find(baseWeaponID.Dagger_R));
                        break;
                }
            }
           
        }
    }
    private void WeaponListSetting(int num)
    {
        weaponListText = itemList[num].GetComponentsInChildren<TextMeshProUGUI>();
        WeaponKind _temp = (WeaponKind)num;
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
            case WeaponKind.DAGGER_N:
                if (_item.skillIncrease == weaponMaxLevel.Dagger_N) ListDisable(num);
                break;
            case WeaponKind.DAGGER_R:
                if (_item.skillIncrease == weaponMaxLevel.Dagger_R) ListDisable(num);
                break;
           
        }
        
    }
    private void WeaponListSetActive()
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

    private void OnWeaponClick(int num)
    {
        if (!isWeapon) return;

        curruntInfo = weaponList[(WeaponKind)num];
        
        MaterialTextSetting();
    }

   

    private void WeaponListInsert(int id)
    {
        ItemInfo _temp = CSVData.Instance.find(id);
        ItemInfo _itemDB;
        if (id != 21)
        {
            _itemDB = CSVData.Instance.find(id + 1);
            if (_temp.grade != _itemDB.grade) { _itemDB = _temp; }
        }
        else
        {
            _itemDB = _temp;
        }
        
        Debug.Log(_itemDB.itemName);
        switch (_itemDB.kind)
        {
            case hand:
                if (_itemDB.grade == normal) 
                {
                    weaponList.Add(WeaponKind.HAND_N, _itemDB); 
                }
                else {
                    weaponList.Add(WeaponKind.HAND_R, _itemDB); 
                }
                break;
            case twoHand:
                if (_itemDB.grade == normal) 
                {
                    weaponList.Add(WeaponKind.THAND_N, _itemDB); 
                }
                else {
                    weaponList.Add(WeaponKind.THAND_R, _itemDB); 
                }
                break;
            case dagger:
                if (_itemDB.grade == normal) {
                    weaponList.Add(WeaponKind.DAGGER_N, _itemDB); 
                }
                else {
                    weaponList.Add(WeaponKind.DAGGER_R, _itemDB); 
                }
                break;
        }
    }
   
}
