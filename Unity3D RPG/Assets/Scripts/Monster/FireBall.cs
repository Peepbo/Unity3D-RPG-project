using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed = 3f;
    private int atk=35;
    Vector3 spawnPos;

    //awake : 최초 생성 즉 gameobject가 처음 켜질때만 작동되는듯
    //start : 스크립트 (즉 게임오브젝트)가 실행됬을 때

    private void Start() // 
    {
        spawnPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, spawnPos) > 15f)
        {
            gameObject.SetActive(false);
        }
    }
    public void setAtk(int value) { atk = value; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetDamage(atk);
            gameObject.SetActive(false);

        }
    }



}
