﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStat : MonoBehaviour
{
    
    public int maxHp;
    public int hp;
    public int atk;
    public float def;
    public float speed;
    
    public float angle;
    public float findRange;
    public float attackRange;
    public float observeRange;

    public int minGold;
    public int maxGold;
    private string[] loot = new string[3];
    private int[] kindId = new int[3];



    private void Start()
    {
       
    }
    

}


