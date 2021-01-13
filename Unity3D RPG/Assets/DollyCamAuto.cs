using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyCamAuto : MonoBehaviour
{
    public float endPos;
    public GameObject cam;
    Cinemachine.CinemachineDollyCart dollyCart;

    bool isUp = true;

    float endTime;
    private void Start()
    {
        Time.timeScale = 0;
        dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dollyCart.m_Position < endPos)
        {
            dollyCart.m_Position += Time.unscaledDeltaTime * dollyCart.m_Speed;
        }
        else
        {
            //Debug.Log("isUp false");
            isUp = false;
            endTime += Time.unscaledDeltaTime;

            if (endTime > 1f)
            {
                cam.SetActive(false);

                if (endTime > 2f)
                {
                    Time.timeScale = 1;
                    this.enabled = false;
                }
            }
        }
    }
}
