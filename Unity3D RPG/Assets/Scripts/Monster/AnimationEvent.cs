using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public enum MONSTER
    {
        GOBLIN,
        SHAMAN
    }

    public MONSTER monster;

    Goblin goblin;
    Shaman shaman;

    private void Start()
    {
        switch (monster)
        {
            case MONSTER.GOBLIN:
                goblin = gameObject.GetComponentInParent<Goblin>();
                break;
            case MONSTER.SHAMAN:
                shaman = gameObject.GetComponentInParent<Shaman>();
                break;
        }
    }

    public void GetRest()
    {
        goblin.GetRest();
    }

    public void ActiveMeshCol()
    {
        goblin.ActiveMeshCol();
    }

    public void DeActiveMeshCol()
    {
        goblin.DeActiveMeshCol();
    }

    public void Flame()
    {
        shaman.Flame();
    }
}