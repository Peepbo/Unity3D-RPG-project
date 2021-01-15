using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_T : MonoBehaviour
{
    private float speed = 5f;
    private int atk = 35;
    Vector3 spawnPos;

    //awake : 최초 생성 즉 gameobject가 처음 켜질때만 작동되는듯
    //start : 스크립트 (즉 게임오브젝트)가 실행됬을 때

    //private void Start() // 
    //{
    //    spawnPos = transform.position;
    //}

    TutorialMng tm;

    private void OnEnable()
    {
        tm = GameObject.Find("TutorialMng").GetComponent<TutorialMng>();
        spawnPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnPos) > 15f)
        {
            #region 01-13
            EffectManager.Instance.EffectActive(7, transform.position, Quaternion.identity);
            #endregion
            gameObject.SetActive(false);
        }
    }
    public void setAtk(int value) { atk = value; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().GetDamage(-1);

                var _obj = other.gameObject.GetComponent<Player>();
                bool _check = _obj.comboAtk.animator.GetBool("isGuardHit");

                if(_check)
                {
                    tm.ChangeQuest(4);
                    tm.KillMonster();
                }

                #region 01-11
                EffectManager.Instance.EffectActive(7,
                    other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                    Quaternion.identity);
                #endregion
            }
            gameObject.SetActive(false);
            #region 01-13
            EffectManager.Instance.EffectActive(7,
                other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                Quaternion.identity);
            #endregion
        }
    }
}
