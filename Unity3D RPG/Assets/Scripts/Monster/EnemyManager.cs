using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;
    protected int hp;
    protected bool isDead;

    public Transform pivotCenter;
    public Transform Radar;



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
            isDead = true;
        }
    }

    public Vector3 GetRandomDirection()
    {
        float _ranX = UnityEngine.Random.Range(-1f, 1f);
        float _ranZ = UnityEngine.Random.Range(-1f, 1f);

        //Vector3 temp = new Vector3(_randomDir, 0, _randomDir).normalized;

        //위랑 아래랑 같다 ok? oo

        //Vector3 temp = new Vector3(_randomDir, 0, _randomDir);
        //temp.Normalize();
        //ran = 0.3f

        //return vector3(0.3f,0,1.0f);

        //-1~1 사이값으로 받을라고 normalized

        return new Vector3(_ranX, 0, _ranZ);
    }

}
