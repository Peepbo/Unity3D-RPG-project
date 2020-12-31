﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

using System.IO;

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
    public int Money;                                               // 돈
    public int StaturePoint;                                        // 성장 포인트
    public int[] Equip = new int[4];                                // 착용 장비
    public List<SubItem> Item = new List<SubItem>();                        // 소지 아이템
    public int[] Characteristic = new int[35];                      // 특성

    public CharacterInfo(int money, int point, int[] equip, List<SubItem> item, int[] charac)
    {
        Money = money;
        StaturePoint = point;
        Equip = equip;
        Item = item;
        Characteristic = charac;
    }
}

public class JsonTest : Singleton<JsonTest>
{
    protected JsonTest() { }

    public CharacterInfo character;

    private void Start()
    {
        character = new CharacterInfo(0, 8,new int[] {-1,-1,-1,-1 } ,
            new List<SubItem> { new SubItem(1, 2), new SubItem(1, 2) },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(LoadCurrency());
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(LoadStaturePoint());
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(LoadEquip());
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(LoadItem());
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log(LoadCharacteristic());
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            character.Money += 50;
            Save();
        }
    }

    public void Save()
    {
        //File.Delete(Application.dataPath + "/Resources/PlayerData.json");

        Debug.Log("저장하기");

        JsonData ItemJson = JsonMapper.ToJson(character);
        File.WriteAllText(Application.dataPath
            + "/Resources/PlayerData.json"
            , ItemJson.ToString());
    }

    public void Load()
    {
        //Debug.Log("불러오기");

        //string Jsonstring = File.ReadAllText(Application.dataPath
        //    + "/Resources/PlayerData.json");
        //JsonData _data = JsonMapper.ToObject(Jsonstring);

        //for (int i = 0; i < _data["Equip"].Count; i++)
        //{
        //    Debug.Log(_data["Equip"][i].ToString());
        //}
        //Debug.Log(int.Parse(_data["Money"].ToString()));
    }

    public int LoadCurrency()
    {
        Debug.Log("소지금 불러오기");

        string Jsonstring = File.ReadAllText(Application.dataPath
            + "/Resources/PlayerData.json");
        JsonData _data = JsonMapper.ToObject(Jsonstring);

        return int.Parse(_data["Money"].ToString());
    }

    public int LoadStaturePoint()
    {
        Debug.Log("성장 포인트 불러오기");

        string Jsonstring = File.ReadAllText(Application.dataPath
            + "/Resources/PlayerData.json");
        JsonData _data = JsonMapper.ToObject(Jsonstring);

        return int.Parse(_data["StaturePoint"].ToString());
    }

    public List<int> LoadEquip()
    {
        Debug.Log("장비 불러오기");

        string Jsonstring = File.ReadAllText(Application.dataPath
            + "/Resources/PlayerData.json");
        JsonData _data = JsonMapper.ToObject(Jsonstring);

        List<int> _output = new List<int>();

        for(int i = 0; i < _data["Equip"].Count; i++)
        {
            _output.Add(int.Parse(_data["Equip"][i].ToString()));
        }

        return _output;
    }

    public List<SubItem> LoadItem()
    {
        Debug.Log("소지품 불러오기");

        string Jsonstring = File.ReadAllText(Application.dataPath
            + "/Resources/PlayerData.json");
        JsonData _data = JsonMapper.ToObject(Jsonstring);

        List<SubItem> _output = new List<SubItem>();

        for (int i = 0; i < _data["Item"].Count; i++)
        {
            _output.Add(new SubItem(
                int.Parse( _data["Item"][i][0].ToString()),
                int.Parse( _data["Item"][i][1].ToString())
                ));
        }

        //Debug.Log(_output.Count);

        //for(int i = 0; i < _output.Count; i++)
        //{
        //    Debug.Log(_output[i].Id);
        //    Debug.Log(_output[i].Number);
        //}

        return _output;
    }

    public List<int> LoadCharacteristic()
    {
        Debug.Log("특성 불러오기");

        string Jsonstring = File.ReadAllText(Application.dataPath
            + "/Resources/PlayerData.json");
        JsonData _data = JsonMapper.ToObject(Jsonstring);

        List<int> _output = new List<int>();

        for (int i = 0; i < _data["Characteristic"].Count; i++)
        {
            _output.Add(int.Parse(_data["Characteristic"][i].ToString()));
        }

        return _output;
    }
}
