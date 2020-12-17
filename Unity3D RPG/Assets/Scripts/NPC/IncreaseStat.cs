using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseStat : MonoBehaviour
{
    string str;
    public GameObject popUp;
    public Text popName;
    public void SaveData(string statData)
    {
        str = statData;

        popName.text = statData;
        popUp.SetActive(true);
    }

    public void ButtonAction()
    {
        string[] _result = str.Split(new char[] { ',' });
        PlayerData.Instance.ChangeStat(_result[0], int.Parse(_result[1]));
    }
}
