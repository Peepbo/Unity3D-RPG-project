using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;
    protected int hp;
    protected bool isDead;


    public int MaxHp = 10;
    [Range(1, 5)]
    public float speed = 1;
    [Range(5, 15)]
    public float findRange;
    [Range(1, 10)]
    public float attackRange;


    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
        hp = MaxHp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public abstract void Move();
    public abstract void Attack();

    public virtual void Damage(int damage)
    {

        hp -= damage;

        if (hp <= 0 && !isDead)
        {
            //사망처리 함수
            print("사망");
        }
    }

}
