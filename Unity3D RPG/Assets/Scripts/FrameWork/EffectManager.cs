using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    protected EffectManager() { }

    public void EffectActive(int index, Vector3 pos, Quaternion rot)
    {
        var _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", index);
        _obj.transform.position = pos;
        _obj.transform.rotation = rot;

        _obj.SetActive(true);
    }

    public void Hit(MonsterType monType, Vector3 pos, Quaternion rot)
    {
        //GOBLINMALE, GOBLINFEMALE, SHAMAN, GOLEM, SLAVEGOBLIN,
        //SKELETON_W,SKELETON_K,SKELETON_S, BOSS, CHEST, SWITCH

        //피 (GOBLINMALE, GOBLINFEMALE, SHAMAN, SLAVEGOBLIN, BOSS)

        //돌 (GOLEM)

        //나무 (CHEST)

        //전기 (SWITCH)

        //Debug.Log(monType.type);
        //Debug.Log(monType.enemyName);

        GameObject _obj = null;

        switch (monType.type)
        {
            case MonType.Melee:

                if (monType.enemyName.Equals(EnemyName.GOLEM))
                {
                    //스톤
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 13);
                    //Debug.Log("Stone" + _obj.name);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                else
                {
                    //블러드
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                    //Debug.Log("Blood" + _obj.name);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                break;
            case MonType.Range:
                //샤먼

                //블러드
                _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                //Debug.Log("Blood" + _obj.name);
                _obj.transform.position = pos;
                _obj.transform.rotation = rot;

                _obj.SetActive(true);

                break;
            case MonType.Other:

                if(monType.enemyName.Equals(EnemyName.CHEST))
                {
                    //상자
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 14);
                    //Debug.Log("Chest" + _obj.name);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                else if(monType.enemyName.Equals(EnemyName.SWITCH))
                {
                    //스위치
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 16);
                    //Debug.Log("Switch" + _obj.name);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                else if(monType.enemyName.Equals(EnemyName.BOX))
                {
                    //나무박스
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 15);
                    //Debug.Log("wood Box" + _obj.name);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                break;
        }
    }
}
