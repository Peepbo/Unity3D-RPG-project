using System.Collections;
using UnityEngine;
using UnityEngine.UI;


partial class Player
{
    Button gaurdButton;
    
    
    public bool isDash;
    bool    isCombo;
    int     comboCount;
    bool    isAtk;
    public bool    isCri;
    public bool    isFight;                //전투중이냐
    int     fightTimer;

    public enum PlayerState
    {
        IDLE,
        MOVE,
        ATK,
        CRIATK,
        HIT,
        EVASION,
        GUARD,
        DIE
    }

    void StateAwake()
    {
        gaurdButton = GameObject.Find("AtkButton").GetComponent<Button>();
    }
    void PlayerStateUpdate()
    {
        animator.SetFloat("Value", playerC.distance);
        ChangeState();
    }
   

    public PlayerState state = PlayerState.IDLE;


    public void ChangeState()
    {
        switch (state)
        {
            case PlayerState.IDLE:
                if(playerC.distance> 0.05f)
                { 
                    state = PlayerState.MOVE;
                }
                if( isFight)
                {
                    state = PlayerState.ATK;
                }
                break;
            case PlayerState.MOVE:
                if (playerC.distance <= 0.05f)
                {
                    state = PlayerState.IDLE;
                }
                if (isFight)
                {
                    state = PlayerState.ATK;
                }
                break;

            case PlayerState.ATK:
                if (!isFight)
                {
                    state = PlayerState.IDLE;
                }
                break;
            case PlayerState.CRIATK:
                break;
            case PlayerState.HIT:
                break;
            case PlayerState.EVASION:
                break;
            case PlayerState.GUARD:


                break;
            case PlayerState.DIE:
                break;
            default:
                break;
        }
    }
    
    public void staminaDown(int value)
    {
        if(stamina >= value)
        {
            stamina -= value;
            if(stamina < 0)
            {
                stamina = 0;
            }
        }
        
    }
    
    IEnumerator DashMove()
    {
        float startTime = Time.time;
        dashTime = 0.755f;
        Vector3 _dashValue = playerC.value3;
        playerC.child.LookAt(playerC.child.position + _dashValue);
        //while (Time.time < startTime + dashTime)
        while (Time.time < startTime + dashTime)
        {
            
            playerC.controller.Move(playerC.child.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        state = PlayerState.IDLE;
        isDash = false;
    }
    public void PlayerDash()
    {
        if (stamina > 30 && isDash == false && isCri == false)
        {
            animator.SetTrigger("Rolling");
            stamina -= 30;
            if(stamina < 0)
            {
                stamina = 0;
            }
            isDash = true;
            isFight = false;
            
            state = PlayerState.EVASION;
            comboAtk.ComboReset();
            StartCoroutine(DashMove());
        }
    }

    public void PlayerGaurd()
    {
        
    }
}

