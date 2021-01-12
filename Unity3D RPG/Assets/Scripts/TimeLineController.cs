using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineController : MonoBehaviour
{
    public PlayableDirector director;
    public TimelineAsset timeline;
    public GameObject boss;
    private void Start()
    {
        setPlay();
    }

    public void setPlay()
    {
        director.Play(timeline);
    }
}
