using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActiveTime());
    }

    IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(3.2f);
        gameObject.SetActive(false);
    }
}
