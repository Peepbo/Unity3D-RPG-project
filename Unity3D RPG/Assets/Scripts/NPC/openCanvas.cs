using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCanvas : MonoBehaviour
{
    public GameObject canvas;
    public bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") isActive = false;
    }

    private void Update()
    {
        canvas.SetActive(isActive);
    }
}
