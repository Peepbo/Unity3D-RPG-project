﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public enum MONSTER
    {
        OBGOBLIN,
        NOBGOBLIN,
        SHAMAN,
        GOLEM
    }

    public MONSTER monster;

    OBGoblin obGoblin;
    Goblin nGoblin;
    Shaman shaman;
    Golem golem;

    private void Start()
    {
        switch (monster)
        {
            case MONSTER.OBGOBLIN:
                obGoblin = gameObject.GetComponentInParent<OBGoblin>();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin = gameObject.GetComponentInParent<Goblin>();
                break;
            case MONSTER.SHAMAN:
                shaman = gameObject.GetComponentInParent<Shaman>();
                break;
            case MONSTER.GOLEM:
                golem = gameObject.GetComponentInParent<Golem>();
                break;
        }
    }

    #region Goblin
    public void GetRest(MONSTER mon)
    {
        switch (mon)
        {
            case MONSTER.OBGOBLIN:
                obGoblin.GetRest();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin.GetRest();
                break;
         
        }
    }

    public void ActiveMeshCol(MONSTER mon)
    {
        switch (mon)
        {
            case MONSTER.OBGOBLIN:
                obGoblin.ActiveMeshCol();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin.ActiveMeshCol();
                break;
          
        }
       
    }

    public void DeActiveMeshCol(MONSTER mon)
    {
        switch (mon)
        {
            case MONSTER.OBGOBLIN:
                obGoblin.DeActiveMeshCol();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin.DeActiveMeshCol();
                break;
          
        }
    }
    #endregion

    #region Shaman
    public void Flame()
    {
        shaman.Flame();
    }
    #endregion

    #region Golem
    public void AttackCycle()
    {
        golem.AttackTarget();
    }
    
    #endregion

}