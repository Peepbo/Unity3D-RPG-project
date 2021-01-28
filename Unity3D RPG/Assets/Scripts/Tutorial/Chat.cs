using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Chat : MonoBehaviour
{
    public Text quest;
    public Text texts;
    public int chatIndex = 0;

    string[] quests =
    {
        "컨트롤러를 사용하여 NPC에 도달하기",
        "기본 공격으로 몬스터 공격하기",
        "강 공격으로 몬스터 공격하기",
        "몬스터 공격 방어하기",
        "구르기 버튼 누르기",
        "시점을 변경하여 특정 오브젝트 파괴하기",
        "포탈 타고 마을로 이동하기",
    };

    string[] npcTalks =
    {
        "신입주제에 늦다니.. 빨리안와?",
        "몬스터가 네 집을 공격할지도 모르는데 느긋하지?",
        "오늘도 훈련이다.  타겟을 향해 기본공격 실시",
        "다음은 조금 더 쎈 공격으로 실시",
        "약공격중 강공격이 가능하니 유용하게 섞어서 사용해",
        "지금보니 공격에 힘이 없는데, 살기위해 방어 연습부터 하자",
        "테스트용 고블린의 공격을 한번 막아봐",
        "행동이 굼뜨군. 행동이 굼뜨니 굴러다니기 딱 좋겠어",
        "굴러서 체력도 기르고, 좀 더 빨리 이동해봐",
        "오.. 구르는게 콩벌레같군 앞으로도 굴러다니도록",
        "시점을 변경하면 못보던 것들을 볼 수 있으니",
        "한 번 변경해서 버튼을 찾아봐",
        "능력도 없는 녀석을 다치지 않게 훈련시키기도 힘들군",
        "어서 꺼져"
    };

    int textCount = 0;

    int[] questChats = { 1, 1, 1, 3, 2, 3, 2 };

    public TutorialMng tutorialMng;
    int questIndex = 0;
    float delay = 0f;

    bool active = true;

    Animator anim;

    public GameObject canvas;

    private void Start()
    {
        Player _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _player.hp = _player.maxHp;
        quest.text = quests[tutorialMng.questNumber];

        texts.text = npcTalks[textCount];

        anim = GetComponent<Animator>();

        canvas.SetActive(false);
        StartCoroutine(ActiveCanvas(6.5f));
    }

    IEnumerator ActiveCanvas(float time)
    {
        canvas.SetActive(false);
        yield return new WaitForSeconds(time);
        canvas.SetActive(true);
    }

    void Update()
    {
        if(active) moveChat();
    }

    public void NextQuest()
    {
        chatIndex++;

        quest.text = quests[tutorialMng.questNumber];
        questIndex++;
        if(questChats[questIndex] == 3) StartCoroutine(ActiveCanvas(8.5f));
        else StartCoroutine(ActiveCanvas(questChats[questIndex] * 3.15f));

        active = true;

        texts.text = npcTalks[textCount];
        anim.SetBool("Close", false);
    }

    public void moveChat()
    {
        if (chatIndex == questChats[questIndex])
        {
            delay += Time.deltaTime;

            if(delay > 3)
            {
                delay = 0;

                textCount++;

                chatIndex = 0;
                active = false;

                anim.SetBool("Close", true);
            }

            return;
        }

        delay += Time.deltaTime;

        if(delay > 3)
        {
            textCount++;
            delay = 0;
            texts.text = npcTalks[textCount];

            chatIndex++;
        }
    }
}
