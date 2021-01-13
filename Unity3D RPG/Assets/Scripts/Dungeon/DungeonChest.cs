using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChest : MonoBehaviour, IDamagedState
{
    public GameObject questObj;
    DungeonQuest dq;

    public int index;
    Animator anim;
    bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        dq = questObj.GetComponent<DungeonQuest>();
    }

    public void Damaged(int value)
    {
        if (isOpen) return;

        //DungeonQuest.QUEST.MELEEALLKILL
      
     

        if (dq.quest == 0)
        {
            if (dq.GetCorrectNumber() == index)
            {
                Debug.Log("키 획득!");
                dq.Clear();
            }

            else Debug.Log("꽝!");
        }

        anim.SetTrigger("Open");

        isOpen = true;

        gameObject.tag = "Untagged";
    }
}
