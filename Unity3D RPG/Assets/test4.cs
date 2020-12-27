using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test4 : MonoBehaviour
{
    Button bt;
    // Start is called before the first frame update
    void Start()
    {
        bt = GetComponent<Button>();
        bt.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
