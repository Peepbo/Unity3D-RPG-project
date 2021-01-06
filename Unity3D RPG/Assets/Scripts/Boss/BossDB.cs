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

[System.Serializable]
public abstract class BossDB : MonoBehaviour
{
    public string bossName;
    public int hp, hpMax;
    public int atk;
    public float atkSpeed;
    public int def;
    public float moveSpeed;
    public int goldMin;
    public int goldMax;
    public ItemDropInfo[] itemDropInfo;
    private bool isStart;
    public BossState bossState;
   public bool start { get { return isStart; } set { isStart = value; } }
}
