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

    void Update()
    {

    }

    public void Content(int order)
    {
        if(data[order].state == ACHIEVE_STATE.ACHIEVE)
        {
            PlayerData.Instance.myCurrency += data[order].reward; // 아이템을 얻는다
            data[order].state = ACHIEVE_STATE.DONE;               // 업적 상태를 바꿔준다
        }
    }
}