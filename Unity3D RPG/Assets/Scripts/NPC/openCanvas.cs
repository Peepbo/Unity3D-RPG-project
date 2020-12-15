using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class openCanvas : MonoBehaviour
{
    public GameObject canvas;
    public bool isActive = false;

    public CinemachineVirtualCamera vcam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
            vcam.LookAt = null;
        }
        
    }

    private void Update()
    {
        canvas.SetActive(isActive);

        if(isActive)
        {
            vcam.LookAt = transform;
        }
    }
}
