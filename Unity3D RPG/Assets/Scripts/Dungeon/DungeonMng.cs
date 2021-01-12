using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMng : Singleton<DungeonMng>
{
    protected DungeonMng() { }

    public int killMelee = 0;
    public int killRange = 0;

    public int killCount = 0; //레인지, 밀리 몬스터 총 합


    public void KillMeleeMonster()
    {
        killMelee++;
        killCount = killMelee + killRange;
    }

    public void KillRangeMonster()
    {
        killRange++;
        killCount = killMelee + killRange;
    }
}
