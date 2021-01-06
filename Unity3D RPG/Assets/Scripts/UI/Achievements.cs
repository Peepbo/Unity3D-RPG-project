using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

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
    public GameObject achieveSlots;
    public AchieveData[] data = new AchieveData[7];

    Color[] color = {
        new Color(117f / 255, 1, 105f / 255, 1),    // YET - 초록
        new Color(1, 1, 180f / 255, 1),             // PROGRESS - 노랑
        new Color(1, 192f / 255, 188f / 255, 1),    // DONE - 빨강
        new Color(177f / 255, 189f / 255, 1, 1),    // ACHIEVE - 파랑
    };

    private void Start()
    {
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

    }

    public void Content(int order)
    {
        if (data[order].state == ACHIEVE_STATE.ACHIEVE)
        {
            PlayerData.Instance.myCurrency += data[order].reward; // 아이템을 얻는다
            data[order].state = ACHIEVE_STATE.DONE;               // 업적 상태를 바꿔준다
        }
    }

    public void ShowAchieveData(int order)
    {
        for (int i = 1; i < data[order].count; i++)
        {
            if (data[i].state == ACHIEVE_STATE.YET)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[0];
            }
            else if (data[i].state == ACHIEVE_STATE.PROGRESS)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[1];
            }
            else if (data[i].state == ACHIEVE_STATE.DONE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[2];
            }
            else if (data[i].state == ACHIEVE_STATE.ACHIEVE)
            {
                achieveSlots.transform.GetChild(i).GetComponent<Image>().color = color[3];
            }
        }
    }

    public void GetAchieveData()
    {

    }
}