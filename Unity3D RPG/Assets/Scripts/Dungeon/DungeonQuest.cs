using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonQuest : Singleton<DungeonQuest>
{
    protected DungeonQuest() { }

    public enum QUEST { KEY, LEVER }

    public QUEST quest;
    //find key
    int correctNumber;
    //find key and turn on lever
    bool isClear = false;

    public QUEST Quest { get { return quest; } }

    public int ClearNumber { get { return correctNumber; } }

    public TextMeshProUGUI text;

    private void Start() 
    { 
        //quest = (QUEST)Random.Range(0, 2);

        if (quest == QUEST.KEY)
            correctNumber = Random.Range(0, 4);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) Clear();
    }

    public void Clear() 
    { 
        isClear = true;

        transform.GetChild(0).gameObject.SetActive(true);

        text.text = "던전 클리어!";
    }
}
