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
}
