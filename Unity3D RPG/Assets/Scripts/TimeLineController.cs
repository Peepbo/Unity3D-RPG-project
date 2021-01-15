﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    //0:Controll Panel1 , 1:MainPlayer, 2:controller
    public GameObject[] activeObj = new GameObject[3];
    public GameObject skipButton;
    public GameObject bossObj;
    public PlayableDirector director;

    private BossDB boss;
    private float popUpTime=0.0f;
    private float time = 21.0f;

    private void Awake()
    {
        boss = bossObj.GetComponent<BossDB>();
    }

    private void Update()
    {
        //playable을 실행시키는 controller가 active상태 일 때
        if (activeObj[2].activeSelf ==true)
        {
            popUpTime += Time.deltaTime;

            if (popUpTime >2.0f)
            {
                //스킵 버튼 활성화
                skipButton.SetActive(true);
            }

        }

        if (director.time >= time)
        {
            // isPlaying = false;
            // controller.SetActive(false);

            for (int i = 0; i < 3; i++)
            {

                if (activeObj[i].activeSelf == false) activeObj[i].SetActive(true);
                else activeObj[i].SetActive(false);

            }

            bossObj.SetActive(true);
            boss.start = true;
            boss.state = BossState.COMBATIDLE;
            boss.hpBar.gameObject.SetActive(true);

            skipButton.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void setPlay()
    {
        director.Play();

    }


    public void SkipCutScene()
    {
        //SkipButton UI전용 함수

        director.Stop();
        director.time = 0.0f;
        for (int i = 0; i < 3; i++)
        {
            if (activeObj[i].activeSelf == false) activeObj[i].SetActive(true);
            else activeObj[i].SetActive(false);
        }

        bossObj.SetActive(true);
        boss.start = true;
        boss.state = BossState.COMBATIDLE;
        boss.hpBar.gameObject.SetActive(true);

        this.enabled = false;

    }

}
