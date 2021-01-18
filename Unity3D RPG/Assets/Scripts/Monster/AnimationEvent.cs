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
        SPAWN,
        TRAINING
    }

    public MONSTER monster;

    OBGoblin obGoblin;
    Goblin nGoblin;
    Shaman shaman;
    Golem golem;
    SlaveGoblin spawnGoblin;
    ShamanT trainingShaman;

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
            case MONSTER.TRAINING:
                trainingShaman = gameObject.GetComponentInParent<ShamanT>();
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

    public void ActiveCollider(MONSTER mon)
    {
        switch (mon)
        {
            case MONSTER.OBGOBLIN:
                obGoblin.ActiveCollider();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin.ActiveCollider();
                break;
            case MONSTER.SPAWN:
                spawnGoblin.ActiveCollider();
                break;
          
        }
       
    }

    public void DeActiveCollider(MONSTER mon)
    {
        switch (mon)
        {
            case MONSTER.OBGOBLIN:
                obGoblin.DeActiveCollider();
                break;
            case MONSTER.NOBGOBLIN:
                nGoblin.DeActiveCollider();
                break;
            case MONSTER.SPAWN:
                spawnGoblin.DeActiveCollider();
                break;

        }
    }
    #endregion

    #region Shaman
    public void Flame()
    {
        shaman.Flame();
    }

    public void FireBall()
    {
        trainingShaman.Fire();
    }

    public void ReadyFire()
    {
        trainingShaman.ChangeReady();
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