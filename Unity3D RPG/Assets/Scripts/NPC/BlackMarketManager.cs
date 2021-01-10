using System.Collections;
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

    public void BuyItem(int num)
    {
        switch (num)
        {
            case 0://500 potion
                if (PlayerData.Instance.myCurrency < 500) return;

                PlayerData.Instance.myCurrency -= 500;
                PlayerData.Instance.player.BuyPotion(1);

                PlayerData.Instance.SaveData();
                break;
            case 1://500 boos skill
                Debug.Log("작업중");
                break;
            case 2://5000 come back
                if (PlayerData.Instance.myCurrency < 5000) return;

                PlayerData.Instance.myCurrency -= 5000;
                LootManager.Instance.Delivery();

                PlayerData.Instance.SaveData();
                LoadingSceneController.Instance.LoadScene("TownScene");
                break;
        }
    }

    public void ClosePanel()
    {
        UiManager0.Instance.PanelOpen = false;
    }
    public void OpenPanel()
    {
        UiManager0.Instance.PanelOpen = true;
    }

}
