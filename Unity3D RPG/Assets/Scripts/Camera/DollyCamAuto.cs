using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyCamAuto : MonoBehaviour
{
    public float endPos;
    public GameObject cam;
    CinemachineDollyCart dollyCart;

    float endTime = 0;
    private void Start()
    {
        Time.timeScale = 0;
        endTime = 0;
        dollyCart = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dollyCart.m_Position < endPos)
        {
            dollyCart.m_Position += Time.unscaledDeltaTime * dollyCart.m_Speed;
        }

        endTime += Time.unscaledDeltaTime;
        if (endTime > 5f)
        {
            cam.SetActive(false);

            if (endTime > 7f)
            {
                Time.timeScale = 1;
                this.enabled = false;
            }
        }
    }
}
