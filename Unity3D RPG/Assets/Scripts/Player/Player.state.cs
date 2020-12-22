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

    bool isDash;
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

                break;
            case PlayerState.MOVE:
                if(playerC.value == Vector2.zero)
                {
                    state = PlayerState.IDLE;
                    Debug.Log("to idle");
                }
                break;

            case PlayerState.ATK:
                


                // state = State.CRIATK
                //state = State.IDLE
                //state = State.HIT
                //state = State.EVASION
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
    
    public void PlayerAtk()
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


    }
    public void PlayerDash()
    {
         StartCoroutine(DashMove());
    }


   
}

