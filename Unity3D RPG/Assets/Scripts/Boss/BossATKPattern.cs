using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossATKPattern 
{
    void SetupATKPattern(ref Animator anim, Transform target);
}
