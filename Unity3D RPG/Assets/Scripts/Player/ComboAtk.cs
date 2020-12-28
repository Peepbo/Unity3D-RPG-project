using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboAtk : MonoBehaviour
{
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
        //atkButton.onClick.AddListener(Attack);
        criButton.onClick.AddListener(CriAttack);
        atkButton.onClick.AddListener(delegate { Attack(); });
    }
   
    public void Attack() 
    {
        if (player.isDash == false)
        {
            if (comboStep == 0)
            {
                animator.Play("Atk1");
                comboStep = 1;
                return;
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

    public void CriAttack()
    {
        if (player.isDash == false)
        {
            if (comboStep == 0)
            {
                
                animator.Play("Atk1"); //크리공격시작
                animator.speed = 0.7f;
                comboStep = 100;
                return;
            }
            if (comboStep != 0)
            {

                if (isCriAtk)
                {
                    isCriAtk = false;
                    comboStep += 10;
                }
            }
        }
    }
    public void ComboPossible()
    {
        isCombo = true;
        isCriAtk = true;
    }

    public void Combo()
    {
        if (comboStep == 2)
        {
            animator.Play("Atk2");
        }
        if (comboStep == 3)
        {
            animator.Play("Atk3");
        }
        if (comboStep == 110)
        {
            animator.Play("Atk2");
            animator.speed = 0.7f; 
        }
        if (comboStep == 120)
        {
            animator.Play("CriAtkFinal");
        }
        if (comboStep >10 && comboStep < 100)
        {
            animator.Play("Atk4");
        }
    }

    public void ComboReset()
    {
        isCombo = false;
        isCriAtk = false;
        comboStep = 0;
        animator.speed = 1f;
    }
}
