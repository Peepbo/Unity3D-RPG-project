using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMgr : MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;
    protected Animator anim;
    protected float gravity = -9.81f;
    public int damage;

    private IMoveAble moveType;
    private IAttackAble attackType;

    [Range(5, 15)]
    public float findRange;
    [Range(1, 3)]
    public float attackRange;
    [Range(1, 5)]
    public float speed;

    private int hp;
    public int maxHp;

    bool isDead;

    protected virtual void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
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

    public Vector3 GetRandomDirection()
    {
        float _ranX = Random.Range(-1f, 1f);
        float _ranZ = Random.Range(-1f, 1f);
        return new Vector3(_ranX, 0, _ranZ);
    }

    //// Start is called before the first frame update

    //#region protected 
    //protected CharacterController controller;
    //protected GameObject target;
    //protected int hp;
    //protected bool isDead;
    //protected float gravity;


    //protected Animator EnemyAnim;
    //protected string animName = "";

    //#endregion

    //public Transform pivotCenter;
    //public Transform radar;

    ////shared stat
    //public int maxHp = 10;
    //public float def = 1f;
    //[Range(1, 5)]
    //public float speed = 1;
    //[Range(5, 15)]
    //public float findRange;
    //[Range(1, 10)]
    //public float attackRange;



    //private void Awake()
    //{
    //    controller = GetComponent<CharacterController>();
    //    target = GameObject.FindWithTag("Player");
    //    EnemyAnim = GetComponent<Animator>();

    //    hp = maxHp;
    //}




    //public void ChangeAniation(string newAnimation)
    //{
    //    if (animName == newAnimation) return;

    //    EnemyAnim.Play(newAnimation);
    //    animName = newAnimation;
    //}



}
