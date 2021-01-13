using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsuranceManger : MonoBehaviour
{
    public Text myCurrency;
    public Transform[] button = new Transform[3];

    bool buyItem = false;

    private void OnEnable()
    {
        LinkData();

        if (buyItem == false) CurrencyColor();
    }
    private void LinkData()
    {
        Debug.Log("Data Load and Link");
        PlayerData.Instance.LoadData_v2();
        myCurrency.text = PlayerData.Instance.myCurrency.ToString();
    }

    void CurrencyColor()
    {
        int[] _pay = new int[] { 500, 1000, 5000 };

        for(int i= 0; i < 3; i++)
        {
            if (PlayerData.Instance.myCurrency < _pay[i])
            {
                button[i].GetComponent<Button>().enabled = false;
                button[i].GetChild(0).GetComponent<Text>().color = Color.red;
            }
            else
            {
                button[i].GetComponent<Button>().enabled = true;
                button[i].GetChild(0).GetComponent<Text>().color = Color.white;
            }
        }
    }

    #region BUTTON FUNCTIONS
    public void Buy(int num)
    {
        buyItem = true;

        switch (num)
        {
            case 0: // normal
                if (PlayerData.Instance.myCurrency < 500) return;
                PlayerData.Instance.myCurrency -= 500;

                PlayerData.Instance.normalInsurance = true;

                PlayerData.Instance.SaveData();
                break;
            case 1: // rare
                if (PlayerData.Instance.myCurrency < 1000) return;
                PlayerData.Instance.myCurrency -= 1000;

                PlayerData.Instance.rareInsurance = true;

                PlayerData.Instance.SaveData();
                break;
            case 2: // normal + rare
                if (PlayerData.Instance.myCurrency < 5000) return;
                PlayerData.Instance.myCurrency -= 5000;

                PlayerData.Instance.normalInsurance = true;
                PlayerData.Instance.rareInsurance = true;

                PlayerData.Instance.SaveData();
                break;
        }

        SoundManager.Instance.SFXPlay2D("UI_Success", 0.6f);

        for (int i = 0; i < 3; i++)
        {
            button[i].GetComponent<Button>().enabled = false;
            button[i].GetChild(0).GetComponent<Text>().color = Color.gray;
        }

        LinkData();
    }

    public void QuitButton() { gameObject.SetActive(false); }
    public void CanTouch()
    {
        UiManager0.Instance.PanelOpen = false;
    }

    public void ClickSound() { SoundManager.Instance.SFXPlay2D("UI_Click"); }
    #endregion
}
