using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSkel : MonoBehaviour
{
    public enum SkelType
    {
        WARRIOR,
        KNIGHT
    }

    public SkelType skeleton;

    SkeletonW skel;
    SkeletonK knight;

    void Start()
    {
        skel = transform.GetComponentInParent<SkeletonW>();
        knight = transform.GetComponentInParent<SkeletonK>();
    }

    public void SetRandomNum(SkelType mon)
    {
        switch (mon)
        {
            case SkelType.WARRIOR:
                skel.RandomAttack();
                break;
            case SkelType.KNIGHT:
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
                break;

        }
    }
}
