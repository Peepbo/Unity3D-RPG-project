using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanT : EnemyMgr, IDamagedState
{
    public enum TrainState
    {
        IDLE, ATTACKABLE
    }

    private TrainState state;
    private float distance;
    private Vector3 MoveDir;

    public GameObject fireBall;
    public Transform firePos;
    private bool isAtkReady;

    private float gravity = -9.81f;
    protected override void Awake()
    {
        base.Awake();
        maxHp = hp = 5;
        MoveDir = Vector3.zero;
        //state = TrainState.IDLE;
        state = TrainState.ATTACKABLE;
        isAtkReady = false;
    }

    void Update()
    {
        if (isDead)
        {
            Die();
            return;
        }
        
        if(!controller.isGrounded)
        {
            MoveDir.y += gravity * Time.deltaTime;
            controller.Move(MoveDir * Time.deltaTime);
        }

        switch (state)
        {
            case TrainState.IDLE:
                anim.SetInteger("state", 0);
                break;

            case TrainState.ATTACKABLE:
                {
                    distance = Vector3.Distance(target.transform.position, transform.position);

                    if (distance <= attackRange)
                    {
                        if (!isAtkReady)
                        {
                            StartCoroutine(Turn());
                        }
                    }
                    else
                    {
                        anim.SetInteger("state", 0);
                    }

                }
                break;
        }

    }


    public void Damaged(int value)
    {
        if (isDead) return;

        if (!isDamaged)
        {
            isDamaged = true;
            hp -= value;

            if (hp <= 0)
            {
                isDead = true;
                anim.SetTrigger("die");
            }

        }

    }

    public override void Die()
    {
        disappearTime += Time.deltaTime;

        if (disappearTime >= 2.0f)
        {
            disappearTime = 0.0f;
            this.gameObject.SetActive(false);
        }

    }

    public IEnumerator Turn()
    {
        if(!isDead)
        {
            float t = 0f;
            while (t < 0.7f)
            {
                t += Time.deltaTime;

                Vector3 _direction = (target.transform.position - transform.position).normalized;
                _direction.y = 0;
                Quaternion _targetRotation = Quaternion.LookRotation(_direction);

                Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, t / 1.5f);

                transform.localRotation = _nextRotation;

                yield return null;
            }

            isAtkReady = true;
            anim.SetInteger("state", 1);
        }
               
    }

    public TrainState ChangeState
    {
        get { return state; }

        set
        {
            state = value;
        }
    }

    #region Animation event functions
    public void Fire()
    {
        if (isDead) return;

        if (fireBall != null)
        {

            StartCoroutine(LookBack(0.7f));

            Instantiate(fireBall, firePos.position, Quaternion.identity);
            SoundManager.Instance.SFXPlay("Shaman_ATK", firePos.position);
        }

        else
        {
            Debug.Log("생성할 수 있는 FireBall 프리팹이 없습니다");
        }
    }
    public void ChangeReady()
    {
        isAtkReady = false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
