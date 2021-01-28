using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    ROAR

}


[System.Serializable]

public abstract class BossDB : MonoBehaviour
{
    [Header("State")]
    public string bossName;
    public int hpMax;
    protected int hp;
    public int atk;
    public float atkSpeed;
    public float atkDelay;
    protected float atkTime;
    public int def;
    public float moveSpeed;
    public int goldMin;
    public int goldMax;
    [Space]
    public ItemDropInfo[] itemDropInfo;
    
    private bool isStart;
    [HideInInspector]
    public BossState state;
    [Space]
    public GameObject minionFactory;
    public int minionMaxCount;
    [Space]
    public Transform[] spawnArea;
    protected float dieTime;
    [Space]
    //public GameObject[] FXfactory;
    protected bool isHit = false;
    protected bool isPlayerCri;
    protected bool isRoar = false;
    protected bool isSpawn = false;
    protected bool isDead = false;
    [Space]
    public Transform target;
    public CapsuleCollider weapon;
    protected List<GameObject> minions = new List<GameObject>();
    public GameObject itemBox;
    protected List<ItemInfo> item = new List<ItemInfo>();
    protected ItemInfo info = new ItemInfo();
    public Slider hpBar;

    public bool start { get { return isStart; } set { isStart = value; } }
    protected int GetDamage(float def, int atk) { float _damage = atk * (1.0f - def/100); return (int)_damage; }
}

