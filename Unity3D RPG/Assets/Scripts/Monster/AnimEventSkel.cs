using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSkel : MonoBehaviour
{
    public enum SkelType
    {
        WARRIOR,
        KNIGHT,
        SLAVE
    }

    public SkelType skeleton;

    SkeletonW skel;
    SkeletonK knight;
    SkeletonS slave;

    void Start()
    {
        skel = transform.GetComponentInParent<SkeletonW>();
        knight = transform.GetComponentInParent<SkeletonK>();
        slave = transform.GetComponentInParent<SkeletonS>();
    }

    public void SetRandomNum(SkelType mon)
    {
        switch (mon)
        {
            case SkelType.WARRIOR:
                skel.RandomAttack();
                break;
            case SkelType.KNIGHT:
                knight.RandomAttack();
                break;
            case SkelType.SLAVE:
                slave.RandomAttack();
                break;
        }

    }

    public void GetRest(SkelType mon)
    {
        switch (mon)
        {
            case SkelType.WARRIOR:
                skel.GetRest();
                break;
            case SkelType.KNIGHT:
                knight.GetRest();
                break;
            case SkelType.SLAVE:
                slave.GetRest();
                break;
        }

    }

    public void Active(SkelType mon)
    {
        switch (mon)
        {
            case SkelType.WARRIOR:
                skel.Active();
                break;
            case SkelType.KNIGHT:
                knight.Active();
                break;
            case SkelType.SLAVE:
                slave.Active();
                break;

        }

    }

    public void DeActive(SkelType mon)
    {
        switch (mon)
        {
            case SkelType.WARRIOR:
                skel.DeActive();
                break;
            case SkelType.KNIGHT:
                knight.DeActive();
                break;
            case SkelType.SLAVE:
                slave.DeActive();
                break;
        }
    }
}
