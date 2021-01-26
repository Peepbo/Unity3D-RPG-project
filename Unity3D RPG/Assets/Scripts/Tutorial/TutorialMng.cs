using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMng : MonoBehaviour
{
    public Chat chat;
    public int questNumber = 0;

    //shamen
    public GameObject monster;
    //Box
    public GameObject box;
    //Player
    public GameObject player;

    BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            col.enabled = false;

            SoundManager.Instance.SFXPlay2D("ItemBox_Appear");
            monster.SetActive(true);
            questNumber++;
            chat.NextQuest();
        }
    }

    public void ChangeQuest()
    {
        questNumber++;
        chat.NextQuest();

        if(questNumber == 5) StartCoroutine(DelayBox());
    }

    IEnumerator DelayBox()
    {
        yield return new WaitForSeconds(3.7f);
        CreateBox();
    }

    public void KillMonster()
    {
        monster.transform.GetChild(1).GetComponent<ShamanT>().Damaged(5);
    }

    public void CreateBox()
    {
        SoundManager.Instance.SFXPlay2D("ItemBox_Appear");
        box.SetActive(true);
    }

    public void Update()
    {
        if(questNumber == 4)
        {
            if (player.GetComponent<Player>().isDash) ChangeQuest();
        }
    }
}