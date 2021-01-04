using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonQuest : Singleton<DungeonQuest>
{
    protected DungeonQuest() { }

    public enum QUEST { KEY, LEVER }

    QUEST quest;

    //find key
    int correctNumber;

    //find key and turn on lever
    bool isClear = false;

    private void Start() 
    { 
        quest = (QUEST)Random.Range(0, 2);

        if (quest == QUEST.KEY) correctNumber = Random.Range(0, 4);
    }

    public void Clear() { isClear = true; }
}
