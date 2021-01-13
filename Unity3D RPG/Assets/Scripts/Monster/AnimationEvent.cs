using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public enum MONSTER
    {
        OBGOBLIN,
        NOBGOBLIN,
        SHAMAN,
        GOLEM,
        SPAWN
    }

    public MONSTER monster;

    OBGoblin obGoblin;
    Goblin nGoblin;
    Shaman shaman;
    Golem golem;
    SlaveGoblin spawnGoblin;
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
            case MONSTER.SPAWN:
                spawnGoblin = gameObject.GetComponentInParent<SlaveGoblin>();
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
            case MONSTER.SPAWN:
                spawnGoblin.GetRest();
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
            case MONSTER.SPAWN:
                spawnGoblin.ActiveMeshCol();
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
            case MONSTER.SPAWN:
                spawnGoblin.DeActiveMeshCol();
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
    
    public void ChangeAnim()
    {
        golem.ChangeIdle();
    }

    public void setDieSound()
    {
        golem.SetDieSound();
    }
    #endregion

}