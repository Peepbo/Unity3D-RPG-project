﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlackMarketManager : MonoBehaviour
{
    public Text currency;

    private void OnEnable()
    {
        LinkData();
    }

    private void LinkData()
    {
        Debug.Log("Data Load and Link");
        PlayerData.Instance.LoadData_v2();

        currency.text = (PlayerData.Instance.myCurrency).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(int num)
    {
        switch (num)
        {
            case 0://500 potion
                if (PlayerData.Instance.myCurrency < 500) return;
                {
                    Debug.LogWarning("작업중");
                }

                break;
            case 1://500 boos skill
                Debug.Log("작업중");
                break;
            case 2://
                if (PlayerData.Instance.myCurrency < 5000) return;

                PlayerData.Instance.myCurrency -= 5000;
                LootManager.Instance.Delivery();

                PlayerData.Instance.SaveData();
                LoadingSceneController.Instance.LoadScene("TownScene");
                break;
        }
    }
}
