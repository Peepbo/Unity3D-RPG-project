using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed = 5f;
    private int atk = 40;
    Vector3 spawnPos;

    private void OnEnable()
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnPos) > 15f)
        {
            #region 01-13 Effect
            EffectManager.Instance.EffectActive(7, transform.position, Quaternion.identity);
            #endregion
            gameObject.SetActive(false);
        }
    }

  //  public void setAtk(int value) { atk = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Enemy"))
        {
            if (other.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<Player>().GetDamage(atk);

                #region 01-11 Effect
                EffectManager.Instance.EffectActive(7,
                    other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                    Quaternion.identity);
                #endregion
            }
            if (other.transform.name.Equals("DamageBox")) return;
            gameObject.SetActive(false);

            #region 01-13 Effect
            EffectManager.Instance.EffectActive(7,
                other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                Quaternion.identity);
            #endregion
        }
    }



}
