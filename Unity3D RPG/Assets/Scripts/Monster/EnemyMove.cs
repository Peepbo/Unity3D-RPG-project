using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class EnemyMove : MonoBehaviour
{

    CharacterController controller;
    GameObject target;

    [Range(1, 5)]
    public float speed = 1;

    [Range(1, 10)]
    public float findRange;

   
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= findRange)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            //target의 y축이 어디에 있든, 현재 오브젝트가 바라보고 있는 건 y=0 위치
            lookPos.y = 0;

            transform.rotation = Quaternion.LookRotation(lookPos);

            lookPos.Normalize();

            controller.Move(lookPos * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.Lerp(Color.cyan, Color.red, Mathf.PingPong(Time.time,0.5f));
        Gizmos.DrawWireSphere(transform.position, findRange);
    }

    //내일 할 일 원거리 몬스터 (오브젝트 풀링 사용) 제발요...
    //원거리 몬스터가 근거리 몬스터에 닿으면 근거리 몬스터 던지기
}

