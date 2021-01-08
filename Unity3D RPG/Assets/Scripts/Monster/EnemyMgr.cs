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
    protected Player player;                    //player script
    protected GameObject target;                //distance , direction 체크용
    protected Animator anim;

    protected bool isHit;
    protected bool isDamaged;
    protected bool isDead;
    protected float disappearTime;


    public GameObject coinEffect;


    //stat
    public int maxHp;
    protected int hp;
    protected int atk;
    protected float def;
    public float speed;

    public float angle;
    [Range(5, 30)]
    public float findRange;
    [Range(1, 10)]
    public float attackRange;
    [Range(1, 5)]
    public float observeRange;

    protected int minGold;
    protected int maxGold;
    protected int currency;



    protected virtual void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<Player>();

        for (int i = 0; i < transform.childCount; i++)
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

    public abstract void DropCoin(int min, int max);

    protected IEnumerator LookBack()
    {
        float t = 0f;
        while (t < 0.7f)
        {
            t += Time.deltaTime;

            Vector3 _direction = (target.transform.position - transform.position).normalized;
            _direction.y = 0;
            Quaternion _targetRotation = Quaternion.LookRotation(_direction);

            //Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime);
            Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, t / 1.5f);

            transform.localRotation = _nextRotation;

            yield return null;
        }

    }
    public Vector3 GetRandomDirection()
    {
        float _ranX = Random.Range(-1f, 1f);
        float _ranZ = Random.Range(-1f, 1f);
        return new Vector3(_ranX, 0, _ranZ);
    }

}
