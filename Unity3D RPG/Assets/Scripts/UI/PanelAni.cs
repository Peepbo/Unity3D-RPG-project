using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAni : MonoBehaviour
{
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 10f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 5);
        }
        else
        {
            time = 0;
        }

        time += Time.deltaTime;
    }
}
