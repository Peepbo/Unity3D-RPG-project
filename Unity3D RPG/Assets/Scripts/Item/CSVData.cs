using CSVReader;
using CSVWrite;
using CSVSimpleReader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CSVReader.Data("itemName")]
public class ItemInfo
{
    public int id;
    public string kind;
    public string itemName;
    public int atk;
    public int def;
    public int price;
    public int count;
    public string prefabName;
}

public class CSVData : Singleton<CSVData>
{
    protected CSVData() { }

    Dictionary<string, ItemInfo> dictionaryData;
    List<string> playerItemData = new List<string>();
    List<string> playerRootData = new List<string>();
    List<string> playerAbility = new List<string>();
    public List<string> playerItemLoad { get { return playerItemData; } }
    public List<string> playerAbilityLoad { get { return playerAbility; } }
    public List<string> playerRootLoad { get { return playerRootData; } }
    

    public ItemInfo find(string key) { return dictionaryData[key]; }

    void Awake()
    {
        //데이터 불러오기 => 파일을 열어서 데이터가 담겨있는 테이블로 변환해줌
        Table itemTable = CSVReader.Reader.ReadCSVToTable("ItemDB");
        playerItemData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",0,1);
        playerAbility = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",1,2);
        playerRootData = CSVSimpleReader.CSVSimpleReader.Reader("playerStateDB",2,3);
        ////테이블에 있는 데이터를 배열로 변환
        //ItemInfo[] arrayData = table.TableToArray<ItemInfo>();     
        ////테이블에 있는 데이터를 List로 변환
        //List<ItemInfo> listData = playerStateTable.TableToList<ItemInfo>();

        //테이블에 있는 데이터를 Dictionary로 변환
        dictionaryData = itemTable.TableToDictionary<string, ItemInfo>();

        Debug.Log("SucceededLoad");
        //for(int i=0; i<playerRootData.Count;i++)
        //Debug.Log(playerRootData[i]);
        //Debug.Log(dictionaryData["OldSword"].itemName);

        System.GC.Collect();
    }
    public void PlayerSave(int money, string weaponEquip, string armourEquip, string acceEquip, 
                                 List<ItemInfo> storage, List<string> ability, string filePath)
    {
        CSVWrite.Write.PlayerSave(money, weaponEquip, armourEquip, acceEquip, storage, ability, "Assets/" + filePath);
    }

    
}
