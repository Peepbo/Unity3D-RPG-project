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
        GameObject _obj = null;

        switch (monType.type)
        {
            case MonType.Melee:

                if(monType.name.Equals("GOBLINMALE") || monType.name.Equals("GOBLINFEMALE") ||
                   monType.name.Equals("SLAVEGOBLIN") || monType.name.Equals("BOSS"))
                {
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                else //골램
                {
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                break;
            case MonType.Range:
                //샤먼

                _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                _obj.transform.position = pos;
                _obj.transform.rotation = rot;

                _obj.SetActive(true);

                break;
            case MonType.Other:

                if(monType.name.Equals("CHEST"))
                {
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                else
                {
                    _obj = ObjectPool.SharedInstance.GetPooledObject("Effect", 1);
                    _obj.transform.position = pos;
                    _obj.transform.rotation = rot;

                    _obj.SetActive(true);
                }

                break;
        }
    }
}
