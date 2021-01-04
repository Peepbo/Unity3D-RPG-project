using System.Collections;
using System.Collections.Generic;
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
    
    public int              maxHp = 100;        //  최대체력
    public int              hp = 100;           //  현제체력
    
    public int              maxStamina = 100;   //  최대 스태미나(최대기력)
    public int              stamina = 100;      //  스태미나(기력)
    float                   staminaTime =0f;     //  스태미나 충전시간
    
    public float            def;                //  방어력
    public float            increasedDef;       //  방어력 증가치

    public float            power;              //  공격력
    public float            increasedAtk= 0;    //  공격력 증가치
    public float            realAtk;            //  최종 공격력
    
    public int              dashValue;          //  플레이어 회피시 스태미너 소모량
    public float            dashTime;           //  플레이어 회피시간
    public float            dashSpeed;          //  플레이어 회피속도

    public float            guardStamina;       //  가드시 소모되는 스태미나(1데미지당 10스태미나)
    public PlayerCondition  condition;          //  플레이어 상태이상

    public float            atkSpeed = 1;       //  공격 속도

    [HideInInspector]
    public int              weaponKind;         //  무기 종류 0 = 한손검 1 = 두손검

    public AnimatorOverrideController overrideController1H;
    public AnimatorOverrideController overrideController2H;


    ItemInfo weapon = new ItemInfo();
    ItemInfo armor = new ItemInfo();
    ItemInfo accessory = new ItemInfo();
    ItemInfo bossItem = new ItemInfo();
    public GameObject currentWeapon;
    public GameObject currentShield;
    public GameObject[] weaponPrefabs = new GameObject[21];
    public GameObject[] shieldPrefabs = new GameObject[11];

    void PlayerStatUpdate()
    {
        StaminaReload();
        EquipStat();
    }
    
    public void EquipStat()
    {
        
        if(weapon != PlayerData.Instance.EquipWeapon())
        {
            weapon = PlayerData.Instance.EquipWeapon();
            if (weapon.kind == "한손검")
            {

                weaponKind = 0;
                animator.runtimeAnimatorController = overrideController1H;
                currentWeapon.SetActive(false);
                currentShield.SetActive(false);
                weaponPrefabs[weapon.id].SetActive(true);
                shieldPrefabs[weapon.id].SetActive(true);
                currentWeapon = weaponPrefabs[weapon.id];
                currentShield = shieldPrefabs[weapon.id];
            }
            else if (weapon.kind == "대검")
            {
                weaponKind = 1;
                animator.runtimeAnimatorController = overrideController2H;
                currentWeapon.SetActive(false);
                currentShield.SetActive(false);
                weaponPrefabs[weapon.id].SetActive(true);
                currentWeapon = weaponPrefabs[weapon.id];
            }
            increasedAtk = weapon.atk;
            atkSpeed = weapon.atkSpeed;
        }
        if (armor != PlayerData.Instance.EquipArmor())
        {
            armor = PlayerData.Instance.EquipArmor();
            increasedDef = armor.def;
        }
        if (accessory != PlayerData.Instance.EquipAccessory())
        {
            accessory = PlayerData.Instance.EquipAccessory();
        }
        if(bossItem != PlayerData.Instance.EquipBossItem())
        {
            bossItem = PlayerData.Instance.EquipBossItem();
        }
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
                staminaTime+= Time.deltaTime;
                if(isFight)
                {
                    if( staminaTime > 6*Time.deltaTime)
                    {
                        staminaTime = 0;
                        stamina+=maxStamina/200;
                    }
                }
                else
                {
                    if (staminaTime > 6 * Time.deltaTime)
                    {
                        staminaTime = 0;
                        stamina+= maxStamina/100;
                    }
                }
            }
        }
    }
}
