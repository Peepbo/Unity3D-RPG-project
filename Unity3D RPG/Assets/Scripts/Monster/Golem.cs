using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyMgr, IDamagedState
{
    public float dashSpeed;

    Vector3 spawnPos;
    FollowTarget follow;
    ReturnMove returnTohome;

    protected override void Awake()
    {
        base.Awake();
        spawnPos = transform.position;
    }
    private void Start()
    {
        follow = gameObject.AddComponent<FollowTarget>();
        returnTohome = gameObject.AddComponent<ReturnMove>();

        follow.initVariable(controller, target, speed);
        returnTohome.initVariable(controller, spawnPos, speed);

        returnTohome.setIsReturn(false);

    }
    private void Update()
    {
        Vector3 _between = target.transform.position - transform.position;
        // Vector3 _direction = _between.normalized;
        float _distance = _between.magnitude;


        if (_distance < findRange && !returnTohome.getIsReturn())
        {

            FollowTarget();


        }

        else
        {
            ReturnHome();
        }

    }

    public void FollowTarget()
    {
        float _home = (spawnPos - transform.position).magnitude;

        setMoveType(follow);
        follow.move();


        if (_home < 0.1f)
        {
            returnTohome.setIsReturn(false);
        }

    }

    public void ReturnHome()
    {
        setMoveType(returnTohome);
        returnTohome.move();
        returnTohome.setIsReturn(true);
    }
    public void Damaged(int value)
    {

    }
    public override void Die()
    {

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(spawnPos, 5f);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, findRange);
    //}
}
