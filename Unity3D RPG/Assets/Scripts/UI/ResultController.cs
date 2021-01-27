using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : Singleton<ResultController>
{
    Text resultText;
    Text countdownText;
    bool isTimer = false;
    float count = 10f;
    GameObject title;
    GameObject countdown;
    GameObject touchPanel;
    GameObject lootPanel;
    bool comeIn = false;

    public void Init(GameObject _title, GameObject _countdown, GameObject _touchPanel, GameObject _lootPanel)
    {
        title = _title;
        countdown = _countdown;
        touchPanel = _touchPanel;
        lootPanel = _lootPanel;
    }
    private void Update()
    {
        if (isTimer)
        {
            count -= Time.deltaTime;
            int _count = (int)count;
            if (count <= 0f)
            {
                _count = 0;
                isTimer = false;
            }
            countdownText.text = _count.ToString();
        }
    }


    public void GameResult(bool isClear)
    {
        if (comeIn) return;
        comeIn = true;
        //title = transform.Find("Title").gameObject;
        title.SetActive(true);
        resultText = title.GetComponentInChildren<Text>();

        if (isClear)
        {
            resultText.text = "던전 클리어!";
            
            countdown.SetActive(true);
            countdownText = countdown.GetComponent<Text>();
            StartCoroutine(resultOpenTimer());
        }
        else
        {
            resultText.text = "유다이";
            StartCoroutine(TitleOff());
        }
    }
    IEnumerator TitleOff()
    {
        yield return new WaitForSeconds(3f);
        touchPanel.SetActive(true);
    }
    IEnumerator resultOpenTimer()
    {
        isTimer = true;
        yield return new WaitUntil(() => count <= 0f);
        title.SetActive(false);
        countdown.SetActive(false);
        touchPanel.SetActive(true);
        lootPanel.SetActive(true);

    }

    public void OnPanelClick()
    {
        comeIn = false;
        PlayerData.Instance.isReturn = true;
        #region 01/19 아이템 전달
        LootManager.Instance.Delivery(resultText.text.Equals("유다이"));
        #endregion
        #region 01/26 업적 데이터 전달
        List<Achieve> _list = new List<Achieve>(JsonData.Instance.LoadAchieve());
        //스테이지 클리어 업적
        for (int i = 0; i <= 4; i++)
        {
            _list[i].Number++;
        }
        //킬 리스트
        DungeonMng.Instance.murderList[4].killCount++;
        //보스클리어, 챕터 클리어 업적
        _list[13].Number++;
        _list[14].Number++;
        JsonData.Instance.AchieveSave(_list);
        //돈 획득 업적
        int _getMoneyInDungeon = LootManager.Instance.dungeonMoney;
        LootManager.Instance.dungeonMoney = 0;

        for (int i = 15; i <= 19; i++)
        {
            _list[i].Number += _getMoneyInDungeon;
        }
        #endregion
        DungeonMng.Instance.ClearCount();
        DungeonMng.Instance.ResetStage();
        LoadingSceneController.Instance.LoadScene("TownScene");
    }


}
