using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : EnemyMgr, IDamagedState
{

    ObservingMove observe;
    ViewingAngle viewAngle;



    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        observe = gameObject.AddComponent<ObservingMove>();
        viewAngle = gameObject.AddComponent<ViewingAngle>();
    }

    void Update()
    {
        Vector3 _transfrom = transform.position;
        if (!controller.isGrounded)
        {
            _transfrom.y += gravity * Time.deltaTime;
            //controller.Move(_transfrom*)
        }
    }

    public void Damaged()
    {

    }
}
