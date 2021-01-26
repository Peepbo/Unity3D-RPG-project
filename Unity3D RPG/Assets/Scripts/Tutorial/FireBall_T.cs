using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_T : MonoBehaviour
{
    private float speed = 5f;
    Vector3 spawnPos;

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
            EffectManager.Instance.EffectActive(7, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy") == false)
        {
            if (other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<Player>().GetDamage(0);

                var _obj = other.gameObject.GetComponent<Player>();
                bool _check = _obj.comboAtk.animator.GetBool("isGuardHit");

                if(_check)
                {
                    tm.ChangeQuest();
                    tm.KillMonster();
                }

                EffectManager.Instance.EffectActive(7,
                    other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                    Quaternion.identity);
            }
            gameObject.SetActive(false);

            EffectManager.Instance.EffectActive(7,
                other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                Quaternion.identity);
        }
    }
}
