using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall : MonoBehaviour, IAttackAble
{
    GameObject target;
    Transform firePos;

    float time = 0.0f;
    float spawn;

    public void init(GameObject newTarget, Transform fire, float spawnTime)
    {
        target = newTarget;
        spawn = spawnTime;
        firePos = fire;
       
    }
    public void attack()
    {
        time += Time.deltaTime;

        Vector3 _direction = (target.transform.position - transform.position).normalized;
        _direction.y = 0;

        transform.rotation = Quaternion.LookRotation(_direction);

        if (time > spawn)
        {
            time = 0.0f;


            var _firePrefab = ObjectPool.SharedInstance.GetPooledObject("EnemySkill");


            _firePrefab.transform.position = firePos.position;
            _firePrefab.transform.rotation = firePos.rotation;
            _firePrefab.SetActive(true);
        }
    }
}
