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
    public GameObject popInfo;                                      // 업적 정보 띄우기

    private List<AchieveInfo> AchieveList = new List<AchieveInfo>(); // 업적 관련(CSV)  ... <only LOAD>
    private List<Achieve> achieveJsonList = new List<Achieve>();     // 업적 관련(Json) ... <SAVE, LOAD>

    Color[] color = {
        new Color(117f / 255, 1, 105f / 255, 1),    // YET - 초록
        new Color(1, 1, 180f / 255, 1),             // PROGRESS - 노랑
        new Color(1, 192f / 255, 188f / 255, 1),    // DONE - 빨강
        new Color(177f / 255, 189f / 255, 1, 1),    // ACHIEVE - 파랑
    };

    private void OnEnable()
    {
        AchieveList = (CSVData.Instance.GetAchieveCSV());

        JsonData.Instance.CheckJsonData();
        achieveJsonList = JsonData.Instance.LoadAchieve();

        for (int i = 0; i < AchieveList.Count; i++)
            popInfo.transform.GetChild(i).GetChild(3).GetComponent<Button>().enabled = false;

        SaveJson();
    }

    void SaveJson()
    {
        Content();
        JsonData.Instance.AchieveSave(achieveJsonList);
    }


    void Update()
    {
        ShowAchieveData();
    }

    public void Content()
    {
        //Debug.LogError(achieveJsonList.Count);

        for (int i = 0; i < AchieveList.Count; i++)
        {
            switch (achieveJsonList[i].State)
            {
                case 0://yet
                    if(achieveJsonList[i].Number > 0)
                        achieveJsonList[i].State = (int)ACHIEVE_STATE.PROGRESS;

                    if (achieveJsonList[i].Number >= AchieveList[i].number)
                    {
                        achieveJsonList[i].State = (int)ACHIEVE_STATE.DONE;
                        popInfo.transform.GetChild(i).GetChild(3).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 1://progress
                    if(achieveJsonList[i].Number >= AchieveList[i].number)
                    {
                        achieveJsonList[i].State = (int)ACHIEVE_STATE.DONE;
                        popInfo.transform.GetChild(i).GetChild(3).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 2://done
                    popInfo.transform.GetChild(i).GetChild(3).GetComponent<Button>().enabled = true;
                    break;
                case 3://achieve
                    popInfo.transform.GetChild(i).GetChild(3).GetComponent<Button>().enabled = false;
                    break;
            }
        }
    }

    public void GetReward(int num)
    {
        PlayerData.Instance.myCurrency += AchieveList[num].reward; // 재화를 얻는다
        achieveJsonList[num].State = (int)ACHIEVE_STATE.ACHIEVE;   // 업적 상태를 바꿔준다

        SaveJson();
    }

    Sprite GetPath(int id)
    {
        return Resources.Load<Sprite>(CSVData.Instance.findAchieve(id).icon);
    }

    public void ShowAchieveData()
    {
        for (int i = 0; i < AchieveList.Count; i++) // 0 = 업적아이콘 / 1 = 업적이름 / 2 = 업적설명 / 3 = 보상받기
        {
            //popInfo.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GetPath(AchieveList[i].id);
            popInfo.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = AchieveList[i].name.ToString();
            popInfo.transform.GetChild(i).GetChild(2).GetComponent<Text>().text = AchieveList[i].descrition.ToString();
            popInfo.transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<Text>().text = AchieveList[i].reward.ToString();

            if (achieveJsonList[i].State == (int)ACHIEVE_STATE.YET)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[0];
            }
            else if (achieveJsonList[i].State == (int)ACHIEVE_STATE.PROGRESS)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[1];
            }
            else if (achieveJsonList[i].State == (int)ACHIEVE_STATE.DONE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[2];
            }
            else if (achieveJsonList[i].State == (int)ACHIEVE_STATE.ACHIEVE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[3];
            }
        }
    }

    public void GetAchieveData()
    {

    }
}