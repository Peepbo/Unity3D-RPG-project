using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class Murder
{
    public string monsterName;
    public bool isKill;

    public Murder(string name, bool kill)
    {
        monsterName = name;
        isKill = kill;
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

partial class JsonData
{
    #region Achieve
    public void AchieveSave(List<Achieve> achieve)
    {
        //JsonUtility.ToJson(JsonUtility.FromJson(json), true);

        //LitJson.JsonData AchieveJson = JsonMapper.ToJson(achieve);

        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(achieve.ToString());
        //string format = System.Convert.ToBase64String(bytes);

        File.WriteAllText(achievePath, JsonMapper.ToJson(achieve));
    }

    public List<Achieve> LoadAchieve()
    {
        List<Achieve> _output = new List<Achieve>();

        for (int i = 0; i < jsonData(achievePath).Count; i++)
        {
            _output.Add(new Achieve(int.Parse(jsonData(achievePath)[i]["Id"].ToString()),
                                    int.Parse(jsonData(achievePath)[i]["State"].ToString()),
                                    int.Parse(jsonData(achievePath)[i]["Number"].ToString())));
        }

        return _output;
    }
    #endregion

    #region Murder
    public void MurderSave(List<Murder> murder)
    {
        //JsonUtility.ToJson(JsonUtility.FromJson(json), true);

        //LitJson.JsonData AchieveJson = JsonMapper.ToJson(achieve);

        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(achieve.ToString());
        //string format = System.Convert.ToBase64String(bytes);

        File.WriteAllText(murderPath, JsonMapper.ToJson(murder));
    }

    public List<Murder> LoadMurder()
    {
        List<Murder> _output = new List<Murder>();

        for (int i = 0; i < jsonData(achievePath).Count; i++)
        {
            _output.Add(new Murder(
                jsonData(murderPath)[i]["monsterName"].ToString(), 
                jsonData(murderPath)[i]["isKill"].ToString() == "true"));
        }

        return _output;
    }

    public void CheckMurder(Murder[] murder)
    {
        List<Murder> _list = LoadMurder();

        for(int i = 0; i < 5; i++) // 내가 잡은것
        {
            if (murder[i].isKill == false) continue; // 잡지 못한거면 넘기고

            for(int j = 0; j < 5; j++) // 저장되어 있는것
            {
                if(murder[i].monsterName.Equals (_list[i].monsterName))
                {
                    _list[i].isKill = true;
                    break;
                }
            }
        }

        MurderSave(_list);
    }
    #endregion
}