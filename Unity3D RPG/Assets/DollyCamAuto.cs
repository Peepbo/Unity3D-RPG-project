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
        dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            if (dollyCart.m_Position < endPos)
            {
                dollyCart.m_Position += Time.deltaTime * dollyCart.m_Speed;
            }
            else
            {
                Debug.Log("isUp false");
                isUp = false;
            }
        }

        else
        {
            dollyCart.m_Position -= Time.deltaTime * dollyCart.m_Speed;

            if (dollyCart.m_Position < 1.5f)
            {
                cam.SetActive(false);
                this.enabled = false;
            }
        }

    }
}
