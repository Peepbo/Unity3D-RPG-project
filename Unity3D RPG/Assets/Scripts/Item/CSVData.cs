using CSVReader;
//using CSVWrite;
using CSVSimpleReader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CSVReader.Data("id")]
public class ItemInfo
{
    public int id;
    public int kindID;
    public string kind;
    public string itemName;
    public string grade;
    public int count;
    public int atk;
    public float atkSpeed;
    public int def;
    public string skill;
    public int skillIncrease;
    public string ingredient1;
    public int ingredientCount1;
    public string ingredient2;
    public int ingredientCount2;
    public string ingredient3;
    public int ingredientCount3;
    public string ingredient4;
    public int ingredientCount4;
    public int price;
    public string descript;
    public float dropRate;
    public string prefabName;
    public string iconPath;
}

public class AchieveInfo
{
    public int id;
    public string name;
    public int state;
    public int number;
    public string descrition;
    public int reward;
    public string icon;

    //id,name,level,state,number,description,reward

    public AchieveInfo(int ID, string NAME, int STATE, int NUMBER, string DESCRIPTION, int REWARD, string ICON)
    {
        id = ID;
        name = NAME;
        state = STATE;
        number = NUMBER;
        descrition = DESCRIPTION;
        reward = REWARD;
        icon = ICON;
    }
}

public class CSVData : Singleton<CSVData>
{
    protected CSVData() { }

    Dictionary<int, ItemInfo> dictionaryData;
    Dictionary<int, AchieveInfo> achieveDictionary;

    public List<string> achieveListData;
    /*
     * playerItemData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",0,1);
     * playerRootData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",1,2);
     * playerAbility = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",2,3);
     */

    public ItemInfo find(int key) 
    {
        ItemInfo _item = new ItemInfo();
        //_item = dictionaryData[key];
        //_item.count = 1;
        _item.id = dictionaryData[key].id;
        _item.kindID = dictionaryData[key].kindID;
        _item.kind = dictionaryData[key].kind;
        _item.itemName = dictionaryData[key].itemName;
        _item.grade = dictionaryData[key].grade;
        _item.count = dictionaryData[key].count;
        _item.atk = dictionaryData[key].atk;
        _item.atkSpeed = dictionaryData[key].atkSpeed;
        _item.def = dictionaryData[key].def;
        _item.skill = dictionaryData[key].skill;
        _item.skillIncrease = dictionaryData[key].skillIncrease;
        _item.ingredient1 = dictionaryData[key].ingredient1;
        _item.ingredientCount1 = dictionaryData[key].ingredientCount1;
        _item.ingredient2 = dictionaryData[key].ingredient2;
        _item.ingredientCount2 = dictionaryData[key].ingredientCount2;
        _item.ingredient3 = dictionaryData[key].ingredient3;
        _item.ingredientCount3 = dictionaryData[key].ingredientCount3;
        _item.ingredient4 = dictionaryData[key].ingredient4;
        _item.ingredientCount4 = dictionaryData[key].ingredientCount4;
        _item.price = dictionaryData[key].price;
        _item.descript = dictionaryData[key].descript;
        _item.dropRate = dictionaryData[key].dropRate;
        _item.prefabName = dictionaryData[key].prefabName;
        _item.iconPath = dictionaryData[key].iconPath;

        //Debug.Log(_item.count);
        return _item;
    }
    public AchieveInfo findAchieve(int num) { return achieveDictionary[num]; }

    void Awake()
    {
        //데이터 불러오기 => 파일을 열어서 데이터가 담겨있는 테이블로 변환해줌
        Table _itemTable = CSVReader.Reader.ReadCSVToTable("ItemDB");

        Table _achieveTable = CSVReader.Reader.ReadCSVToTable("AchievementDB");

        achieveListData = CSVSimpleReader.CSVSimpleReader.Reader("AchievementDB", 1, 21);

        ////테이블에 있는 데이터를 배열로 변환
        //ItemInfo[] arrayData = table.TableToArray<ItemInfo>();     
        ////테이블에 있는 데이터를 List로 변환
        //List<ItemInfo> listData = playerStateTable.TableToList<ItemInfo>();

        //테이블에 있는 데이터를 Dictionary로 변환
        dictionaryData = _itemTable.TableToDictionary<int, ItemInfo>();

        //achieveDictionaryData = _achieveTable.TableToDictionary<int, AchieveInfo>();

        //Debug.Log(achieveDictionaryData.Count);

        Debug.Log("SucceededLoad");

        System.GC.Collect();
    }

    public List<AchieveInfo> GetAchieveCSV() 
    {
        List<AchieveInfo> _data = new List<AchieveInfo>();

        for(int i = 0; i < achieveListData.Count; i+=7)
        {
            _data.Add(new AchieveInfo ( int.Parse( achieveListData[i]),
                                                   achieveListData[i + 1],
                                        int.Parse( achieveListData[i + 2]),
                                        int.Parse( achieveListData[i + 3]),
                                                   achieveListData[i + 4],
                                        int.Parse (achieveListData[i + 5]),
                                                   achieveListData[i + 6] ));
        }

        return _data;
    }
    //public void PlayerSave(int money, List<int> equip, 
    //                             List<ItemInfo> storage, List<string> ability, string filePath)
    //{
    //    CSVWrite.Write.PlayerSave(money, equip, storage, ability, "Assets/" + filePath);
    //}

    
}
