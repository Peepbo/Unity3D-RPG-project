using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SlaveGoblin : EnemyMgr, IDamagedState
{
    private FollowTarget follow;

    private Vector3 direction;

    private bool isStart = true;

    public GameObject weapon;

    protected override void Awake()
    {
        base.Awake();

        //startPos = transform.position;
        hp = maxHp = 30;
        atk = 30;
        def = 5.0f;
        minGold = 20;
        maxGold = 30;

        follow = gameObject.AddComponent<FollowTarget>();
        follow.Init(AI, target, speed, attackRange);

        AI.enabled = false;
        weapon.GetComponent<AxColision>().SetDamage(atk);
        anim.SetInteger("state", 0);
    }

    void Update()
    {

        if (AI.enabled == false) AI.enabled = true;

        direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;

        float _distance = Vector3.Distance(transform.position, target.transform.position);

        if (isDead)
        {
            Die();
            return;
        }

        if (isStart)
        {
            if (_distance < findRange)
            {
                isStart = false;
            }
        }

        else
        {
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
                isStart = true;
                anim.SetInteger("state", 0);
                AI.isStopped = true;
            }
        }


    }


    public void FollowTarget()
    {
        anim.SetInteger("state", 1);
        setMoveType(follow);
        Move();
    }


    public void ActiveMeshCol()
    {
        if (isDead) return;
        weapon.GetComponent<MeshCollider>().enabled = true;
    }
    public void DeActiveMeshCol()
    {
        if (isDead) return;
        weapon.GetComponent<MeshCollider>().enabled = false;
    }


    public void GetRest()
    {
        if (isDead) return;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("isRest", true);
        AI.isStopped = true;
        yield return new WaitForSeconds(0.97f);
        //StartCoroutine(LookBack());
        anim.SetInteger("state", 0);
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
        if (isDead) return;

        if (hp > 0)
        {
            if (!isDamaged)
            {
                hp -= (int)(value * (1.0f - def / 100));
                StartCoroutine(GetDamage());
            }

            if (hp <= 0)
            {
                hp = 0;
                isDead = true;
                anim.SetTrigger("Die");
                controller.enabled = false;

                AI.enabled = false;
                weapon.GetComponent<MeshCollider>().enabled = false;
                StopAllCoroutines();

                DungeonMng.Instance.killMelee++;
            }

            if (player.isCri)
            {
                anim.SetTrigger("isDamage");
                weapon.GetComponent<MeshCollider>().enabled = false;
            }

        }
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

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, findRange);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);

    }


}
