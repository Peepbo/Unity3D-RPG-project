﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveGoblin : EnemyMgr, IDamagedState
{
    private FollowTarget follow;

    private Vector3 direction;

    private int findCount;
    private bool isFind;

    // public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();
        //startPos = transform.position;
        hp = maxHp;
        atk = 30;
        def = 5.0f;
        minGold = 20;
        maxGold = 30;
        findCount = 0;

    }
    void Start()
    {
        //Goblin Move pattern

        follow = gameObject.AddComponent<FollowTarget>();
        follow.Init(AI, target, speed, attackRange);

        //weapon.GetComponent<AxColision>().SetDamage(atk);


        anim.SetInteger("state", 0);
    }

    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;

        Debug.DrawRay(transform.position, direction * findRange, Color.blue);

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        if (isDead)
        {
            Die();
            return;
        }

        if (_distance < findRange)
        {
            FollowTarget();

            if (_distance < attackRange)
            {
                anim.SetInteger("state", 2);
            }
        }
        else
        {
            anim.SetInteger("state", 0);
            AI.isStopped = true;
        }

    }


    public void FollowTarget()
    {
        //temp
        AI.isStopped = false;
        //controller의 speed를 animation velocity 값에 넣어준다.
        //anim.SetFloat("velocity", controller.velocity.magnitude);
        //anim.SetFloat("velocity", AI.speed);
        anim.SetInteger("state", 1);
        setMoveType(follow);
        Move();
    }


    public void ActiveMeshCol()
    {
        if (isDead) return;
        //weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void DeActiveMeshCol()
    {
        if (isDead) return;
        // weapon.GetComponent<MeshCollider>().enabled = false;
    }


    public void GetRest()
    {
        if (isDead) return;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {

        //rest를 켜고
        anim.SetBool("isRest", true);
        AI.isStopped = true;
        yield return new WaitForSeconds(1.5f);

        Vector3 _direction = target.transform.position - transform.position;
        _direction.Normalize();
        _direction.y = 0;


        transform.forward = (_direction);

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        AI.isStopped = false;

    }

    IEnumerator GetDamage()
    {
        isDamaged = true;
        AI.isStopped = true;

        yield return new WaitForSeconds(.7f);

        AI.isStopped = false;
        isDamaged = false;

    }

    public void Damaged(int value)
    {
        // weapon.GetComponent<MeshCollider>().enabled = false;
        if (isDamaged || isDead) return;

        if (hp > 0)
        {
            hp -= (int)(value * (1.0f - def / 100));
            StartCoroutine(GetDamage());
        }

        else
        {
            hp = 0;
            isDead = true;
            anim.SetTrigger("Die");

            controller.enabled = false;
            AI.enabled = false;
            StopAllCoroutines();

        }
        anim.SetTrigger("isDamage");

    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if (disappearTime > 3.5f)
        {
            gameObject.SetActive(false);
            disappearTime = 0f;
        }
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


}
