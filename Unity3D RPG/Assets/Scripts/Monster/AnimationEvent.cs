﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public enum MONSTER
    {
        GOBLIN,
        SHAMAN,
        GOLEM
    }

    public MONSTER monster;

    Goblin goblin;
    Shaman shaman;
    MiniGolem golem;

    private void Start()
    {
        switch (monster)
        {
            case MONSTER.GOBLIN:
                goblin = gameObject.GetComponentInParent<Goblin>();
                break;
            case MONSTER.SHAMAN:
                shaman = gameObject.GetComponentInParent<Shaman>();
                break;
            case MONSTER.GOLEM:
                golem = gameObject.GetComponentInParent<MiniGolem>();
                break;
        }
    }

    #region Goblin
    public void GetRest()
    {
        goblin.GetRest();
    }

    public void ActiveMeshCol()
    {
        goblin.ActiveMeshCol();
    }

    public void DeActiveMeshCol()
    {
        goblin.DeActiveMeshCol();
    }
    #endregion

    #region Shaman
    public void Flame()
    {
        shaman.Flame();
    }
    #endregion

    #region Golem
    public void GetRandomNum()
    {
        golem.GetRandomNum();
    }
    #endregion

}