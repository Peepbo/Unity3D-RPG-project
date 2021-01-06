using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeATK_Pattern : BossATKPattern
{
    public void SetupATKPattern(ref Animator anim, Transform target)
    {
        anim.SetTrigger("ThreeATK");
        
    }
}
