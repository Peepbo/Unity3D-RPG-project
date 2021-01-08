using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void GetPlayerStats()
    {
        popStats.transform.GetChild(0).GetComponent<Text>().text = 
            "체력 : " + player.hp.ToString() + " / 1000";
        popStats.transform.GetChild(1).GetComponent<Text>().text = 
            "스태미나 : " + player.stamina.ToString() + " / 1000";
        popStats.transform.GetChild(2).GetComponent<Text>().text =
            "공격력 : " + player.realAtk.ToString();
        popStats.transform.GetChild(3).GetComponent<Text>().text =
            "방어력 : " + player.def.ToString();
    }

    public void GetReinforce()
    {
        int myHp = player.maxHp / 100;
        float myStamina = player.maxStamina / 100;

        if (myHp > 0)
        {
            for (int i = 0; i < myHp; i++)
            {
                popHp.transform.GetChild(i).GetComponent<Image>().color = Color.red;
            }
        }
        if (myStamina > 0)
        {
            for (int i = 0; i < myStamina; i++)
            {
                popStamina.transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
