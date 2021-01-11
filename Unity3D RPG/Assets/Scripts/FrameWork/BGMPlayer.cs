using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BGMPlayer : MonoBehaviour
{
    public string BGMclipName;

    private void Start()
    {
        SoundManager.Instance.BGMPlay(BGMclipName);
    }
   
}
