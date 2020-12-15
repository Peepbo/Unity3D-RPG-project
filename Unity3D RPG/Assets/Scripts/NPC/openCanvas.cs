using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class openCanvas : MonoBehaviour
{
    public GameObject canvas;
    public bool isActive = false;
    Vector3 angle;

    public CinemachineVirtualCamera vcam;
    public CinemachineTargetGroup group;
    public CinemachineTargetGroup.Target target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = true;
            group.m_Targets.SetValue(target, 1);
            //angle = vcam.transform.eulerAngles;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
            group.m_Targets.SetValue(null, 1);
        }
    }

    private void Update()
    {
        canvas.SetActive(isActive);
    }
}
