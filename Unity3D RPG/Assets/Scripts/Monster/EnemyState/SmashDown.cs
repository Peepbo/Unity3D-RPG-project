using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDown : MonoBehaviour,IAttackAble
{
    GameObject weapon;
    AxColision axCol;
    MeshCollider meshCol;
    Animator anim;

    public bool isAttack;
    //int damage;
    public void initVariable(GameObject weaponType, Animator animType)
    {
        weapon = weaponType;
        anim = animType;

        meshCol = weapon.GetComponent<MeshCollider>();
        axCol = weapon.GetComponent<AxColision>();
    }

    public void attack()
    {
        float _per = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        //meshCol.

        if (isAttack)
        {
            if (_per > 0.5f && _per < 0.55f)
            {
                meshCol.enabled = true;
            }

            else if(_per > 0.75f)
            {
                meshCol.enabled = false;
            }


        }

        else if(_per < 0.1f)
        {
            isAttack = true;
        }
    }
}
