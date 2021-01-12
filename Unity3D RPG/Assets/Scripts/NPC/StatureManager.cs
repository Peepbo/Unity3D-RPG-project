using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatureManager : MonoBehaviour
{
    public enum STAT
    {
        HP,
        STEMINA
    }

    public GameObject hpSlots;
    public GameObject steminaSlots;

    public Image[] hpImg;
    public Image[] steminaImg;

    public Text money;
    public Text myLevel;

    public Text[] paymentAmount;
    public int payMoney;
    Player player;
   
    private void OnEnable()
    {
        LinkData();
    }

    // Start is called before the first frame update
    void Start()
    {
        hpImg = hpSlots.GetComponentsInChildren<Image>();
        steminaImg = steminaSlots.GetComponentsInChildren<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        ChangeColor();

    }

    void LinkData()
    {
        Debug.Log("Data Load and Link");
        PlayerData.Instance.LoadData_v2();

        money.text = PlayerData.Instance.myCurrency.ToString();
        myLevel.text = "성장 단계 : " + 
            (PlayerData.Instance.hpLv + PlayerData.Instance.stmLv).ToString();

        int _pay = 0;

        for (int i = 0; i < 2; i++)
        {
            _pay = 1000 + (PlayerData.Instance.hpLv + PlayerData.Instance.stmLv) * 1000;
            payMoney = _pay;
            paymentAmount[i].text = _pay.ToString() + "G";

            if (PlayerData.Instance.myCurrency < _pay)
                paymentAmount[i].color = Color.red;
            else
                paymentAmount[i].color = Color.white;
        }
    }
    public void ChangeColor()
    {

        for (int i = 1; i < hpImg.Length; i++)
        {
            if (i > PlayerData.Instance.hpLv) continue;

            hpImg[i - 1].color = Color.green;
        }

        for (int i = 1; i < steminaImg.Length; i++)
        {
            if (i > PlayerData.Instance.stmLv) continue;

            steminaImg[i - 1].color = Color.yellow;
        }

    }

    #region BUTTON FUNCTIONS
    public void Enhancement(int num)
    {
        if (paymentAmount[0].color == Color.red) return;

        switch ((STAT)num)
        {
            case STAT.HP:
                PlayerData.Instance.hpLv++;
                break;
            case STAT.STEMINA:
                PlayerData.Instance.stmLv++;
                break;
        }
        PlayerData.Instance.myCurrency -= payMoney;
        player.GrowthStat();
        PlayerData.Instance.SaveData();

        LinkData();

        ChangeColor((STAT)num);
    }

    public void ChangeColor(STAT st)
    {
        switch (st)
        {
            case STAT.HP:
                for(int i = 1; i < hpImg.Length; i++)
                {
                    if (i > PlayerData.Instance.hpLv) return;

                    hpImg[i-1].color = Color.green;
                }
                break;
            case STAT.STEMINA:
                for(int i = 1; i < steminaImg.Length; i++)
                {
                    if (i > PlayerData.Instance.stmLv) return;

                    steminaImg[i-1].color = Color.yellow;
                }
                break;
        }
    }

    public void QuitButton() { gameObject.SetActive(false); }
    #endregion
}
