using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suhyun : MonoBehaviour
{
    public Transform target;

    public GameObject[] prefab = new GameObject[3];
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(prefab[0], target.position, Quaternion.identity);
        }
    }
}
