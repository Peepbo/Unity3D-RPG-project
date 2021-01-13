using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSwitch : MonoBehaviour
{
    // public BossDB boss;
    public GameObject[] activeObj = new GameObject[3];

    TimeLineController timeLine;

    private void Awake()
    {
        timeLine = GameObject.Find("TimeLine").GetComponent<TimeLineController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag == "Player")
        {
            for(int i = 0; i < 3; i ++)
            {
                if (activeObj[i].activeSelf == false) activeObj[i].SetActive(true);
                else activeObj[i].SetActive(false);
            }
            //controller.SetActive(true);
            timeLine.setPlay();

            //boss.start = true;
            //boss.state = BossState.COMBATIDLE;
            //boss.hpBar.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }


}
