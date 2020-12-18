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


    public enum State
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

    public State state = State.IDLE;


    public void ChangeState()
    {
        switch (state)
        {
            case State.IDLE:
                
                // state = State.ATK;
                //state = State.HIT
                break;
            case State.MOVE:

                break;
            case State.ATK:
                
                // state = State.CRIATK
                //state = State.IDLE
                //state = State.HIT
                //state = State.EVASION
                break;
            case State.CRIATK:
                //state = State.IDLE
                //state = State.HIT
                //state = State.EVASION
                break;
            case State.HIT:
                //state = State.IDLE
                break;
            case State.EVASION:
                break;
            case State.GUARD:
                break;
            case State.DIE:
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

