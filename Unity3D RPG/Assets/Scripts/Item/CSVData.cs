using CSVReader;
using CSVWrite;
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

public class CSVData : Singleton<CSVData>
{
    protected CSVData() { }

    Dictionary<int, ItemInfo> dictionaryData;
    List<string> playerItemData = new List<string>();
    List<string> playerRootData = new List<string>();
    List<string> playerAbility = new List<string>();
    public List<string> playerItemLoad { get { return playerItemData; } }
    public List<string> playerAbilityLoad { get { return playerAbility; } }
    public List<string> playerRootLoad { get { return playerRootData; } }
    

    public ItemInfo find(int key) { return dictionaryData[key]; }

    void Awake()
    {
        //데이터 불러오기 => 파일을 열어서 데이터가 담겨있는 테이블로 변환해줌
        Table _itemTable = CSVReader.Reader.ReadCSVToTable("ItemDB");
        playerItemData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",0,1);
        playerRootData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",1,2);
        playerAbility = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",2,3);
        ////테이블에 있는 데이터를 배열로 변환
        //ItemInfo[] arrayData = table.TableToArray<ItemInfo>();     
        ////테이블에 있는 데이터를 List로 변환
        //List<ItemInfo> listData = playerStateTable.TableToList<ItemInfo>();

        //테이블에 있는 데이터를 Dictionary로 변환
        dictionaryData = _itemTable.TableToDictionary<int, ItemInfo>();

        Debug.Log("SucceededLoad");

        System.GC.Collect();
    }
    public void PlayerSave(int money, List<int> equip, 
                                 List<ItemInfo> storage, List<string> ability, string filePath)
    {
        CSVWrite.Write.PlayerSave(money, equip, storage, ability, "Assets/" + filePath);
    }

    
}
