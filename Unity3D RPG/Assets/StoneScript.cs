using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    Rigidbody rigid;
    GameObject effect;
    Renderer mat;

    public float gravity = 9.7f;
    public float fadeTime = 5f;
    public int damage = 30;
    public MeshCollider col;

    bool oneTime = false;
    bool fadeOn = false;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StopAllCoroutines();
            col.enabled = false;
            fadeOn = true;

            other.GetComponent<Player>().GetDamage(damage);

            EffectManager.Instance.EffectActive(4, transform.position, Quaternion.identity);
            EffectManager.Instance.EffectActive(5, transform.position, Quaternion.identity);

            effect.SetActive(false);
        }

        else if (other.tag == "Enemy")
        {
            StopAllCoroutines();
            col.enabled = false;
            fadeOn = true;

            other.GetComponent<IDamagedState>().Damaged(damage);

            EffectManager.Instance.EffectActive(4, transform.position, Quaternion.identity);
            EffectManager.Instance.EffectActive(5, transform.position, Quaternion.identity);

            effect.SetActive(false);
        }
    }

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        effect = transform.GetChild(0).gameObject;

        mat = gameObject.GetComponent<Renderer>();
        Color _color = mat.material.color;

        mat.material.color = _color;
    }

    private void Update()
    {
        if (rigid.useGravity == false) return;
        //Debug.DrawRay(transform.position, Vector3.down * 2.5f, Color.red);

        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 2.5f))
        {
            if (oneTime == false)
            {
                oneTime = true;
                StartCoroutine(fadeObject());

                effect.SetActive(true);
                EffectManager.Instance.EffectActive(3, transform.GetChild(0).position, Quaternion.identity);
            }

            if (fadeOn)
            {
                Color _endColor = mat.material.color;
                _endColor.a = 0;
                mat.material.color = Color.Lerp(mat.material.color, _endColor, Time.deltaTime * 3f);

            }
        }

        else rigid.AddForce(Vector3.down * gravity);
    }

    IEnumerator fadeObject()
    {
        yield return new WaitForSeconds(fadeTime);
        fadeOn = true;
    }

}
