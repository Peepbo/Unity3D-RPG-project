using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;
    protected float hp;
    protected bool isDead;


    public float MaxHp = 10.0f;
    [Range(1, 5)]
    public float speed = 1;
    [Range(5, 10)]
    public float findRange;
    [Range(1, 10)]
    public float attackRange;
    

    protected virtual void Awake()
    {
        print("awake");
        controller = GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
        hp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    public virtual void Move()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            Vector3 _lookPos = target.transform.position - transform.position;
            //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
            _lookPos.y = 0;

            transform.rotation = Quaternion.LookRotation(_lookPos);

            _lookPos.Normalize();

            controller.Move(_lookPos * speed * Time.deltaTime);
        }
    }
    public abstract void Attack();

    public virtual void Damage(float damage)
    {
        hp -= damage;

        if(hp<=0 && !isDead)
        {
            //사망처리 함수
        }
    }
}
