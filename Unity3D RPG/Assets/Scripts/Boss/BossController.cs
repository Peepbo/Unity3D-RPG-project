using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController 
{
    
    Transform target;
  
    public void Init(Transform _target) 
    {
        
        target = _target;
  
        
    }
    public void Idle(ref NavMeshAgent agent, ref Animator anim, ref BossState state)
    {

        //anim.SetTrigger("Idle");
        if ((agent.transform.position - target.position).sqrMagnitude > 1f)
            state = BossState.RUN;
        Debug.Log(state);
    }
    public void CombatIdle(ref Animator anim)
    {
        anim.SetTrigger("CombatIdle");
    }
    
    public void Move(ref NavMeshAgent agent, ref Animator anim)
    {
        agent.SetDestination(target.position);
        agent.stoppingDistance = 1f;
        anim.SetTrigger("Run");
    }
    
    public void attack(BossATKPattern pattern, ref Animator anim)
    {
        pattern.SetupATKPattern(ref anim, target);
    }
    public void Hit(ref Animator anim)
    {
        anim.SetTrigger("Hit");
    }
    //IEnumerator hitAnimEnd( Animator anim)
    //{
    //    yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
    //    anim.SetTrigger("Idle");

    //}
    public void Die(ref Animator anim)
    {
        anim.SetTrigger("Die");
    }
}
