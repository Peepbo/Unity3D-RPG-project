using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMgr : MonoBehaviour
{
    private IMoveAble moveType;

    protected NavMeshAgent AI;                  //Monster AI Move용
    protected NavMeshPath path;
    protected CharacterController controller;   //충돌처리용
    protected Player player;                    //player script l
    protected GameObject target;                //distance , direction 체크용
    protected Animator anim;

    protected bool isHit;                       //player 강공격 상태에서의 데미지 처리용
    protected bool isDamaged;                   //GetDamage 제한용
    protected bool isDead;                      //몬스터 사망처리
    protected float disappearTime;              //사망 후 프리팹 비활성화 타임

    public GameObject ItemBox;                  //몬스터가 드롭하는 Item정보를 담고 있을 ItemBox


    //stat
    protected int maxHp;            //최대 체력
    protected int hp;               //현재 체력
    protected int atk;              //공격력
    protected float def;            //방어력
    public float speed;             //NavMeshAgent 속도

    public float angle;             //Goblin종족 시야범위 판정
    [Range(5, 30)]
    public float findRange;         //타겟 확인 가능 범위
    [Range(1, 10)]
    public float attackRange;       //공격 가능 범위
    [Range(1, 5)]
    public float observeRange;      //Observe 기능을 사용하는 몬스터의 감시 범위

    //Item
    protected int minGold;
    protected int maxGold;
    protected int currency;


    protected int[] itemKind = new int[3];
    protected ItemInfo drop = new ItemInfo();
    protected List<ItemInfo> item = new List<ItemInfo>();

    protected virtual void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");

        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            AI = gameObject.GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
        }
        player = target.GetComponent<Player>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag.Equals("Animation"))
                anim = transform.GetChild(i).GetComponent<Animator>();
        }


        isDead = false;
    }

    public void Move()
    {
        moveType.move();
    }

    public void setMoveType(IMoveAble newMoveType)
    {
        //IMoveAble 인터페이스 상속받아 구현된 스크립트를 받아온다.
        this.moveType = newMoveType;
    }

    public abstract void Die();


    protected IEnumerator LookBack(float time)
    {
        //현재 흐르는 시간이 time이 될 때까지 타겟을 바라보도록 회전
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;

            Vector3 _direction = (target.transform.position - transform.position).normalized;
            _direction.y = 0;
            Quaternion _targetRotation = Quaternion.LookRotation(_direction);

            //Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime);
            Quaternion _nextRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, t / time);

            transform.localRotation = _nextRotation;

            yield return null;
        }

    }
    //public Vector3 GetRandomDirection()
    //{
    //    float _ranX = Random.Range(-1f, 1f);
    //    float _ranZ = Random.Range(-1f, 1f);
    //    return new Vector3(_ranX, 0, _ranZ);
    //}

}
