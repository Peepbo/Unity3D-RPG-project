using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSkel : MonoBehaviour
{
    SkeletonW skel;

    void Start()
    {
        skel = transform.GetComponentInParent<SkeletonW>();
    }

    public void SetRandomNum()
    {
        skel.RandomAttack();
    }

    public void GetRest()
    {
        skel.GetRest();
    }

    public void Active()
    {
        skel.Active();
    }
    
    public void DeActive()
    {
        skel.DeActive();
    }
}
