using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Respawn")
        {

        }
    }
}
