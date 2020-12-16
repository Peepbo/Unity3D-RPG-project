using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : EnemyManager
{
    GameObject arrowPrefab;
    Transform firePoint;

    float spawnCount = 0;
    protected override void Awake()
    {
        base.Awake();
        firePoint = transform.Find("firePoint");
    }
    public override void Move()
    {
        // if (Vector3.Distance(target.transform.position, transform.position) < attackRange) return;
        base.Move();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.cyan, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.Lerp(Color.red, Color.red, Mathf.PingPong(Time.time, 0.5f));
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public override void Attack()
    {
        //fire 

        //float dis = Vector3.Distance(target.transform.position, transform.position);
        //if (dis < attackRange)
        //{
        //    Instantiate(arrowPrefab, target.transform);
        //}

        arrowPrefab = ObjectPool.SharedInstance.GetPooledObject("arrow");
        
        float dis = Vector3.Distance(target.transform.position, transform.position);
        spawnCount += Time.deltaTime;
        if (dis < attackRange && spawnCount > 3.0f)
        {
            arrowPrefab.transform.position = firePoint.position;
            arrowPrefab.transform.rotation = transform.rotation;
            arrowPrefab.SetActive(true);
           // Instantiate(arrowPrefab,firePoint.position,transform.rotation);
            spawnCount = 0;
        }

    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
    }
}
