using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboAtk : MonoBehaviour
{
    public WeaponCollider currentCollider;
    GameObject particle;
    Button atkButton;
    Button criButton;
    public Animator animator;
    public Player player;
    bool isCombo;
    bool isCriCombo;
    bool isCriAtk;
    
    int comboStep;
    private void Awake()
    {
        atkButton = GameObject.Find("AtkButton").GetComponent<Button>();
        criButton = GameObject.Find("CriAtkButton").GetComponent<Button>();
        atkButton.onClick.AddListener(Attack);
        criButton.onClick.AddListener(CriAttack);
        //atkButton.onClick.AddListener(delegate { Attack(); });
    }
    
    public void ColiderOn()
    {
        if (player.isGuard == false)
        {
            currentCollider.meshCollider.enabled = true;
            currentCollider.particle.SetActive(true);
        }

    }
    public void ParticleOff()
    {
        currentCollider.particle.SetActive(false);
    }
    public void ColiderOff()
    {
        currentCollider.meshCollider.enabled = false;
        currentCollider.particle.SetActive(false);
    }

    public void GuardHitEnd()
    {
        player.animator.SetBool("isGuardHit", false);
    }
    
    public void Attack() 
    {
        if(player.isDie == false)
        {
            if (player.isDash == false && player.isGuard == false)
            {
                player.isFight = true;
                if (comboStep == 0)
                {
                    if (player.stamina >= 30)
                    {
                        player.staminaDown(30);
                        animator.Play("Atk1");
                        comboStep = 1;
                        return;
                    }
                    else
                    {
                        player.isFight = false;
                        player.isCri = false;
                    }
                }
                if (comboStep != 0)
                {
                    if (isCombo)
                    {
                        isCombo = false;
                        comboStep += 1;
                    }
                }
            }
        }
       
    }

    public void CriAttack()
    {
        if(player.isGuard == false && player.isDie == false )
        {

            player.isFight = true;
            player.isCri = true;
            if (player.isDash == false)
            {
                if (comboStep == 0)
                {
                    if ( player.stamina >= 50)
                    {
                        player.staminaDown(50);
                        animator.Play("Atk3"); //크리공격시작
                        comboStep = 100;
                        return;
                    }
                    else
                    {
                        player.isFight = false;
                        player.isCri = false;
                    }
                }
                else if (comboStep != 0)
                {
                    if (isCriAtk)
                    {
                        isCriAtk = false;
                        comboStep += 10;
                    }
                }
            }
        }
    }
    public void ComboPossible()
    {
       
        isCombo = true;
        isCriAtk = true;
        ColiderOn();
    }

    public void Combo()
    {
        ColiderOff();
        if (comboStep == 2)
        {
            ComboAnimation(30, "Atk2");
        }
        if (comboStep == 3)
        {
            ComboAnimation(30, "Atk4");
        }
        if (comboStep >=110)
        {
            ComboAnimation(50, "CriAtkFinal");
        }
        if (comboStep >10 && comboStep < 100)
        {
            ComboAnimation(50, "CriAtkFinal");
        }
       
    }

    public void ComboReset()
    {
        isCombo = false;
        isCriAtk = false;
        comboStep = 0;
        player.isFight = false;
        player.isCri = false;
        ColiderOff();
    }


    void ComboAnimation(int value, string name)
    {
        if(player.stamina>= value)
        {
            player.staminaDown(value);
            animator.Play(name);
        }
        else
        {
            player.isFight = false;
            player.isCri = false;
        }
    }


}
