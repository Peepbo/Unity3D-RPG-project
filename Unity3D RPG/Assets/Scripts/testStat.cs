using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testStat : MonoBehaviour
{
    public Image img;
    [Range(0,1)]
    public float hp;

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        hp = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = hp;
    }
}
