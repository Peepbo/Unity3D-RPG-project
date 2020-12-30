using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

partial class Player
{
    public enum PlayerCondition
    {
        NONECONDITION,
        FIRECONDITION,
        POISONCONDITION,
        BLOODCONDITION,
        STURNCONDITION
    }
    
    public string           playerName;         //  플레이어 이름
    [HideInInspector]
    public int              maxHp = 100;        //  최대체력
    public int              hp = 100;           //  현제체력
    
    public int              maxStamina = 100;   //  최대 스태미나(최대기력)
    public int              stamina = 100;      //  스태미나(기력)
    int                     staminaTime =0;     //  스태미나 충전시간
    
    public float            def;                //  방어력
    public float            power;              //  공격력

    public int              dashValue;          //  플레이어 회피시 스태미너 소모량
    public float            dashTime;           //  플레이어 회피시간
    public float            dashSpeed;          //  플레이어 회피속도

    public float            guardStamina;       //  가드시 소모되는 스태미나(1데미지당 10스태미나)
    public PlayerCondition  condition;          //  플레이어 상태이상


    void PlayerStatUpdate()
    {
        StaminaReload();
    }
    public void GetHp(int value)
    {
        hp += value;
        if(hp>= maxHp)
        {
            hp = maxHp;
        }

    }
    public void GetDamage(int damage)
    {
        if(isGuard)
        {
            stamina -= 40;
            if(stamina <= 0)
            {
                stamina = 0;
            }
            animator.Play("ShieldBlock");
            animator.SetBool("isGuardHit", true);
            //animator.SetTrigger("GuardHit");
        }
        else
        {
            hp -= damage;
            if(hp < 0)
            {
                hp = 0;
                PlayerDie();
            }
        }
    }
    public void StaminaReload() // 스태미나 자동 충전
    {
        if(isGuard == false)
        {
            if(stamina<maxStamina)
            {
                staminaTime++;
                if(isFight)
                {
                    if( staminaTime > 12)
                    {
                        staminaTime = 0;
                        stamina++;
                    }
                }
                else
                {
                    if (staminaTime > 6)
                    {
                        staminaTime = 0;
                        stamina++;
                    }
                }
            }
        }
    }
}
