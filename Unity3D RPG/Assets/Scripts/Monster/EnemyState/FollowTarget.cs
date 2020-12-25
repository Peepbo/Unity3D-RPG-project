using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour, IMoveAble
{
    CharacterController controller;
    GameObject target;
    float speed;

    public void initVariable(CharacterController cc, GameObject destination, float followSpeed)
    {
        this.controller = cc;
        this.target = destination;
        this.speed = followSpeed;
    }
    public void move()
    {

        Vector3 _transform2Target = target.transform.position - transform.position;
        Vector3 _direction = _transform2Target.normalized;
        // float _distance = _transform2Target.magnitude;

        _direction.y = 0;
        transform.rotation = Quaternion.LookRotation(_direction);

        controller.Move(_direction * speed * Time.deltaTime);

    }
}
