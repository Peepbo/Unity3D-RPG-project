﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Potion : MonoBehaviour, IPointerDownHandler
{
    GameObject potionParticle1;
    GameObject potionParticle2;
    Player player;
    [SerializeField] GameObject button;
    public Text potionNumTxt;
    private void Awake()
    {
        player = GameObject.Find("MainPlayer").GetComponent<Player>();
        potionNumTxt = button.transform.GetChild(1).GetComponent<Text>();
        potionParticle1 = GameObject.Find("Character_Hero_Knight_Male").transform.GetChild(0).gameObject;
        potionParticle2 = GameObject.Find("Character_Hero_Knight_Male").transform.GetChild(1).gameObject;

    }
   
    public void OnPointerDown(PointerEventData eventData)
    {
        if(player.isDie ==false)
        {
            if (PlayerData.Instance.myCurrentPotion > 0)
            {
                if (potionParticle1.activeSelf == false && potionParticle2.activeSelf == false)
                {
                    potionParticle1.SetActive(true);
                    potionParticle2.SetActive(true);
                    player.GetHp(30);
                    potionNumTxt.text = PlayerData.Instance.myCurrentPotion.ToString();

                }
            }
        }
       
    }
    
    
}
