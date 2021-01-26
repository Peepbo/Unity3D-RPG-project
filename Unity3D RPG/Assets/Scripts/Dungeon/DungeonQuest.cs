using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DungeonQuest : MonoBehaviour
{
    public bool keyQuest = false;
    public bool allKillQuest = false;
    public bool meleeAllKillQuest = false;

    public enum QUEST { 
        KEY,
        ALLKILL,
        MELEEALLKILL
    }

    public QUEST quest;

   
    #region KEY
    //find key
    public int boxNumber = 4;
    int correctNumber = 0;
    #endregion

    #region KILL
    //kill monster
    public int maxMonster = 0;
    public int meleeMonster = 0;
    #endregion

    bool isClear = false;

    public Text text;

    public int GetCorrectNumber() { return correctNumber; }

    private void Awake()
    {
        isClear = false;

        var _allEnemys = GameObject.FindGameObjectsWithTag("Enemy");

        MonsterType mt;

        quest = ChooseQuest();

        switch (quest)
        {
            case QUEST.KEY:
                text.text = "던전 목표: 키 찾기";

                correctNumber = Random.Range(0, boxNumber);
                break;
            case QUEST.ALLKILL:
                text.text = "던전 목표: 모든 몬스터 처치하기";
                
                for (int i = 0; i < _allEnemys.Length; i++)
                {
                    if (_allEnemys[i].TryGetComponent(out mt))
                    {
                        if (mt.type == MonType.Other) continue;

                        maxMonster++;
                    }
                }
                break;
            case QUEST.MELEEALLKILL:
                text.text = "던전 목표: 모든 근접 몬스터 처치하기";

                for (int i = 0; i < _allEnemys.Length; i++)
                {
                    if (_allEnemys[i].TryGetComponent(out mt))
                    {
                        if (mt.GetEnemyType() == MonType.Melee)
                        {
                            meleeMonster++;
                        }
                    }
                }
                break;
        }
    }

    public void Clear() 
    { 
        isClear = true;

        transform.GetChild(0).gameObject.SetActive(true);

        text.text = "던전 클리어!";
    }

    QUEST ChooseQuest()
    {
        bool[] _ck = new bool[3] { keyQuest, allKillQuest, meleeAllKillQuest };

        List<int> _list = new List<int>();

        for(int i = 0; i < 3; i++)
        {
            if (_ck[i]) _list.Add(i);
        }

        return (QUEST)_list[Random.Range(0, _list.Count)];
    }
}
