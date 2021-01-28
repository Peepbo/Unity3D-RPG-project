using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class SmathManager : MonoBehaviour
{
    //const string rare = "rare";
    const string normal = "normal";
    const string make = "제작하기 ";
    const int materialMaxCount = 8;
    const int makePercent =9;
    bool[] isHasMaterial = new bool[materialMaxCount/2];
    int materialCount;

    Text moneyText;
    Text infoText;
    Text[] materialText;
    Text percentText;
    ItemInfo curruntInfo = new ItemInfo();
    Image infoImage;
    GameObject makeButton;
    GameObject itemListGroup;
    public GameObject itemMakeListFactory;
    public GameObject resultPanel;
    List<GameObject> itemList = new List<GameObject>();
    Dictionary<string, ItemInfo> lootList = new Dictionary<string, ItemInfo>();

    private void Awake()
    {
        InfoInit();
        ListMake();
    }
    void Start()    
    {
        PlayerItemListInit();
        WeaponListSerch();
        ArmourListSerch();
        AccListSerch();
        
    }
 
    private void ListMake()
    {
        for (int i = 0; i < maxAcc; i++) itemList.Add(Instantiate(itemMakeListFactory, itemListGroup.transform));
    }

    private void InfoInit()
    {
        //money
        moneyText = transform.Find("CoinText").GetComponent<Text>();
        
        //itemList
        itemListGroup = transform.Find("ItemList/ScrollRect/ListGroup").gameObject;
        //itemInfo
        infoImage = transform.Find("ItemInfo/ItemIconBG/ItemIcon").GetComponent<Image>();
        infoText = transform.Find("ItemInfo/InfoText").GetComponent<Text>();
        materialText = transform.Find("ItemInfo/Material").GetComponentsInChildren<Text>();
        makeButton = transform.Find("ItemInfo/MakeButton").gameObject;
        percentText = transform.Find("ItemInfo/PercentText").GetComponent<Text>();
        percentText.text = "제작 확률 : " + makePercent.ToString() + "0%";
        //percentText.color = Color.blue;
    }

    private void PlayerItemListInit()
    {
        //NPC List Clear
        moneyText.text = PlayerData.Instance.myCurrency.ToString();
        for (int i = 0; i < maxAcc; i++) itemList[i].SetActive(false);
        weaponList.Clear();
        armourList.Clear();
        accList.Clear();
        accCheck.Clear();
        lootList.Clear();
        maxWeapon = 4;
        maxArmour = 2;
        isTHandBase = false;
         
        //플레이어 정보 리스트에 저장
        List<ItemInfo> _EquipDB = PlayerData.Instance.haveEquipItem; //창고아이템 (장비)
        List<ItemInfo> _LootDB = PlayerData.Instance.haveLootItem; //창고아이템 (전리품)
        
        int[] _playerItemDB = PlayerData.Instance.myEquipment; //착용아이템

        
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


        for (int i = 0; i < _EquipDB.Count; i++)
        {
            if (_EquipDB[i].kindID == 1) { WeaponListInsert(_EquipDB[i].id); LootListInsert(_EquipDB[i]); }
            else if (_EquipDB[i].kindID == 2) { ArmourListInsert(_EquipDB[i].id); LootListInsert(_EquipDB[i]); }
            else if (_EquipDB[i].kindID == 3) { AccListInsert(_EquipDB[i].id); }
        }
        for (int i = 0; i < _LootDB.Count; i++)
        {
            LootListInsert(_LootDB[i]); 
        }
    }

    private void ListDisable(int kind) //아이템제작 리스트 활성화 / 비활성화
    {
        itemList[kind].GetComponent<Button>().interactable = 
            (itemList[kind].GetComponent<Button>().interactable )? false:true;
    }


    private void LootListInsert(ItemInfo info) //플레이어의 전리품들 한곳으로 모으기위한 루트리스트에 등록
    {
        if (lootList.ContainsKey(info.itemName) == false)
            lootList.Add(info.itemName, info);
        else
            lootList[info.itemName].count += info.count;
    }

    private string LootFind(string itemName) //플레이어가 갖고있는 전리품 갯수 확인
    {
        if (lootList.ContainsKey(itemName) == false)
            return "0";
        else
            return lootList[itemName].count.ToString();
    }

    private void MaterialTextSetting() //제작아이템의 대한 정보와 필요한 재료에 대한 정보
    {
        infoImage.sprite = Resources.Load(curruntInfo.iconPath, typeof(Sprite)) as Sprite;
        infoText.text = curruntInfo.skillIncrease + "티어 " + curruntInfo.itemName + "\n" + "공격력 " +
        curruntInfo.atk + "\n" + "방어력 " + curruntInfo.def + "\n" + curruntInfo.skill;
        makeButton.GetComponentInChildren<Text>().text = make + curruntInfo.price.ToString();


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

    private void MaterialText(Text text, string num1, string num2) //필요한 재료의 갯수를 처리하는 곳
    {
        text.text = num1 + " / " + num2;
        if (int.Parse(num1) < int.Parse(num2)) { text.color = Color.red; }
        else { text.color = Color.white; isHasMaterial[materialCount] = true; }
        materialCount++;
    }

    private void MakeButtonActive() //필요한 재료를 확인하고 다 갖춰졌으면 활성화
    {
        makeButton.GetComponent<Button>().interactable = true;

        for (int i = 0; i < materialCount; i++)
        {
            if (!isHasMaterial[i]) makeButton.GetComponent<Button>().interactable = false;
        }
        if (curruntInfo.price > PlayerData.Instance.myCurrency) makeButton.GetComponent<Button>().interactable = false;
    }

    public void OnMakeButton() //장비제작 버튼을 눌렀을 경우 로직
    {
        ClickSound();
        resultPanel.SetActive(true);
        GameObject _success = resultPanel.transform.Find("ResultRect/Success").gameObject;
        GameObject _fail = resultPanel.transform.Find("ResultRect/Fail").gameObject;
        
        if (Random.Range(0,10) <makePercent) // 제작성공
        {
            _success.SetActive(true);
            _fail.SetActive(false);
            SoundManager.Instance.SFXPlay2D("UI_Success");
            MaterialPlayerDataRemove(true);
        }
        else //제작실패
        {
            _success.SetActive(false);
            _fail.SetActive(true);
            SoundManager.Instance.SFXPlay2D("UI_Fail");
            MaterialPlayerDataRemove(false);
        }
        makeButton.GetComponent<Button>().interactable = false;
        this.Start();
    }

    private void MaterialPlayerDataRemove(bool success) //장비제작 후처리(아이템재료삭제)
    {
        bool _isSave = false;
        bool[] _check = new bool[materialCount];
        for (int i = 0; i < materialCount; i++)
            _check[i] = false;
        EquipmentCheck(success, ref _isSave, ref _check);
        ChestCheck(ref _check);

        PlayerData.Instance.myCurrency -= curruntInfo.price;
        if (!_isSave && success) PlayerData.Instance.SaveChest(curruntInfo.id);


    }
    //창고아이템에 아이템재료가 있는지 확인 후 삭제
    private void ChestCheck(ref bool[] check)
    {
        for(int i = 0; i < PlayerData.Instance.haveEquipItem.Count;)
        {
            if(!check[0]&&lootList[curruntInfo.ingredient1].id == PlayerData.Instance.haveEquipItem[i].id)
            {
                check[0] = true;
                PlayerData.Instance.haveEquipItem[i].count -= curruntInfo.ingredientCount1;
                if (PlayerData.Instance.haveEquipItem[i].count == 0) { PlayerData.Instance.haveEquipItem.RemoveAt(i); continue; }
            }
            else if ( materialCount > 1 && !check[1] && lootList[curruntInfo.ingredient2].id == PlayerData.Instance.haveEquipItem[i].id)
            {
                check[1] = true;
                PlayerData.Instance.haveEquipItem[i].count -= curruntInfo.ingredientCount2;
                if (PlayerData.Instance.haveEquipItem[i].count == 0) { PlayerData.Instance.haveEquipItem.RemoveAt(i); continue; }
            }
            else if (materialCount > 2 && !check[2] && lootList[curruntInfo.ingredient3].id == PlayerData.Instance.haveEquipItem[i].id)
            {
                check[2] = true;
                PlayerData.Instance.haveEquipItem[i].count -= curruntInfo.ingredientCount3;
                if (PlayerData.Instance.haveEquipItem[i].count == 0) { PlayerData.Instance.haveEquipItem.RemoveAt(i); continue; }
            }
            else if ( materialCount > 3 && !check[3] && lootList[curruntInfo.ingredient4].id == PlayerData.Instance.haveEquipItem[i].id)
            {
                check[3] = true;
                PlayerData.Instance.haveEquipItem[i].count -= curruntInfo.ingredientCount4;
                if (PlayerData.Instance.haveEquipItem[i].count == 0) { PlayerData.Instance.haveEquipItem.RemoveAt(i); continue; }
            }
        
            i++;
        }

        for (int i = 0; i < PlayerData.Instance.haveLootItem.Count;)
        {
            if (!check[0] && lootList[curruntInfo.ingredient1].id == PlayerData.Instance.haveLootItem[i].id)
            {
                check[0] = true;
                PlayerData.Instance.haveLootItem[i].count -= curruntInfo.ingredientCount1;
                if (PlayerData.Instance.haveLootItem[i].count == 0) { PlayerData.Instance.haveLootItem.RemoveAt(i); continue; }
            }
            else if (materialCount > 1 && !check[1] && lootList[curruntInfo.ingredient2].id == PlayerData.Instance.haveLootItem[i].id)
            {
                check[1] = true;
                PlayerData.Instance.haveLootItem[i].count -= curruntInfo.ingredientCount2;
                if (PlayerData.Instance.haveLootItem[i].count == 0) { PlayerData.Instance.haveLootItem.RemoveAt(i); continue; }
            }
            else if (materialCount > 2 && !check[2] && lootList[curruntInfo.ingredient3].id == PlayerData.Instance.haveLootItem[i].id)
            {
                check[2] = true;
                PlayerData.Instance.haveLootItem[i].count -= curruntInfo.ingredientCount3;
                if (PlayerData.Instance.haveLootItem[i].count == 0) { PlayerData.Instance.haveLootItem.RemoveAt(i); continue; }
            }
            else if (materialCount > 3 && !check[3] && lootList[curruntInfo.ingredient4].id == PlayerData.Instance.haveLootItem[i].id)
            {
                check[3] = true;
                PlayerData.Instance.haveLootItem[i].count -= curruntInfo.ingredientCount4;
                if (PlayerData.Instance.haveLootItem[i].count == 0) { PlayerData.Instance.haveLootItem.RemoveAt(i); continue; }
            }
           
            i++;
        }
    }
    //장비아이템에 아이템재료가 있는지 확인 후 삭제
    private void EquipmentCheck(bool success, ref bool _isSave, ref bool[] check)
    {
        for (int k = 0; k < PlayerData.Instance.myEquipment.Length; k++)
        {
            if (lootList.ContainsKey(curruntInfo.ingredient1)&&
                    lootList[curruntInfo.ingredient1].id == PlayerData.Instance.myEquipment[k])
            {
                check[0] = true;
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true;}
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (materialCount > 1 && !check[1] && lootList.ContainsKey(curruntInfo.ingredient2) && 
                    lootList[curruntInfo.ingredient2].id == PlayerData.Instance.myEquipment[k])
            {
                check[1] = true;
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (materialCount > 2 && !check[2] && lootList.ContainsKey(curruntInfo.ingredient3) && 
                    lootList[curruntInfo.ingredient3].id == PlayerData.Instance.myEquipment[k])
            {
                check[2] = true;
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
            else if (materialCount > 3 && !check[3] && lootList.ContainsKey(curruntInfo.ingredient4) && 
                    lootList[curruntInfo.ingredient4].id == PlayerData.Instance.myEquipment[k])
            {
                check[3] = true;
                if (success)
                { PlayerData.Instance.myEquipment[k] = curruntInfo.id; _isSave = true; }
                else
                { PlayerData.Instance.myEquipment[k] = -1; }
            }
        }

        if(_isSave)
        {
            //여기에 추가해주세요.
            PlayerData.Instance.player.EquipStat();

            PlayerData.Instance.SaveData();
        }
    }


    public void ClickSound()
    {
        SoundManager.Instance.SFXPlay2D("UI_Click",0.6f);
    }

    #region BUTTON ACTIONS
    public void QuitButton() { gameObject.SetActive(false); }

    public void CanTouch()
    {
        UiManager0.Instance.PanelOpen = false;
    }

    #endregion
}
