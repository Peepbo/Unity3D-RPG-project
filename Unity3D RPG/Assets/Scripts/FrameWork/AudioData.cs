using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioData : MonoBehaviour
{
    public string[] soundData;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.SFXPlay(soundData[0],transform.position);
            
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.Instance.SFXMuteSwitch();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.Instance.MasterMuteSwitch();
        }

    }
}
