﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxColision : MonoBehaviour
{
    public bool isOn;
   
    BoxCollider boxCol;
    int damage = 0;
    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().GetDamage(damage);
            GetComponent<BoxCollider>().enabled = false;

            #region 01-11
            EffectManager.Instance.EffectActive(6,
                other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position),
                Quaternion.identity);
            #endregion

            isOn = false;
        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
