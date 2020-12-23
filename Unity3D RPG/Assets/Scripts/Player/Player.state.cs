using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


partial class Player
{
    PlayerController moveScript;
    public bool isDash;
    bool    isCombo;
    int     comboCount;
    bool    isAtk;
    bool    isCri;
    bool    isFight;                //전투중이냐
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
    

    void StateStart()
    {
        moveScript = GetComponent<PlayerController>();
    }
    void StateUpdate()
    {
        
    }
   

    public PlayerState state = PlayerState.IDLE;


    public void ChangeState()
    {
        switch (state)
        {
            case PlayerState.IDLE:

                // state = State.ATK;
                //state = State.HIT
                FightEnd();

                Vector2 _movementInput = playerC.value;
                if(_movementInput != Vector2.zero)
                {
                    state = PlayerState.MOVE;
                }

                if( isAtk)
                {
                    state = PlayerState.ATK;
                }


                break;
            case PlayerState.MOVE:
                if(playerC.value == Vector2.zero)
                {
                    state = PlayerState.IDLE;
                }

                if (isAtk)
                {
                    state = PlayerState.ATK;
                }
                break;

            case PlayerState.ATK:

                if (!isAtk)
                {
                    state = PlayerState.IDLE;
                }

                break;
            case PlayerState.CRIATK:
                //state = State.IDLE
                //state = State.HIT
                //state = State.EVASION
                break;
            case PlayerState.HIT:
                //state = State.IDLE
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
    void FightStart()
    {
        fightTimer = 0;
        isFight = true;
    }
    void FightEnd()
    { //전투중인지 검사
        if (isFight)
        {
            fightTimer++;
            if (fightTimer > 100)
            {
                fightTimer = 0;
                isFight = false;
            }
        }

    }
    public void PlayerAtkStart()
    {
        isAtk = true;
    }
    public void PlayerAtkEnd()
    {

    }
    public void PlayerCriticalAtk()
    {

    }
  
    IEnumerator DashMove()
    {
        float startTime = Time.time;
        
        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.child.forward * dashSpeed * Time.deltaTime);
           
            yield return null;
        }
       
        Debug.Log("change Idle");
        state = PlayerState.IDLE;
        isDash = false;
    }
    public void PlayerDash()
    {
        
        if( stamina> 30)
        {
            stamina -= 30;
            if(stamina < 0)
            {
                stamina = 0;
            }
            isDash = true;
            state = PlayerState.EVASION;
            StartCoroutine(DashMove());
        }
    }


   
}

