﻿using System.Collections.Generic;
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

public class Achieve
{
    public int Id;
    public int State;
    public int Number;

    public Achieve(int id, int state, int number)
    {
        Id = id;
        State = state;
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
            Save(
                0, 
                new int[] { 0, 0}, 
                new int[] { 0, 33, -1, -1 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<SubItem> { }
                );
        }

        if( File.Exists(achievePath) == false)
        {
            List<Achieve> _list = new List<Achieve>();
            for(int i = 0; i < CSVData.Instance.GetAchieveCSV().Count; i++)
            {
                _list.Add(new Achieve(i, 0, 0));
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
        CharacterInfo character = new CharacterInfo(money, point, equip, charac, item);

        //LitJson.JsonData ItemJson = JsonMapper.ToJson(character);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ItemJson.ToString());
        //string format = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(path, format);
        File.WriteAllText(path, JsonMapper.ToJson(character));
    }

    public void AchieveSave(List<Achieve> achieve)
    {
        /*
        ItemInfo _item = list.Find(x => (x.id == item.id));

        if (_item == null) list.Add(item);

        else
        {
            int _index = list.IndexOf(_item);

            list[_index].count += item.count;
        }
        */

        //List<Achieve> _old = LoadAchieve();

        //int _order = 0;
        //for(int i = 0; _order < achieve.Count; i++)
        //{
        //    if (achieve[_order].Id == _old[i].Id)
        //    {
        //        _old[i].Number += achieve[_order].Number;

        //        _order++;
        //    }
        //}

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
            //byte[] bytes = System.Convert.FromBase64String(Jsonstring);
            //string reformat = System.Text.Encoding.UTF8.GetString(bytes);
            //LitJson.JsonData _data = JsonMapper.ToObject(reformat);
            LitJson.JsonData _data = JsonMapper.ToObject(Jsonstring);
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
            //LitJson.JsonData _data = JsonMapper.ToObject(reformat);
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
                                    int.Parse(AchieveJsonData[i]["State"].ToString()),
                                    int.Parse(AchieveJsonData[i]["Number"].ToString())));
        }

        return _output;
    }


    public int LoadCurrency()
    {
        return int.Parse(jsonData["Money"].ToString());
    }

    public List<int> LoadStaturePoint()
    {
        List<int> _output = new List<int>();

        for(int i = 0; i < 2; i++)
        {
            _output.Add(int.Parse(jsonData["StaturePoint"][i].ToString()));
        }

        return _output;
    }

    public List<int> LoadEquip()
    {
        List<int> _output = new List<int>();

        for (int i = 0; i < 4; i++)
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
