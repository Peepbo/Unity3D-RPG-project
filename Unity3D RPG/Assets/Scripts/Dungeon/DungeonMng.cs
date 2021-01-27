using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMng : Singleton<DungeonMng>
{
    protected DungeonMng() { }

    public int stage = 0;

    DungeonQuest dungeonQuest;

    public int countMelee;
    public int countRange;
    public int killCount = 0; //레인지, 밀리 몬스터 총 합

    public int playMap = -1;

    #region Achieve
    public Murder[] murderList = new Murder[5]
    {
        new Murder("Goblin",0),
        new Murder("OBGoblin",0),
        new Murder("Shamen",0),
        new Murder("Golem",0),
        new Murder("Chieftain",0)
    };
    #endregion

    private void Start()
    {
        Debug.Log("DungeonMng start");

        var obj = GameObject.FindWithTag("Quest");

        if (obj != null)
        {
            dungeonQuest = obj.GetComponent<DungeonQuest>();
        }
    }

    public int killMelee
    {
        get { return countMelee; }

        set
        {
            countMelee = value;

            LinkCount();

            if(dungeonQuest == null)
            {
                var obj = GameObject.FindWithTag("Quest");
                dungeonQuest = obj.GetComponent<DungeonQuest>();
            }


            if (dungeonQuest.quest == DungeonQuest.QUEST.MELEEALLKILL)
            {
                if (killMelee == dungeonQuest.meleeMonster)
                {
                    dungeonQuest.Clear();
                    //ClearCount();
                }
            }

            else if (dungeonQuest.quest == DungeonQuest.QUEST.ALLKILL)
            {
                if (killCount == dungeonQuest.maxMonster)
                {
                    dungeonQuest.Clear();
                    //ClearCount();
                }
            }
            
        }
    }

    public int killRange
    {
        get { return countRange; }

        set
        {
            //여기 앞에서 업적에 저장
            countRange = value;

            LinkCount();

            if (dungeonQuest == null)
            {
                var obj = GameObject.FindWithTag("Quest");
                dungeonQuest = obj.GetComponent<DungeonQuest>();
            }

            if (dungeonQuest.quest == DungeonQuest.QUEST.ALLKILL)
            {
                if (killCount == dungeonQuest.maxMonster)
                {
                    dungeonQuest.Clear();
                    //ClearCount();
                }
            }
        }
    }


    public void LinkCount()
    {
        killCount = countMelee + countRange;
    }

    //player
    public void ClearCount()
    {
        killCount = 0;
        countMelee = 0;
        countRange = 0;

        for (int i = 0; i < 5; i++) murderList[i].killCount = 0;
    }

    //player
    public void ResetStage()
    {
        stage = 0;
        playMap = -1;
    }
}
