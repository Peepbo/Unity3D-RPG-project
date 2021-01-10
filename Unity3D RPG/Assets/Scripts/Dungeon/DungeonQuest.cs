using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonQuest : MonoBehaviour
{
    public enum QUEST { 
        KEY,
        ALLKILL,
        MELEEALLKILL
    }

    public QUEST quest;
    //find key
    int correctNumber = 0;
    //kill monster
    public int killCount = 0;
    public int maxMonster = 0;

    public int obstacle = 0;

    bool isClear = false;

    public QUEST Quest { get { return quest; } }

    public TextMeshProUGUI text;

    public int GetCorrectNumber() { return correctNumber; }

    private void Awake()
    {
        //Debug.Log("awake");

        isClear = false;

        //quest = QUEST.KEY;

        //if (quest == QUEST.KEY)
        //{
        //    correctNumber = Random.Range(0, 4);
        //    text.text = "던전 목표: 키 찾기";
        //}
        //else if (quest == QUEST.ALLKILL)
        //{
        //    text.text = "던전 목표: 모든 적 처치하기";
        //}

        switch (Quest)
        {
            case QUEST.KEY:
                correctNumber = Random.Range(0, 4);
                text.text = "던전 목표: 키 찾기";
                break;
            case QUEST.ALLKILL:
                text.text = "던전 목표: 모든 몬스터 처치하기";
                //maxMonster = GameObject.FindGameObjectsWithTag("Enemy").Length - obstacle;
                break;
            case QUEST.MELEEALLKILL:
                text.text = "던전 목표: 모든 근접 몬스터 처치하기";
                break;
        }
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.U)) Clear();
    //}

    public void Clear() 
    { 
        isClear = true;

        transform.GetChild(0).gameObject.SetActive(true);

        text.text = "던전 클리어!";
    }
}
