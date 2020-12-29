using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall : MonoBehaviour, IAttackAble
{
    GameObject target;
    GameObject firePrefab;
    Transform firePos;

    float time = 0.0f;
    float spawn;

    public void init(GameObject newTarget,GameObject prefab, Transform fire, float spawnTime)
    {
        target = newTarget;
        spawn = spawnTime;
        firePos = fire;
        firePrefab = prefab;
    }
    public void attack()
    {
        time += Time.deltaTime;

        Vector3 _direction = (target.transform.position - transform.position).normalized;
        _direction.y = 0;

        transform.rotation = Quaternion.LookRotation(_direction);

        if (time > spawn)
        {
            //firePrefab = ObjectPool.SharedInstance.GetPooledObject("FireBall");

            time = 0.0f;
            firePrefab.transform.position = firePos.position;
            firePrefab.transform.rotation = firePos.rotation;
            firePrefab.SetActive(true);

        }


    }
}
