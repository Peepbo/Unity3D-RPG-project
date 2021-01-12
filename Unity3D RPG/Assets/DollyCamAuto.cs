using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyCamAuto : MonoBehaviour
{
    public float endPos;
    public GameObject cam;
    Cinemachine.CinemachineDollyCart dollyCart;

    float endTime;
    private void Start()
    {
        dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dollyCart.m_Position < endPos)
        {
            dollyCart.m_Position += Time.deltaTime * dollyCart.m_Speed;
        }
        else
        {
            endTime += Time.deltaTime;

            if (endTime > 5f)
            {
                cam.SetActive(false);
                this.enabled = false;
            }
        }
    }
}
