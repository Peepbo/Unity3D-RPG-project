using CSVReader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CSVReader.Data("itemName")]
[System.Serializable]
public class ItemInfo
{
    public int id;
    public string kind;
    public string itemName;
    public int atk;
    public int def;
    public int price;
    public string prefabName;
}

public class ItemCSV : Singleton<ItemCSV>
{
    protected ItemCSV() { }

    private Dictionary<string, ItemInfo> dictionaryData;

    //public Dictionary<string, ItemInfo> itemDB { get { return dictionaryData; } }
    public ItemInfo find(string key) { return dictionaryData[key]; }
    
    void Awake()
    {
        //데이터 불러오기 => 파일을 열어서 데이터가 담겨있는 테이블로 변환해줌
        Table table = CSVReader.Reader.ReadCSVToTable("ItemDB");
        
        ////테이블에 있는 데이터를 배열로 변환
        //ItemInfo[] arrayData = table.TableToArray<ItemInfo>();
        ////테이블에 있는 데이터를 List로 변환
        //List<ItemInfo> listData = table.TableToList<ItemInfo>();

        //테이블에 있는 데이터를 Dictionary로 변환
        dictionaryData = table.TableToDictionary<string, ItemInfo>();

        Debug.Log("SucceededLoad");
        //Debug.Log(dictionaryData["OldSword"].itemName);
        
        System.GC.Collect();
    }

}
