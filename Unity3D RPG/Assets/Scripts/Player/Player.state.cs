using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Player
{

    bool isCombo;

    bool isAtk;
    bool isCri;


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
                break;
            case PlayerState.MOVE:

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

}

