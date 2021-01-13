using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonType
{
    Melee, Range
}
public enum EnemyName
{
    GOBLINMALE, GOBLINFEMALE, SHAMAN, GOLEM, SLAVEGOBLIN,
    SKELETON_W,SKELETON_K,SKELETON_S
}
public class MonsterType : MonoBehaviour
{
    public MonType type;
    public EnemyName enemyName;

    public MonType GetEnemyType()
    {
        return type;
    }
    public EnemyName GetEnemyName()
    {
        return enemyName;
    }

  

}
