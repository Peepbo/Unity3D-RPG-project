using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suhyun : MonoBehaviour
{
    public GameObject prefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Instantiate(prefab, transform.position, Quaternion.identity);
            prefab.SetActive(true);
        }
    }
}
