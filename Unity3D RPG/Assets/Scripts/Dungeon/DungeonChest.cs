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
        Debug.Log("damaged 0");

        if (isOpen) return;
        Debug.Log("damaged 1");

        if (dq.Quest == DungeonQuest.QUEST.KEY)
        {
            if (dq.GetCorrectNumber() == index)
            {
                Debug.Log("키 획득!");
                dq.Clear();
            }

            else Debug.Log("꽝!");
        }

        else Debug.Log("아이템 획득!");

        anim.SetTrigger("Open");

        isOpen = true;

        Debug.Log("damaged 2");
    }
}
