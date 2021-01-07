using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum ACHIEVE_STATE
{
    YET,
    PROGRESS,
    DONE,
    ACHIEVE
}

public enum ACHIEVE_TYPE
{
    WITHSTEP,
    WITHOUTSTEP
}

public class AchieveData
{
    public ACHIEVE_STATE state;
    public int id;
    public int count;
    public string name;
    public string content;
    public int reward;
}

public class Achievements : MonoBehaviour
{
    public GameObject achieveSlots;                                 // 업적 슬롯
    public GameObject popInfo;
    public AchieveData[] data = new AchieveData[7];                 // 업적 갯수
    public List<AchieveInfo> AchieveList = new List<AchieveInfo>(); // 업적 관련

    public GameObject[] achievementData;
    public GameObject achievements;


    Color[] color = {
        new Color(117f / 255, 1, 105f / 255, 1),    // YET - 초록
        new Color(1, 1, 180f / 255, 1),             // PROGRESS - 노랑
        new Color(1, 192f / 255, 188f / 255, 1),    // DONE - 빨강
        new Color(177f / 255, 189f / 255, 1, 1),    // ACHIEVE - 파랑
    };

    private void Start()
    {
        SetAchieveData();

        JsonData.Instance.CheckJsonData();

        List<Achieve> _list = new List<Achieve>();

        _list = JsonData.Instance.LoadAchieve();

        for (int i = 0; i < _list.Count; i++)
        {
            Debug.Log(_list[i].Number);
        }

        JsonData.Instance.AchieveSave(_list);
    }

    void Update()
    {
        ShowAchieveData();
    }

    public void SetAchieveData()
    {
        AchieveList = (CSVData.Instance.GetAchieveCSV());

        //for(int i = 0; i < AchieveList.Count; i++)
        //{
        //    Debug.Log(AchieveList[i].name);
        //}
    }

    public void Content()
    {
        for (int i = 0; i < AchieveList.Count; i++)
        {
            if (AchieveList[i].state == (int)ACHIEVE_STATE.DONE)
            {
                PlayerData.Instance.myCurrency += AchieveList[i].reward; // 아이템을 얻는다
                AchieveList[i].state = (int)ACHIEVE_STATE.ACHIEVE;       // 업적 상태를 바꿔준다
            }
        }
    }

    public void ShowAchieveData()
    {

        for (int i = 0; i < AchieveList.Count; i++) // 0 = 업적아이콘 / 1 = 업적이름 / 2 = 업적설명 / 3 = 보상받기
        {
            //popInfo.transform.GetChild(i - 1).GetChild(0).GetComponent<Image>().sprite = 아이콘 이름;
            popInfo.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = AchieveList[i].name.ToString();
            popInfo.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = AchieveList[i].descrition.ToString();
            popInfo.transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = AchieveList[i].reward.ToString();

            if (AchieveList[i].state == (int)ACHIEVE_STATE.YET)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[0];
            }
            else if (AchieveList[i].state == (int)ACHIEVE_STATE.PROGRESS)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[1];
            }
            else if (AchieveList[i].state == (int)ACHIEVE_STATE.DONE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[2];
            }
            else if (AchieveList[i].state == (int)ACHIEVE_STATE.ACHIEVE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[3];
            }
        }
    }

    public void GetAchieveData()
    {

    }
}