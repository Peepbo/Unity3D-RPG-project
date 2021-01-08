﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //public ChestManager chestData;
    //public StatManager statData;
    // Start is called before the first frame update

    private void Start()
    {
        JsonData.Instance.CheckJsonData();
        PlayerData.Instance.LoadData_v2();

        PlayerData.Instance.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PlayerData.Instance.player.EquipStat();

        //Debug.Log(PlayerData.Instance.myCurrency);

        //chestData.MakeData();
        //chestData.ItemUpdate();
        //chestData.OwnLoots();
        //chestData.GetLootsData();

        //chestData.GetData();

        //statData.ResetStat();
        //statData.GetData();
    }

    public void LoadData()
    {
        PlayerData.Instance.LoadData_v2();
    }

    public void ClosePanel()
    {
        UiManager0.Instance.PanelOpen = false;
    }
}
