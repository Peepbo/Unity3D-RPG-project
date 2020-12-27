using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : EnemyMgr,IDamagedState
{

    ViewingAngle viewAngle;
    ObservingMove observe;
 

    
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
    
    public void Damaged()
    {

    }
}
