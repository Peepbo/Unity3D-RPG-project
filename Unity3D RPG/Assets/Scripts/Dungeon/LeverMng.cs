using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMng : MonoBehaviour
{
    public GameObject[] lever = new GameObject[2];
    public int activeLever = 0;
    bool oneTime = false;

    public GameObject door;

    public GameObject cam;

    public void TurnOn()
    {
        activeLever++;
    }

    public void Update()
    {
        if(activeLever == 2 && !oneTime)
        {
            oneTime = true;
            StartCoroutine(CameraEvent());
        }
    }

    IEnumerator CameraEvent()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.7f);
        cam.SetActive(true);
        yield return new WaitForSecondsRealtime(1.3f);
        door.GetComponent<Animator>().SetBool("Open", true);
        yield return new WaitForSecondsRealtime(2.5f);
        cam.SetActive(false);
        yield return new WaitForSecondsRealtime(0.8f);
        Time.timeScale = 1f;
    }
}
