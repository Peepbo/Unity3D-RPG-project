using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController 
{
    
   public void Idle(ref Animator anim) 
    {
        anim.SetTrigger("Idle");
    }
    public void Move(Transform target, ref NavMeshAgent agent)
    {
        agent.SetDestination(target.position);
    }
}
