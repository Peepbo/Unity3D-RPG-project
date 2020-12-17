using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Player
{
    public enum State
    {
        ATK,
        CRIATK,
        HIT,
        EVASION,
        GUARD,
        DIE
        
    }

    public State state;


}

