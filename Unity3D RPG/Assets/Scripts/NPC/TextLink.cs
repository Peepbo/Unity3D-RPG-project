﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLink : MonoBehaviour
{
    public int inputA;
    public int inputB;
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    public void GetCSV()
    {
        myText.text = PlayerData.Instance.info[inputA, inputB].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //myText.text = PlayerData.Instance.info[inputA, inputB].ToString();
    }
}
