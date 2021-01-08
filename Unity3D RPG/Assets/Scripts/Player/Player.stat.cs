﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

partial class Player
{
    public enum PlayerCondition
    {
        NONECONDITION,
        SLOWCONDITION,
        FIRECONDITION,
        POISONCONDITION,
        BLOODCONDITION,
        STURNCONDITION
    }
    
    public string           playerName;         //  플레이어 이름

    public int              potionNum;          //  플레이어 포션 갯수 
    public int              potionMaxNum;       //  플레이어 포션 최대 갯수

    public int              maxHp = 100;        //  최대체력
    public int              hp = 100;           //  현제체력

    public float            maxStamina;         //  최대 스태미나(최대기력)
    public float            stamina;            //  스태미나(기력)
    float                   staminaTime =0f;    //  스태미나 충전시간
    
    public float            def;                //  방어력
    public float            increasedDef;       //  방어력 증가치
    float                   realDef;            //  최종방어력

    public float            power;              //  공격력
    public float            increasedAtk= 0;    //  공격력 증가치
    public float            realAtk;            //  최종 공격력
    
    public int              dashValue;          //  플레이어 회피시 스태미너 소모량
    public float            dashTime;           //  플레이어 회피시간
    public float            dashSpeed;          //  플레이어 회피속도

    public float            guardStamina;       //  가드시 소모되는 스태미나(1데미지당 10스태미나)
    public PlayerCondition  condition;          //  플레이어 상태이상

    public float            atkSpeed = 1;       //  공격 속도

    public bool             isSlow;             // 플레이어가 느려졌는가
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

        PlayerData.Instance.nowHp = hp; // 싱글톤에 현재 hp 전달
    }


    public void ReturnData()
    {
        if(PlayerData.Instance.isReturn == true) //집에 갔을떄 
        {
            PlayerData.Instance.nowHp = maxHp;
            hp = maxHp;

            PlayerData.Instance.myCurrentPotion = PlayerData.Instance.myPotion;

            PlayerData.Instance.isReturn = false;
        }
        else
        {
            hp = PlayerData.Instance.nowHp;
        }
    }
    public void GrowthStat()
    {
        if(PlayerData.Instance.hpLv <=10)
        {
            maxHp = 100+ PlayerData.Instance.hpLv*200 ;
        }
        else if(PlayerData.Instance.hpLv> 10)
        {
            maxHp = 2100 + (PlayerData.Instance.hpLv-10) * 20;
        }

        if (PlayerData.Instance.stmLv <= 10)
        {
            maxStamina = 100 + PlayerData.Instance.stmLv * 30;
            Debug.Log("스태미너 레벨"+PlayerData.Instance.stmLv);

        }
        else if (PlayerData.Instance.stmLv > 10)
        {
            maxStamina = 400 + (PlayerData.Instance.stmLv-10) * 3;
        }

        PlayerData.Instance.nowHp = maxHp;
        hp = maxHp;
        stamina = maxStamina;
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
            comboAtk.currentCollider = currentWeapon.GetComponent<WeaponCollider>();
            increasedAtk = weapon.atk;
            atkSpeed = weapon.atkSpeed;

            realAtk = atkSpeed + increasedAtk;
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
        if(isDie== false)
        {
            if(isGuard && !isGuardGrogi)
            {
                stamina -= 40;
                if(stamina <= 0)
                {
                    stamina = 0;
                    isGuardGrogi = true;
                }
                animator.Play("ShieldBlock");
                animator.SetBool("isGuardHit", true);
                //animator.SetTrigger("GuardHit");
            }
            else
            {
                hp -= (int)(damage *(1- realDef/100));
                if(hp < 0)
                {
                    hp = 0;
                    PlayerDie();
                }
            }
        }
    }
    public void StaminaReload() // 스태미나 자동 충전
    {
        if(isDie == false)
        {
            if (isGuard == false)
            {
                if (stamina < maxStamina)
                {
                    staminaTime += Time.deltaTime;
                    if (isFight)
                    {
                        if (staminaTime > 6 * Time.deltaTime)
                        {
                            staminaTime = 0;
                            stamina += (maxStamina / 200);
                            if (stamina >= maxStamina)
                            {
                                stamina = maxStamina;
                                if (isGuardGrogi == true)
                                {
                                    isGuardGrogi = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (staminaTime > 6 * Time.deltaTime)
                        {
                            staminaTime = 0;
                            stamina += maxStamina / 100;
                            if (stamina >= maxStamina)
                            {
                                stamina = maxStamina;
                                if (isGuardGrogi == true)
                                {
                                    isGuardGrogi = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
