using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("awake");
        JsonData.Instance.CheckJsonData();
        PlayerData.Instance.LoadData_v2();
    }
}
