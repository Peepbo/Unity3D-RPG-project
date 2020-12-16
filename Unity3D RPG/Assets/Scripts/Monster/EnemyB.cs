using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : EnemyManager
{ 
    GameObject arrowPrefab;
    Transform firePoint;

    private void Awake()
    {
        arrowPrefab = GameObject.Find("arrow");
        firePoint = transform.Find("firePoint");
    }
    protected override void Move()
    {
        base.Move();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    protected override void attack()
    {
        //fire 
        float dis = Vector3.Distance(target.transform.position, transform.position);
        if (dis < attackRange)
        {
            Instantiate(arrowPrefab, target.transform);
        }
    }
}
