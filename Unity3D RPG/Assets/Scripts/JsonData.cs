using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

using System.IO;
using System;

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

public class Achieve
{
    public int Id;
    public string Name;
    public int Level;
    public int State;
    public int Number;
    public string Description;

    public Achieve(int id, string name, int level, int state, int number, string description)
    {
        Id = id;
        Name = name;
        Level = level;
        State = state;
        Number = number;
        Description = description;
    }
}

[System.Serializable]
public class CharacterInfo
{
    public int Money;                                               // 돈
    public int[] StaturePoint= new int [2];                         // 성장 포인트
    public int[] Equip = new int[4];                                // 착용 장비
    public List<SubItem> Item = new List<SubItem>();                // 소지 아이템
    public int[] Characteristic = new int[35];                      // 특성

    public CharacterInfo(int money, int[] point, int[] equip, List<SubItem> item, int[] charac)
    {
        Money = money;
        StaturePoint = point;
        Equip = equip;
        Item = item;
        Characteristic = charac;
    }
}

public class JsonData : Singleton<JsonData>
{
    protected JsonData() { }
    public string path;
    const string fileName = "PlayerData.json";

    public string achievePath;
    const string fileName2 = "AchievementData.json";
   
    private void Awake()
    {
        path = PathNameSetup(fileName);

        achievePath = PathNameSetup(fileName2);
    }

    public void CheckJsonData()
    {
        if (File.Exists(path) == false)
        {
            Save(100, new int[] {0, 0}, new int[] { 0, 33, -1, -1 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<SubItem> { });
        }

        if( File.Exists(achievePath) == false)
        {
            List<Achieve> _list = new List<Achieve>();
            for(int i = 0; i < 8; i++)
            {
                _list.Add(new Achieve(i, "", 1, 0, 0, ""));
            }

            AchieveSave(_list);
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
        CharacterInfo character = new CharacterInfo(money, point, equip, item, charac);

        LitJson.JsonData ItemJson = JsonMapper.ToJson(character);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ItemJson.ToString());
        string format = System.Convert.ToBase64String(bytes);
        File.WriteAllText(path, format);
    }

    public void AchieveSave(List<Achieve> achieve)
    {
        //JsonUtility.ToJson(JsonUtility.FromJson(json), true);

        //LitJson.JsonData AchieveJson = JsonMapper.ToJson(achieve);

        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(achieve.ToString());
        //string format = System.Convert.ToBase64String(bytes);

        File.WriteAllText(achievePath, JsonMapper.ToJson(achieve));
    }

    
    public LitJson.JsonData jsonData 
    { 
        get
        {
            string Jsonstring = File.ReadAllText(path);
            byte[] bytes = System.Convert.FromBase64String(Jsonstring);
            string reformat = System.Text.Encoding.UTF8.GetString(bytes);
            LitJson.JsonData _data = JsonMapper.ToObject(reformat);

            return _data;
        } 
    }

    public LitJson.JsonData AchieveJsonData
    {
        get
        {
            string Jsonstring = File.ReadAllText(achievePath);
            //byte[] bytes = System.Convert.FromBase64String(Jsonstring);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes);
            LitJson.JsonData _data = JsonMapper.ToObject(Jsonstring);

            return _data;
        }
    }

    public List<Achieve> LoadAchieve()
    {
        List<Achieve> _output = new List<Achieve>();

        for(int i = 0; i < AchieveJsonData.Count; i++)
        {
            _output.Add(new Achieve(int.Parse(AchieveJsonData[i]["Id"].ToString()),
                                    AchieveJsonData[i]["Name"].ToString(),
                                    int.Parse(AchieveJsonData[i]["Level"].ToString()),
                                    int.Parse(AchieveJsonData[i]["State"].ToString()),
                                    int.Parse(AchieveJsonData[i]["Number"].ToString()),
                                    AchieveJsonData[i]["Description"].ToString()));
        }
        /*
    public int Id;
    public string name;
    public int Level;
    public int State;
    public int Number;
    public string description;
         */
        return _output;
    }


    public int LoadCurrency()
    {
        return int.Parse(jsonData["Money"].ToString());
    }

    public int LoadStaturePoint()
    {
        return int.Parse(jsonData["StaturePoint"].ToString());
    }

    public List<int> LoadEquip()
    {
        List<int> _output = new List<int>();

        for (int i = 0; i < jsonData["Equip"].Count; i++)
        {
            _output.Add(int.Parse(jsonData["Equip"][i].ToString()));
        }

        return _output;
    }

    public List<SubItem> LoadItem()
    {
        List<SubItem> _output = new List<SubItem>();

        for (int i = 0; i < jsonData["Item"].Count; i++)
        {
            _output.Add(new SubItem(
                int.Parse(jsonData["Item"][i][0].ToString()),
                int.Parse(jsonData["Item"][i][1].ToString())
                ));
        }
        return _output;
    }

    public List<int> LoadCharacteristic()
    {
        List<int> _output = new List<int>();

        for (int i = 0; i < jsonData["Characteristic"].Count; i++)
        {
            _output.Add(int.Parse(jsonData["Characteristic"][i].ToString()));
        }

        return _output;
    }
}
