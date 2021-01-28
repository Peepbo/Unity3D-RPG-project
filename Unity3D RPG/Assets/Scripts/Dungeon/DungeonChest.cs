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

        if (dq.quest != 0) GetComponent<Outline>().enabled = false;
    }

    public void Damaged(int value)
    {
        if (isOpen) return;

        if (dq.quest == 0)
        {
            if (dq.GetCorrectNumber() == index) dq.Clear();
        }

        anim.SetTrigger("Open");

        isOpen = true;

        gameObject.tag = "Untagged";
    }

    public void PlaySound()
    {
        SoundManager.Instance.SFXPlay2D("Chest_Open");
    }
}
