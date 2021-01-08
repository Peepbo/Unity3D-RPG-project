using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController 
{
    
    Transform target;
    Animator anim;
    NavMeshAgent agent;
    public void Init(Transform _target, Animator _anim, NavMeshAgent _agent) 
    {
        
        target = _target;
        anim = _anim;
        agent = _agent;
  
        
    }
    public void Idle()
    {
        
        

       
    }
    public void CombatIdle()
    {
        
        anim.SetTrigger("CombatIdle");
        
    }
    
    public void Move()
    {
        agent.SetDestination(target.position);
        
        anim.SetTrigger("Run");
    }
    
    //public void attack(BossATKPattern pattern)
    //{
    //    pattern.SetupATKPattern(ref anim, target);
    //}
    public void Hit()
    {
        anim.SetTrigger("Hit");
    }
    //IEnumerator hitAnimEnd( Animator anim)
    //{
    //    yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
    //    anim.SetTrigger("Idle");

    //}
    public void Die(ref BossState state)
    {
        anim.SetTrigger("Die");
    }
}
