using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Chat : MonoBehaviour
{
    public Text quest;
    public RectTransform texts;
    public int chatIndex = 0;

    Vector3 savePos;

    string[] quests =
    {
        "컨트롤러를 사용하여 NPC에 도달하기",
        "기본 공격으로 몬스터 처치하기",
        "강 공격으로 몬스터 처치하기",
        "몬스터 공격 방어하기",
        "굴러서 해당 위치로 이동하기",
        "시점을 변경하여 특정 오브젝트 파괴하기",
        "포탈 타고 마을로 이동하기",
    };

    //2,1,1,3,2,2,2
    int[] questChats = { 1, 1, 1, 3, 2, 3, 2 };

    public TutorialMng tutorialMng;
    int questIndex = 0;
    float delay = 0f;

    bool active = true;

    Animator anim;

    public const float distance = 78f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        savePos = texts.position;
        savePos.y += distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(active) moveChat();
    }

    public void NextQuest()
    {
        questIndex++;
        active = true;
        savePos.y += distance;
        texts.position = savePos;
        anim.SetBool("Close", false);
    }

    public void moveChat()
    {
        if (chatIndex == questChats[questIndex])
        {
            delay += Time.deltaTime;

            if(delay > 2)
            {
                delay = 0;

                Debug.Log("close");

                chatIndex = 0;
                active = false;

                anim.SetBool("Close", true);
            }

            return;
        }

        delay += Time.deltaTime;

        if(delay > 2)
        {
            texts.position = Vector3.Lerp(texts.position, savePos, Time.deltaTime * 12f);

            if (Vector3.Distance(texts.position, savePos) < 0.05f)
            {
                texts.position = savePos;
                Debug.Log("next");

                savePos.y += distance;

                chatIndex++;
                delay = 0;
            }
        }
    }
}
