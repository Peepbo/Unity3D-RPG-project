using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    Goblin goblin;

    private void Start()
    {
        goblin = gameObject.GetComponentInParent<Goblin>();
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
}