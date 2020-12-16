using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;

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
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
            lookPos.y = 0;

            transform.rotation = Quaternion.LookRotation(lookPos);

            lookPos.Normalize();

            controller.Move(lookPos * speed * Time.deltaTime);
        }
    }
    protected abstract void attack();

}
