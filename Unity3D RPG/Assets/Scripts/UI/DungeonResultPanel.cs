using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonResultPanel : Singleton<DungeonResultPanel>
{
    Text resultText;
    Text countdownText;
    bool isTimer = false;
    float count = 10f;
    GameObject title;
    GameObject countDown;
    bool comeIn = false;

    public GameObject lootPanel;


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
        title = transform.Find("Title").gameObject;
        title.SetActive(true);
        resultText = title.GetComponentInChildren<Text>();
       
        if (isClear)
        {
            resultText.text = "던전 클리어!";
            countDown = transform.Find("Countdown").gameObject;
            countDown.SetActive(true);
            countdownText = countDown.GetComponent<Text>();
            
            StartCoroutine(resultOpenTimer());
        }
        else
        {
            resultText.text = "ㄹㅇㅋㅋ";
            StartCoroutine(TitleOff());
            
        }
    }
    IEnumerator TitleOff()
    {
        yield return new WaitForSeconds(3f);
        //title.SetActive(false);
        PanelOpen();
        //OnPanelClick();
    }
    IEnumerator resultOpenTimer()
    {
        isTimer = true;
        yield return new WaitUntil(() => count <= 0f);
        title.SetActive(false);
        PanelOpen();
        lootPanel.SetActive(true);

    }
    public void PanelOpen()
    {
        transform.Find("TouchPanel").gameObject.SetActive(true);
        countDown.SetActive(false);
    }

    public void OnPanelClick()
    {
        comeIn = false;
        PlayerData.Instance.isReturn = true;
        LootManager.Instance.Delivery(true);
        DungeonMng.Instance.ClearCount();
        DungeonMng.Instance.ResetStage();
        LoadingSceneController.Instance.LoadScene("TownScene");
    }

   
}
