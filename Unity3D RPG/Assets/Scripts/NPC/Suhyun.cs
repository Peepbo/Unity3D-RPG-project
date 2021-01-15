using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Suhyun : MonoBehaviour
{
   // public CinemachineTrackedDolly cam;

    private void Start()
    {
        //cam = GetComponent<CinemachineTrackedDolly>();
    }

    public void Update()
    {
        //if(cam.m_PathPosition < 84f)
        //{
        //    cam.m_PathPosition = Mathf.Lerp(cam.m_PathPosition, 84.54f, Time.realtimeSinceStartup);
        //}
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
