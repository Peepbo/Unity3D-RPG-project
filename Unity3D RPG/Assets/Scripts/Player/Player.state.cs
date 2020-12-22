using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



partial class Player
{
    PlayerController moveScript;
    bool isCombo;

    bool isAtk;
    bool isCri;

    public bool isDash;
    public float dashTime;
    public float dashSpeed;

    void StateStart()
    {
        moveScript = GetComponent<PlayerController>();

    }
    void StateUpdate()
    {
        
    }
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

    public PlayerState state = PlayerState.IDLE;


    public void ChangeState()
    {
        switch (state)
        {
            case PlayerState.IDLE:

                // state = State.ATK;
                //state = State.HIT
                Vector2 _movementInput = playerC.value;
                if(_movementInput != Vector2.zero)
                {
                    state = PlayerState.MOVE;
                    Debug.Log("to move");
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
                    Debug.Log("to idle");
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
        isDash = true;
        state = PlayerState.EVASION;
         StartCoroutine(DashMove());
    }


   
}

