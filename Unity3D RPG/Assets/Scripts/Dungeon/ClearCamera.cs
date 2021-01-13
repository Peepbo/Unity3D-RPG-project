using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClearCamera : MonoBehaviour
{
    public float dieTime = 0f;
    public GameObject canvas;
    public CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CamChange());
    }

    IEnumerator CamChange()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        //yield return new WaitForSeconds(1f);
        canvas.SetActive(false);
        cam.Priority = 15;

        yield return new WaitForSecondsRealtime(0.2f);
        SoundManager.Instance.SFXPlay2D("Portal_Open",0.8f);

        SoundManager.Instance.SFXPlay("Portal_Loop",
            transform.parent.GetChild(1).position, true);
        yield return new WaitForSecondsRealtime(dieTime - 0.2f);
        //yield return new WaitForSeconds(dieTime);
        canvas.SetActive(true);
        cam.enabled = false;
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
