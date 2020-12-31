using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyMgr ,IDamagedState
{
    public float dashSpeed;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        
    }

    public void Damaged(int value)
    {

    }
    public override void Die()
    {
        
    }
}
