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
                button[i].GetComponent<Button>().enabled = false;
                button[i].GetChild(0).GetComponent<Text>().color = Color.black;
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
                PlayerData.Instance.normalInsurance = true;
                break;
            case 1: // rare
                PlayerData.Instance.rareInsurance = true;
                break;
            case 2: // normal + rare
                PlayerData.Instance.normalInsurance = true;
                PlayerData.Instance.rareInsurance = true;
                break;
        }

        for(int i = 0; i < 3; i++)
        {
            button[i].GetComponent<Button>().enabled = false;
            button[i].GetChild(0).GetComponent<Text>().color = Color.gray;
        }

        LinkData();
    }
    #endregion
}
