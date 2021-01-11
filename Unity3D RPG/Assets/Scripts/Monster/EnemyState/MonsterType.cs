using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonType
{
    Melee, Range
}
public class MonsterType : MonoBehaviour
{
    public MonType type;

    public MonType GetEnemyType()
    {
        return type;
    }

}
