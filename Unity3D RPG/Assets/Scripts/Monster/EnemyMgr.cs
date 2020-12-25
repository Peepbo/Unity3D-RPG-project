using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMgr :MonoBehaviour
{
    protected CharacterController controller;
    protected GameObject target;
    

    private IMoveAble moveType;
    private IAttackAble attackType;

    [Range(5,10)]
    public float findRange;
    [Range(1,5)]
    public float speed;

    private int hp;
    public int maxHp;

    protected virtual void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
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


    //public Vector3 GetRandomDirection()
    //{
    //    float _ranX = Random.Range(-1f, 1f);
    //    float _ranZ = Random.Range(-1f, 1f);
    //    return new Vector3(_ranX, 0, _ranZ);
    //}

    //public void ChangeAniation(string newAnimation)
    //{
    //    if (animName == newAnimation) return;

    //    EnemyAnim.Play(newAnimation);
    //    animName = newAnimation;
    //}



}
