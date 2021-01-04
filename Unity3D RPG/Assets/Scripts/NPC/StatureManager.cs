using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatureManager : MonoBehaviour
{
    public GameObject StaturePanel;

    public enum STAT
    {
        HP,
        STEMINA
    }

    public int hpLevel;
    public int steminaLevel;

    public GameObject hpSlots;
    public GameObject steminaSlots;

    public Image[] hpImg;
    public Image[] steminaImg;

    public Text money;
    public Text myLevel;

    public Text[] paymentAmount;

    // Start is called before the first frame update
    void Start()
    {
        hpImg = hpSlots.GetComponentsInChildren<Image>();
        steminaImg = steminaSlots.GetComponentsInChildren<Image>();

        hpLevel = PlayerData.Instance.hpLv;
        steminaLevel = PlayerData.Instance.stmLv;
    }

    private void Update()
    {
        if(StaturePanel.activeSelf)
        {
            money.text = PlayerData.Instance.myCurrency.ToString();
            myLevel.text = "성장 단계 : " + (hpLevel + steminaLevel).ToString();

            int _pay;

            for (int i = 0; i < 2; i++)
            {
                _pay = 1000 + (hpLevel + steminaLevel) * 1000;

                paymentAmount[i].text = _pay.ToString() + "G";

                if (PlayerData.Instance.myCurrency < _pay)
                    paymentAmount[i].color = Color.red;
                else
                    paymentAmount[i].color = Color.white;
            }
        }
    }

    public void Enhancement(int num)
    {
        if (paymentAmount[0].color == Color.red) return;

        switch ((STAT)num)
        {
            case STAT.HP:
                hpLevel++;

                PlayerData.Instance.hpLv = hpLevel;
                break;
            case STAT.STEMINA:
                steminaLevel++;

                PlayerData.Instance.stmLv = steminaLevel;
                break;
        }

        ChangeColor((STAT)num);
    }

    public void ChangeColor(STAT st)
    {
        switch (st)
        {
            case STAT.HP:
                for(int i = 1; i < hpImg.Length; i++)
                {
                    if (i > hpLevel) return;

                    hpImg[i-1].color = Color.green;
                }
                break;
            case STAT.STEMINA:
                for(int i = 1; i < steminaImg.Length; i++)
                {
                    if (i > steminaLevel) return;

                    steminaImg[i-1].color = Color.yellow;
                }
                break;
        }
    }
}
