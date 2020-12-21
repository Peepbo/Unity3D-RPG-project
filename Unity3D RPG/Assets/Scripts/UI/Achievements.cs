using CSVReader;
using CSVWrite;
using CSVSimpleReader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CSVReader.Data("achievement")]

public enum ACHIEVE_STATE
{
    YET,
    PROGRESS,
    DONE,
    ACHIEVE
}

public class AchieveData
{
    public ACHIEVE_STATE state;
    public int id;
    public string name;
    public string content;
    public int reward;
}

public class Achievements : MonoBehaviour
{
    public AchieveData[] data = new AchieveData[7];

    public void Content(int order)
    {
        if(data[order].state == ACHIEVE_STATE.ACHIEVE)
        {
            //아이템을 얻는다
            PlayerData.Instance.myCurrency += data[order].reward;
            //

            data[order].state = ACHIEVE_STATE.DONE;
        }
    }

    private void Update()
    {
    }
}

public class AchieveDataReader
{
    Dictionary<string, AchieveData> contentsData;
    List<string> achieveName = new List<string>();
    List<string> achieveContents = new List<string>();
    List<string> achieveReward = new List<string>();

    public List<string> achieveNameLoad { get { return achieveNameLoad; } }
    public List<string> achieveContentsLoad { get { return achieveContentsLoad; } }
    public List<string> achieveRewardLoad { get { return achieveRewardLoad; } }
    public AchieveData find(string key) { return contentsData[key]; }

    Table achieveTable = CSVReader.Reader.ReadCSVToTable("AchievementDB");

}