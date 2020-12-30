using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall : MonoBehaviour, IAttackAble
{
    GameObject target;
    Transform firePos;
    float smoothTime = 0.3f;
    float yVelocity = 0.0f;

    float time = 0.0f;
    float spawn;

    public void init(GameObject newTarget, Transform fire, float spawnTime)
    {
        target = newTarget;
        spawn = spawnTime;
        firePos = fire;
       
    }
    IEnumerator attackCycle()
    {
        
        yield return new WaitForSeconds(1f);

       
    }

    public void attack()
    {
        time += Time.deltaTime;
        Vector3 _direction = (target.transform.position - transform.position).normalized;
        _direction.y = 0;
        // transform.rotation = Quaternion.LookRotation(_direction);

        if (time > spawn)
        {
            time = 0.0f;


            var _firePrefab = ObjectPool.SharedInstance.GetPooledObject("EnemySkill");


            _firePrefab.transform.position = firePos.position;
            _firePrefab.transform.rotation = firePos.rotation;
            _firePrefab.SetActive(true);


            float newPos = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref yVelocity, smoothTime);
            transform.position = new Vector3(transform.position.x, newPos, transform.position.z);
            transform.rotation = Quaternion.LookRotation(transform.position);

        }

    }
}
