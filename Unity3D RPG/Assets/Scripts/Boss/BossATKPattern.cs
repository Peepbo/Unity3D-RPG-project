using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossATKPattern 
{
    void SetupATKPattern(ref Animator anim, Transform target);
}

public class ThreeATKPattern : IBossATKPattern
{
    public void SetupATKPattern(ref Animator anim, Transform target)
    {
        anim.SetTrigger("ThreeATK");
        anim.SetInteger("ThreeATKKind",0);
      
    }
}
