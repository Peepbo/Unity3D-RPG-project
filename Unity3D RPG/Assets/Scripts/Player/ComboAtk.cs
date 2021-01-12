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
    Button dashButton;
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
        dashButton = GameObject.Find("DashButton").GetComponent<Button>();
        atkButton.onClick.AddListener(Attack);
        criButton.onClick.AddListener(CriAttack);
        dashButton.onClick.AddListener(player.PlayerDash);
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
                    if (player.stamina >= 15)
                    {
                        player.staminaDown(15);
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
                else if (comboStep != 0)
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
                    if ( player.stamina >= 25)
                    {
                        player.staminaDown(25);
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
            ComboAnimation(15, "Atk2");
        }
        if (comboStep == 3)
        {
            ComboAnimation(15, "Atk4");
        }
        if (comboStep >=110)
        {
            ComboAnimation(25, "CriAtkFinal");
        }
        if (comboStep >10 && comboStep < 100)
        {
            ComboAnimation(25, "CriAtkFinal");
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

    public void FootSound()
    {
        string randomFoot = "Player_Foot0" + Random.Range(1, 5).ToString();
        SoundManager.Instance.SFXPlay2D(randomFoot);
    }
}
