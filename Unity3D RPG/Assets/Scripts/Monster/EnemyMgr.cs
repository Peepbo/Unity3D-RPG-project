using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMgr : MonoBehaviour
{
    private IMoveAble moveType;
    private IAttackAble attackType;
    protected NavMeshAgent AI;
    protected CharacterController controller;
    protected GameObject target;
    protected Animator anim;
    protected float gravity = -9.81f;

    protected bool isDamaged;
    protected bool isDead;
    protected float disappearTime;

    //temp
    public GameObject coinEffect;


    [Range(5, 15)]
    public float findRange;
    [Range(1, 3)]
    public float attackRange;



    protected virtual void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");

        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Animation")
                anim = transform.GetChild(i).GetComponent<Animator>();
        }
        AI = gameObject.GetComponent<NavMeshAgent>();

        isDead = false;
    }

    public void Move()
    {
        moveType.move();
    }

    public void Attack()
    {
        attackType.attack();
    }

    public void setMoveType(IMoveAble newMoveType)
    {
        this.moveType = newMoveType;
    }

    public void setAttackType(IAttackAble newAttackType)
    {
        this.attackType = newAttackType;
    }

    public abstract void Die();
   
    public Vector3 GetRandomDirection()
    {
        float _ranX = Random.Range(-1f, 1f);
        float _ranZ = Random.Range(-1f, 1f);
        return new Vector3(_ranX, 0, _ranZ);
    }

}
