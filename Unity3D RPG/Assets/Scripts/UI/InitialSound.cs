using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitialSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var soundSettings = GetComponentsInChildren<Slider>();

        soundSettings[0].onValueChanged.AddListener(SoundManager.Instance.MasterVolumeFader); 
        soundSettings[1].onValueChanged.AddListener(SoundManager.Instance.BGMVolumeFader);
        soundSettings[2].onValueChanged.AddListener(SoundManager.Instance.SFXVolumeFader);
    }
}
