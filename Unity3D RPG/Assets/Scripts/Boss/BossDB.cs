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

[System.Serializable]
public class BossDB
{
    public string name;
    public int hp, hpMax;
    public int atk;
    public float atkSpeed;
    public int def;
    public float moveSpeed;
    public int goldMin;
    public int goldMax;
    public ItemDropInfo[] itemDropInfo;
}
