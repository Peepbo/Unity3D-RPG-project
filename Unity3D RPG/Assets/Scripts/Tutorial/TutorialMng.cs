using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMng : MonoBehaviour
{
    bool isStart = false;
    BoxCollider col;

    public Chat chat;

    public bool[] questCheck = new bool[7];
    public int questNumber = 0;

    //shamen
    public GameObject monster;

    public GameObject player;

    #region begin
    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isStart = true;
            col.enabled = false;

            monster.SetActive(true);
            questCheck[questNumber] = true;
            questNumber++;
            chat.NextQuest();
        }
    }
    #endregion

    public void ChangeQuest(int index)
    {
        questCheck[questNumber] = true;
        questNumber++;
        chat.NextQuest();
    }

    public void KillMonster()
    {
        monster.transform.GetChild(1).GetComponent<ShamanT>().Damaged(5);
    }

    public void Update()
    {
        if(questNumber == 4)
        {
            if (player.GetComponent<Player>().isDash) ChangeQuest(5);
        }
    }
}