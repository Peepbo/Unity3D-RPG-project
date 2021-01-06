using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChest : MonoBehaviour, IDamagedState
{
    public int index;
    Animator anim;
    bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Damaged(int value)
    {
        if (isOpen) return;

        if (DungeonQuest.Instance.Quest == DungeonQuest.QUEST.KEY)
        {
            if (DungeonQuest.Instance.ClearNumber == index)
            {
                Debug.Log("키 획득!");
                DungeonQuest.Instance.Clear();
            }

            else Debug.Log("꽝!");
        }

        else Debug.Log("아이템 획득!");

        anim.SetTrigger("Open");

        isOpen = true;
    }
}
