using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    Player player;
    bool isCol = false;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        {
            isCol = false;
        }
    }
    private void Awake()
    {
        player = Player.FindObjectOfType<Player>();
    }
    private void Update()
    {

    }
    public void hit()
    {
        print("준비중");
        if (!isCol) return;
        print("기다려 때린다?");

        player.GetDamage(1);
        print(player.hp);


    }
}
