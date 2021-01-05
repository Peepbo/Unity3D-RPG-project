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
        yield return new WaitForSeconds(1f);
        canvas.SetActive(false);
        cam.Priority = 15;

        yield return new WaitForSeconds(dieTime);
        canvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
