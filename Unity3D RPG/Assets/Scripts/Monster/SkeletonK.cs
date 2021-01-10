using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonK : EnemyMgr, IDamagedState
{

    private FollowTarget follow;
    private ReturnMove back;
    private Vector3 direction;
    private float distance;

    private Vector3 startPos;

    public GameObject weapon;
    protected override void Awake()
    {
        base.Awake();
        hp = maxHp = 175;
        atk = 80;
        def = 20.0f;
        minGold = 100;
        maxGold = 200;
        for (int i = 0; i < 3; i++)
        {
            itemKind[i] = 79;
            drop = CSVData.Instance.find(itemKind[i]);
            item.Add(drop);
        }
        AI.enabled = true;
    }

    private void Start()
    {
        //Goblin Move pattern
        //observe = gameObject.AddComponent<ObservingMove>();
        follow = gameObject.AddComponent<FollowTarget>();
        back = gameObject.AddComponent<ReturnMove>();

       // weapon.GetComponent<WepCol>().setAtk(atk);

        follow.Init(AI, target, speed, 0);
        back.init(AI, startPos, speed);

        anim.SetInteger("state", 0);
    }

    private void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (isDead)
        {
            Die();
            return;
        }

        if (AI.enabled == false) AI.enabled = true;


    }

    public void Damaged(int value)
    {

    }

    public override void Die()
    {

    }

}
