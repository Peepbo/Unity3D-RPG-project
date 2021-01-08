using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemDropInfo
{
    public string itemName ;
    public int itemID;
    public int itemDropCount;
}

public enum BossState
{
    IDLE,
    COMBATIDLE,
    ATK,
    RUN,
    HIT,
    DIE,

}
public enum BossATKPattern
{
    THREEATK,
    THUMP,
    SPAWN,
    END
}

[System.Serializable]
public abstract class BossDB : MonoBehaviour
{
    public string bossName;
    public int hp, hpMax;
    public int atk;
    public float atkSpeed;
    public float atkDelay;
    public float atkTime;
    public int def;
    public float moveSpeed;
    public int goldMin;
    public int goldMax;
    public ItemDropInfo[] itemDropInfo;
    private bool isStart;
    public BossState state;
    public GameObject minionFactory;
    public int minionMaxCount;
    public Transform[] spawnArea;
    public float dieCount;
    public GameObject[] FXfactory;
   public bool start { get { return isStart; } set { isStart = value; } }
}

