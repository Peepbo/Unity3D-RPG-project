using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public void SFXVolumeFader(float volume)
    {
        SoundManager.Instance.SFXVolumeFader(volume);
    }
    public void BGMVolumeFader(float volume)
    {
        SoundManager.Instance.BGMVolumeFader(volume);
    }
    public void MasterVolumeFader(float volume)
    {
        SoundManager.Instance.MasterVolumeFader(volume);
    }
}
