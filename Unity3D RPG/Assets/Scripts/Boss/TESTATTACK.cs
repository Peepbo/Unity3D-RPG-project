using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTATTACK : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject boss;
    int hp = 100;
    // Update is called once per frame
    //int atk = 50;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boss.GetComponent<IDamagedState>().Damaged(10);
        }
        
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
           

        }
        Debug.Log(hp);
    
    }
}
