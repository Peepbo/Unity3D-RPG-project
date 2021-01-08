using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonQuest : MonoBehaviour
{
    public enum QUEST { KEY, LEVER }

    public QUEST quest;
    //find key
    int correctNumber = 0;
    //find key and turn on lever
    bool isClear = false;

    public QUEST Quest { get { return quest; } }

    public TextMeshProUGUI text;

    public int GetCorrectNumber() { return correctNumber; }

    private void Awake()
    {
        Debug.Log("awake");

        isClear = false;

        quest = QUEST.KEY;

        if (quest == QUEST.KEY)
            correctNumber = Random.Range(0, 4);

        text.text = "던전 목표: 키 찾기";
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
