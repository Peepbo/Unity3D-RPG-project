using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

partial class Player
{
    public int maxHp; // 최대체력
    public int Hp; // 현제체력
    public int power;

    

    public void GetDamage(int damage)
    {
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
            PlayerDie();
        }
    }


    public void PlayerDie()
    {
        // 죽는거 구현 하면댐 
    }

    
}
