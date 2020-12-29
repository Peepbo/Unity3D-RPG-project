//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;

public partial class SmathManager : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    GameObject itemMakeListFactory;
    GameObject itemListGroup;
    List<GameObject> itemList = new List<GameObject>();
   // int itemListCount =0;
    const string rare = "rare";
    const string normal = "normal";
    // Start is called before the first frame update
    void Start()    
    {
        PlayerMoneyInit();
        PlayerItemListInit();
        itemMakeListFactory = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/ItemMakeList.prefab", typeof(GameObject));
        itemListGroup = transform.Find("ItemList/ScrollRect/ListGroup").gameObject;
        ListMake();
        WeaponListSerch();
        
    }

    private void ListMake()
    {
        for (int i = 0; i < maxAcc; i++)
        {
            itemList.Add(Instantiate(itemMakeListFactory, itemListGroup.transform));
            itemList[i].SetActive(false);
        }
    }

    private void PlayerMoneyInit()
    {
        moneyText = transform.Find("Coin").GetComponentInChildren<TextMeshProUGUI>();
        moneyText.text = PlayerData.Instance.myCurrency.ToString();
    }

    private void PlayerItemListInit()
    {
        List<ItemInfo> _itemDB = PlayerData.Instance.myItem;
        int[] _playerItemDB = PlayerData.Instance.myEquipment;

        WeaponListInsert(_playerItemDB[1]);

        armourList.Add(CSVData.Instance.find(PlayerData.Instance.myEquipment[2]));
        accList.Add(CSVData.Instance.find(PlayerData.Instance.myEquipment[3]));
        for (int i = 0; i < _itemDB.Count; i++)
        {
            if (_itemDB[i].kindID == 1) { WeaponListInsert(_itemDB[i].id); }
            else if (_itemDB[i].kindID == 2) { armourList.Add(_itemDB[i]); }
            else if (_itemDB[i].kindID == 3) { accList.Add(_itemDB[i]); }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    
}
