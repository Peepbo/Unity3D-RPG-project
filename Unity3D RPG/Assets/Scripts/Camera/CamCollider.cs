using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollider : MonoBehaviour
{
    public GameObject cam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player")) cam.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player")) cam.SetActive(false);
    }
}
