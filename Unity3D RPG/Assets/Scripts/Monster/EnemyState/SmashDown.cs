using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDown : MonoBehaviour,IAttackAble
{
    public BoxCollider collider;
    public bool isAttack;
    public void initVariable(BoxCollider col)
    {
        collider = col;
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        //isAttack = true;
    //        //is damaged
    //        other.gameObject.GetComponent<Player>().GetDamage(1);
    //    }
    //}

    public void initVariable(Animator anim)
    {
       
    }

    public void attack()
    {
        //박스 콜라이더를 키고
        //collider.enabled = true;

        //체크
        //if(collider.tri) 
        //if(isAttack) other.gameObject.GetComponent<Player>().GetDamage(1);

        //끄고 
        //collider.enabled = false;
        Debug.Log("내려찍기");
    }
}
