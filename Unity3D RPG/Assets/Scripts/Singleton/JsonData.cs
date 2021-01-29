using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class SubItem
{
    public int Id;
    public int Number;

    public SubItem(int id, int number)
    {
        Id = id;
        Number = number;
    }
}

[System.Serializable]
public class CharacterInfo
{
    public int   Money;                                             // 돈
    public int[] StaturePoint = new int[2];                         // 성장 포인트
    public int[] Equip = new int[4];                                // 착용 장비
    public int[] Characteristic = new int[35];                      // 특성
    public List<SubItem> Item = new List<SubItem>();                // 소지 아이템

    public CharacterInfo(int money, int[] point, int[] equip, int[] charac, List<SubItem> item)
    {
        Money = money;
        StaturePoint = point;
        Equip = equip;
        Characteristic = charac;
        Item = item;
    }
}

public partial class JsonData : Singleton<JsonData>
{
    protected JsonData() { }
    public string userPath;
    const string userFileName = "PlayerData.json";

    public string achievePath;
    const string achieveFileName = "AchievementData.json";

    public string murderPath;
    const string murderFileName = "MurderData.json";

    private void Awake()
    {
        userPath = PathNameSetup(userFileName);
        achievePath = PathNameSetup(achieveFileName);
        murderPath = PathNameSetup(murderFileName);
    }

    public void CheckJsonData()
    {
        if (File.Exists(userPath) == false)
        {
            Save(
                0, 
                new int[] { 0, 0}, 
                new int[] { 0, 33, -1, -1 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<SubItem> { }
                );
        }

        if (File.Exists(achievePath) == false)
        {
            List<Achieve> _list = new List<Achieve>();
            for(int i = 0; i < CSVData.Instance.GetAchieveCSV().Count; i++)
            {
                _list.Add(new Achieve(i, 0, 0));
            }

            AchieveSave(_list);
        }

        if (File.Exists(murderPath) == false)
        {
            List<Murder> _list = new List<Murder>();
            string[] _name = new string[5] { "Goblin", "OBGoblin", "Shaman", "Golem", "Chieftain" };
            for(int i = 0; i < 5; i++)
            {
                _list.Add(new Murder(_name[i], 0));
            }

            MurderSave(_list);
        }
    }

    public string PathNameSetup(string fileName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string _path = Application.persistentDataPath;
            return Path.Combine(_path, fileName);
        }
        else
        {
            string _path = Application.dataPath;
            return Path.Combine(_path, "Resources", fileName);
        }
    }

    public void Save(int money, int[] point, int[] equip, int[] charac, List<SubItem> item)
    {
        CharacterInfo character = new CharacterInfo(money, point, equip, charac, item);

        File.WriteAllText(userPath, JsonMapper.ToJson(character));
    }

    
    public LitJson.JsonData jsonData(string path) 
    {
        string Jsonstring = File.ReadAllText(path);

        LitJson.JsonData _data = JsonMapper.ToObject(Jsonstring);
        return _data;
    }

    public int LoadCurrency()
    {
        return int.Parse(jsonData(userPath)["Money"].ToString());
    }

    public List<int> LoadStaturePoint()
    {
        List<int> _output = new List<int>();

        for(int i = 0; i < 2; i++)
        {
            _output.Add(int.Parse(jsonData(userPath)["StaturePoint"][i].ToString()));
        }

        return _output;
    }

    public List<int> LoadEquip()
    {
        List<int> _output = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            _output.Add(int.Parse(jsonData(userPath)["Equip"][i].ToString()));
        }

        return _output;
    }

    public List<SubItem> LoadItem()
    {
        List<SubItem> _output = new List<SubItem>();

        int _max = jsonData(userPath)["Item"].Count;
        for (int i = 0; i < _max; i++)
        {
            _output.Add(new SubItem(
                int.Parse(jsonData(userPath)["Item"][i][0].ToString()),
                int.Parse(jsonData(userPath)["Item"][i][1].ToString())
                ));
        }
        return _output;
    }

    public List<int> LoadCharacteristic()
    {
        List<int> _output = new List<int>();

        int _max = jsonData(userPath)["Characteristic"].Count;
        for (int i = 0; i < _max; i++)
        {
            _output.Add(int.Parse(jsonData(userPath)["Characteristic"][i].ToString()));
        }

        return _output;
    }
}
