using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonS : EnemyMgr, IDamagedState
{
    private FollowTarget follow;
    private Vector3 direction;
    private float distance;

    public GameObject weapon;
    private bool isStay = true;

    protected override void Awake()
    {
        base.Awake();

        hp = maxHp = 60;
        atk = 55;
        def = 10.0f;
        AI.enabled = false;
        follow = gameObject.AddComponent<FollowTarget>();
        follow.Init(AI, target, speed, 0);

        weapon.GetComponent<WepCol>().setAtk(atk);
    }

    void Update()
    {
        if (isDead)
        {
            Die();
            return;
        }

        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (AI.enabled == false) AI.enabled = true;

        if (isStay)
        {
            if (distance < findRange)
            {
                AI.isStopped = false;
                isStay = false;
                anim.SetInteger("state", 1);
            }
            else
            {
                AI.isStopped = true;
                anim.SetInteger("state", 0);
            }

        }

        else
        {
            if (distance < findRange)
            {
                Follow();
                if (distance <= attackRange)
                {
                    AI.stoppingDistance = attackRange;
                    //attack;
                    anim.SetInteger("state", 2);
                }
            }
            else
            {
                isStay = true;
                anim.SetInteger("state", 0);
            }
        }
    }


    private void Follow()
    {
        setMoveType(follow);
        Move();
        anim.SetInteger("state", 1);
    }


    IEnumerator AttackRoutine()
    {
        //rest를 켜고
        anim.SetBool("isRest", true);
        AI.isStopped = true;
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(LookBack());

        yield return new WaitForSeconds(1.5f);
        //rest를 끈다
        anim.SetInteger("state", 1);
        anim.SetBool("isRest", false);
        if (!isStay)
            AI.isStopped = false;

    }


    public void Damaged(int value)
    {
        if (isDead) return;

        if (hp > 0)
        {
            if (!isDamaged)
            {
                anim.SetTrigger("isDamage");
                hp -= (int)(value * (1.0f - def / 100));
            }

            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                anim.SetTrigger("Die");
                StopAllCoroutines();
                AI.enabled = true;

                DungeonMng.Instance.killMelee++;
            }
        }
    }


    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if (disappearTime > 2.0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findRange);
    }

    #region
    public void RandomAttack()
    {
        int _type = Random.Range(0, 2);
        anim.SetInteger("atkType", _type);
    }

    public void GetRest()
    {
        if (isDead) return;
        StartCoroutine(AttackRoutine());
    }

    public void Active()
    {
        if (isDead) return;
        weapon.GetComponent<MeshCollider>().enabled = true;
    }

    public void DeActive()
    {
        weapon.GetComponent<MeshCollider>().enabled = false;
    }
    #endregion
}
