using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Profile : MonoBehaviour
{
    public GameObject popStats;

    public GameObject popHp;
    public GameObject popStamina;

    private Player player;

    private void Update()
    {
        GetPlayerStats();
        GetReinforce();
    }

    public void Start()
    {
        player = Player.FindObjectOfType<Player>();
        GetPlayerStats();
        GetReinforce();
    }

    public void GetPlayerStats()
    {
        popStats.transform.GetChild(0).GetComponent<Text>().text = 
            "체력 : " + player.hp.ToString();
        popStats.transform.GetChild(1).GetComponent<Text>().text = 
            "스태미나 : " + player.stamina.ToString();
        popStats.transform.GetChild(2).GetComponent<Text>().text =
            "공격력 : " + player.realAtk.ToString();
        popStats.transform.GetChild(3).GetComponent<Text>().text =
            "방어력 : " + player.def.ToString();
    }

    public void GetReinforce()
    {
       

        int myHpLv = PlayerData.Instance.hpLv;
        int myStaminaLv = PlayerData.Instance.stmLv;
        if (myHpLv > 0)
        {
            for (int i = 0; i < myHpLv; i++)
            {
                popHp.transform.GetChild(i).GetComponent<Image>().color = Color.red;
            }   
        }
        if (myStaminaLv > 0)
        {
            for (int i = 0; i < myStaminaLv; i++)
            {
                popStamina.transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    public void OpenPanel()
    {
        UiManager0.Instance.PanelOpen = true;
    }

    public void ClosePanel()
    {
        UiManager0.Instance.PanelOpen = false;
    }
}
