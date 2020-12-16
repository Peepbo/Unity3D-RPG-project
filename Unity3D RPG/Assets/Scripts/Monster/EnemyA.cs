using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyManager
{
    protected override void Move()
    {
        base.Move();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);
    }
}
