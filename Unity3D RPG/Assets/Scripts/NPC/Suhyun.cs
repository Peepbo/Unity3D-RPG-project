﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suhyun : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerData.Instance.SaveChest(79);
            PlayerData.Instance.SaveChest(80);
            PlayerData.Instance.SaveChest(81);
        }
    }
}
