using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    public GameObject[] activeObj = new GameObject[3];
    public GameObject bossObj;
    BossDB boss;
    public PlayableDirector director;

   // public GameObject controller;
    public bool isPlaying = true;
    private float time = 21.0f;
    private void Awake()
    {
        boss = bossObj.GetComponent<BossDB>();
    }

    private void Update()
    {
        if (director.time >= time)
        {
            isPlaying = true;
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

            this.gameObject.SetActive(false);
        }

    }
    public void setPlay()
    {
        director.Play();

    }


}
