using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class SmathManager : MonoBehaviour
{
    const string rare = "rare";
    const string normal = "normal";
    const string make = "제작하기 ";
    const int materialMaxCount = 8;
    bool[] isHasMaterial = new bool[materialMaxCount/2];
    int materialCount;
    const int makePercent =9;
    TextMeshProUGUI moneyText;
    TextMeshProUGUI infoText;
    TextMeshProUGUI[] materialText;
    GameObject makeButton;
    ItemInfo curruntInfo = new ItemInfo();
    Image infoImage;
    public GameObject itemMakeListFactory;
    GameObject itemListGroup;
    public GameObject resultPanel;
    List<GameObject> itemList = new List<GameObject>();
    Dictionary<string, ItemInfo> lootList = new Dictionary<string, ItemInfo>();
   // int itemListCount =0;
    void Start()    
    {
        InfoInit();
        PlayerItemListInit();
        
        ListMake();
        WeaponListSerch();
        ArmourListSerch();
        AccListSerch();
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(PlayerData.Instance.myEquipment[1]);
        }
    }

    private void ListMake()
    {
        for (int i = 0; i < maxAcc; i++)
        {
            itemList.Add(Instantiate(itemMakeListFactory, itemListGroup.transform));
            itemList[i].SetActive(false);
        }
    }

    private void InfoInit()
    {
        //money
        moneyText = transform.Find("Coin").GetComponentInChildren<TextMeshProUGUI>();
        moneyText.text = PlayerData.Instance.myCurrency.ToString();
        //itemList
        //itemMakeListFactory = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/ItemMakeList.prefab", typeof(GameObject));
        itemListGroup = transform.Find("ItemList/ScrollRect/ListGroup").gameObject;
        //itemInfo
        infoImage = transform.Find("ItemInfo/ItemIconBG/ItemIcon").GetComponent<Image>();
        infoText = transform.Find("ItemInfo/InfoText").GetComponent<TextMeshProUGUI>();
        materialText = transform.Find("ItemInfo/Material").GetComponentsInChildren<TextMeshProUGUI>();
        makeButton = transform.Find("ItemInfo/MakeButton").gameObject;
    }

    private void PlayerItemListInit()
    {
        List<ItemInfo> _itemDB = PlayerData.Instance.myItem;
        int[] _playerItemDB = PlayerData.Instance.myEquipment;

        //test
        //ItemInfo a = new ItemInfo();
        //a.id = 79;
        //a.itemName = "수정 가루";
        //a.count = 10;
        //a.kindID = 4;
        //_itemDB.Add(a);

        if (_playerItemDB[1] != -1)
        {
            WeaponListInsert(_playerItemDB[1]);
            LootListInsert(CSVData.Instance.find(_playerItemDB[1]));
        }
        if (_playerItemDB[2] != -1)
        {
            ArmourListInsert(_playerItemDB[2]);
            LootListInsert(CSVData.Instance.find(_playerItemDB[2]));
        }
        if(_playerItemDB[3] != -1) AccListInsert(_playerItemDB[3]);


        for (int i = 0; i < _itemDB.Count; i++)
        {
            if (_itemDB[i].kindID == 1) { WeaponListInsert(_itemDB[i].id); LootListInsert(_itemDB[i]); }
            else if (_itemDB[i].kindID == 2) { ArmourListInsert(_itemDB[i].id); LootListInsert(_itemDB[i]); }
            else if (_itemDB[i].kindID == 3) { AccListInsert(_itemDB[i].id); }
            else if (_itemDB[i].kindID == 4) { LootListInsert(_itemDB[i]); }
        }
    }

    private void ListDisable(int kind)
    {
        itemList[kind].GetComponent<Button>().interactable = (itemList[kind].GetComponent<Button>().interactable )? false:true;
    }


    private void LootListInsert(ItemInfo info)
    {
        if (lootList.ContainsKey(info.itemName) == false)
            lootList.Add(info.itemName, info);
        else
            lootList[info.itemName].count += info.count;
    }

    private string LootFind(string itemName)
    {
        if (lootList.ContainsKey(itemName) == false)
            return "0";
        else
            return lootList[itemName].count.ToString();
    }
    private void MaterialTextSetting()
    {
        infoImage.sprite = Resources.Load(curruntInfo.iconPath, typeof(Sprite)) as Sprite;
        infoText.text = curruntInfo.skillIncrease + "티어 " + curruntInfo.itemName + "\n" + "공격력 " +
        curruntInfo.atk + "\n" + "방어력 " + curruntInfo.def + "\n" + curruntInfo.skill;
        makeButton.GetComponentInChildren<TextMeshProUGUI>().text = make + curruntInfo.price.ToString();


        for (int i = 0; i < materialMaxCount; i++) 
        { 
            materialText[i].text = "";
            if (i < materialMaxCount/2)
                isHasMaterial[i] = false;
        }
        materialCount = 0;

        if ((materialText[0].text = curruntInfo.ingredient1) != "")
        { MaterialText(materialText[1], LootFind(curruntInfo.ingredient1), curruntInfo.ingredientCount1.ToString()); }
        if ((materialText[2].text = curruntInfo.ingredient2) != "")
        { MaterialText(materialText[3], LootFind(curruntInfo.ingredient2), curruntInfo.ingredientCount2.ToString()); }
        if ((materialText[4].text = curruntInfo.ingredient3) != "")
        { MaterialText(materialText[5], LootFind(curruntInfo.ingredient3), curruntInfo.ingredientCount3.ToString()); }
        if ((materialText[6].text = curruntInfo.ingredient4) != "")
        { MaterialText(materialText[7], LootFind(curruntInfo.ingredient4), curruntInfo.ingredientCount4.ToString()); }

        MakeButtonActive();
    }
    private void MaterialText(TextMeshProUGUI text, string num1, string num2)
    {
        text.text = num1 + " / " + num2;
        if (int.Parse(num1) != int.Parse(num2)) { text.color = Color.red; }
        else { text.color = Color.black; isHasMaterial[materialCount] = true; }
        materialCount++;
    }

    private void MakeButtonActive()
    {
        makeButton.GetComponent<Button>().interactable = true;

        for (int i = 0; i < materialCount; i++)
        {
            if (!isHasMaterial[i]) makeButton.GetComponent<Button>().interactable = false;
        }
        if (curruntInfo.price > PlayerData.Instance.myCurrency) makeButton.GetComponent<Button>().interactable = false;
    }


    public void OnMakeButton()
    {
        resultPanel.SetActive(true);
        GameObject _success = resultPanel.transform.Find("ResultRect/Success").gameObject;
        GameObject _fail = resultPanel.transform.Find("ResultRect/Fail").gameObject;
        
        if (Random.Range(0,10) <makePercent) // 강화성공
        {
            _success.SetActive(true);
            _fail.SetActive(false);

            MaterialPlayerDataRemove(true);
        }
        else //강화실패
        {
            _success.SetActive(false);
            _fail.SetActive(true);

            MaterialPlayerDataRemove(false);
        }

        Debug.Log("start한다");
        this.Start();
    }

    private void MaterialPlayerDataRemove(bool success)
    {
        bool _isSave = false;

        EquipmentCheck(success, ref _isSave);
        ChestCheck();

        PlayerData.Instance.myCurrency -= curruntInfo.price;
        if (!_isSave && success) PlayerData.Instance.SaveChest(curruntInfo.id);
    }

    private void ChestCheck()
    {
        for (int i = 0; i < PlayerData.Instance.myItem.Count;)
        {
            if (lootList[curruntInfo.ingredient1].id == PlayerData.Instance.myItem[i].id)
            {
                PlayerData.Instance.myItem[i].count -= curruntInfo.ingredientCount1;
                if (PlayerData.Instance.myItem[i].count == 0) { PlayerData.Instance.myItem.RemoveAt(i); continue; }
            }
            else if (lootList[curruntInfo.ingredient2].id == PlayerData.Instance.myItem[i].id)
            {
                PlayerData.Instance.myItem[i].count -= curruntInfo.ingredientCount2;
                if (PlayerData.Instance.myItem[i].count == 0) { PlayerData.Instance.myItem.RemoveAt(i); continue; }
            }
            else if (lootList[curruntInfo.ingredient3].id == PlayerData.Instance.myItem[i].id)
            {
                PlayerData.Instance.myItem[i].count -= curruntInfo.ingredientCount3;
                if (PlayerData.Instance.myItem[i].count == 0) { PlayerData.Instance.myItem.RemoveAt(i); continue; }
            }
            else if (lootList[curruntInfo.ingredient4].id == PlayerData.Instance.myItem[i].id)
            {
                PlayerData.Instance.myItem[i].count -= curruntInfo.ingredientCount4;
                if (PlayerData.Instance.myItem[i].count == 0) { PlayerData.Instance.myItem.RemoveAt(i); continue; }
            }
            else
                i++;
        }
    }

    private void EquipmentCheck(bool success, ref bool _isSave)
    {
        for (int k = 0; k < PlayerData.Instance.myEquipment.Length; k++)
        {
            if (lootList.ContainsKey(curruntInfo.ingredient1)&&lootList[curruntInfo.ingredient1].id == PlayerData.Instance.myEquipment[k])
            {
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (lootList.ContainsKey(curruntInfo.ingredient2) && lootList[curruntInfo.ingredient2].id == PlayerData.Instance.myEquipment[k])
            {
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (lootList.ContainsKey(curruntInfo.ingredient3) && lootList[curruntInfo.ingredient3].id == PlayerData.Instance.myEquipment[k])
            {
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (lootList.ContainsKey(curruntInfo.ingredient4) && lootList[curruntInfo.ingredient4].id == PlayerData.Instance.myEquipment[k])
            {
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
        }
    }
}
