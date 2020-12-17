using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStat : MonoBehaviour
{
    public void ButtonAction(string statData)
    {
        string[] _result = statData.Split(new char[] { ',' });
        PlayerData.Instance.ChangeStat(_result[0], int.Parse(_result[1]));
    }
}
