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

    #region KEY
    //find key
    int correctNumber = 0;
    #endregion

    #region KILL
    //kill monster
    public int maxMonster = 0;
    public int meleeMonster = 0;
    public int killCount = 0;
    #endregion

    bool isClear = false;

    public QUEST Quest { get { return quest; } }

    public TextMeshProUGUI text;

    public int GetCorrectNumber() { return correctNumber; }

    private void Awake()
    {
        isClear = false;

        var _allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        MonsterType mt = new MonsterType();

        switch (Quest)
        {
            case QUEST.KEY:
                correctNumber = Random.Range(0, 4);
                text.text = "던전 목표: 키 찾기";
                break;
            case QUEST.ALLKILL:
                text.text = "던전 목표: 모든 몬스터 처치하기";
                
                for (int i = 0; i < _allEnemys.Length; i++)
                {
                    if (_allEnemys[i].TryGetComponent<MonsterType>(out mt))
                    {
                        maxMonster++;
                    }
                }
                break;
            case QUEST.MELEEALLKILL:
                text.text = "던전 목표: 모든 근접 몬스터 처치하기";

                for (int i = 0; i < _allEnemys.Length; i++)
                {
                    if (_allEnemys[i].TryGetComponent<MonsterType>(out mt))
                    {
                        if (mt.GetEnemyType() == MonType.Melee) meleeMonster++;
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
}
