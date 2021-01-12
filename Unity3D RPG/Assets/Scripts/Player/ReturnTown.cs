using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTown : MonoBehaviour
{
    
 
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerData.Instance.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PlayerData.Instance.player.EquipStat();
        PlayerData.Instance.player.GrowthStat();
        PlayerData.Instance.myPotion = 5;
        PlayerData.Instance.myCurrentPotion = 5;
       
    }

   
}
