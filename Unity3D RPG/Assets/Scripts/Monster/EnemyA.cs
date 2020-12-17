using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyManager
{
    public override void Move()
    {
        base.Move();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);
    }

    public override void Attack()
    {
        //swing the weapon
        float dis = Vector3.Distance(transform.position, target.transform.position);

        if (dis <= findRange)
        {
            print("attack");
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

}
