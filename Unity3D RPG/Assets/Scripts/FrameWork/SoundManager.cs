using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
 public class SoundInfo
{
    public string name;
    public AudioClip clip;
}

enum PlayerName { BGM1,BGM2,AMB,UI}
public class SoundManager : Singleton<SoundManager>
{
    protected SoundManager() { }

    [SerializeField]
    private AudioMixer mixer;
    private Dictionary<string, AudioClip> soundBank = new Dictionary<string, AudioClip>();
    private AudioSource[] player;
    public SoundInfo[] soundData;
    bool isSFXMute = false;
    bool isMasterMute = false;
    
    private void Awake()
    {
        SoundBankInit();
        player = GetComponents<AudioSource>();
    }
    private void Start()
    {
        
        BGMPlay("MainTitleBGM");
        AMBPlay("Intro_Amb",0.7f);
        
    }
    private void SoundBankInit()
    {
        for (int i = 0; i < soundData.Length; i++)
        {
            soundBank.Add(soundData[i].name, soundData[i].clip);
        }
    }
    public void SFXPlay2D(string clipName, float volume = 0.5f)
    {
        //player[(int)PlayerName.AMB].clip = soundBank[clipName];
        player[(int)PlayerName.UI].volume = volume;
        player[(int)PlayerName.UI].PlayOneShot(soundBank[clipName]);
    }
    public void SFXPlay(string clipName, Vector3 position, bool isLoop=false)
    {
        GameObject _speaker = ObjectPool.SharedInstance.GetPooledObject("Sound");
        _speaker.SetActive(true);
        _speaker.transform.position = position;
        AudioSource _audioSource;
        if (_speaker.GetComponent<AudioSource>() != null) _audioSource = _speaker.GetComponent<AudioSource>(); 
        else _audioSource = _speaker.AddComponent<AudioSource>();

        _audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        _audioSource.playOnAwake = false;
        _audioSource.clip = soundBank[clipName];
        _audioSource.spatialBlend = 0.9f;
        _audioSource.minDistance = 5f;
        _audioSource.maxDistance = 10f;
        if (isLoop)
            _audioSource.loop = true;
        if (isSFXMute)
        {
            _audioSource.mute = true;
        }
        _audioSource.Play();
        if(!isLoop)
            StartCoroutine(ObjectPoolReturn(_audioSource.clip.length, _speaker));
    }
    public void AMBPlay(string clipName, float volume = 0.5f)
    {
        player[(int)PlayerName.AMB].clip = soundBank[clipName];
        player[(int)PlayerName.AMB].volume = volume;
        player[(int)PlayerName.AMB].Play();

    }
    public void AMBStop()
    {
        StartCoroutine(FadeOut(player[(int)PlayerName.AMB]));
    }
    public void BGMPlay(string clipName)
    {
        if(player[(int)PlayerName.BGM1].isPlaying)
        {
            player[(int)PlayerName.BGM2].clip = soundBank[clipName];
            player[(int)PlayerName.BGM2].volume = 0f;
            player[(int)PlayerName.BGM2].Play();
            StopCoroutine(FadeIn(player[(int)PlayerName.BGM1]));

            StartCoroutine(FadeIn(player[(int)PlayerName.BGM2]));
            StartCoroutine(FadeOut(player[(int)PlayerName.BGM1]));
        }
        else
        {
            player[(int)PlayerName.BGM1].clip = soundBank[clipName];
            player[(int)PlayerName.BGM1].volume = 0f;
            player[(int)PlayerName.BGM1].Play();
            StartCoroutine(FadeIn(player[(int)PlayerName.BGM1]));
            if (player[(int)PlayerName.BGM2].isPlaying)
            {
                StopCoroutine(FadeIn(player[(int)PlayerName.BGM2]));
                StartCoroutine(FadeOut(player[(int)PlayerName.BGM2]));
            }
        }
    }

    public void BGMStop(string clipName)
    {
        if(player[(int)PlayerName.BGM1].clip == soundBank[clipName])
        {
            player[(int)PlayerName.BGM1].Stop();
        }
        else
        {
            player[(int)PlayerName.BGM2].Stop();
        }
    }
    public void BGMAllStop()
    {
        player[(int)PlayerName.BGM1].Stop();
        player[(int)PlayerName.BGM2].Stop();
    }

    public void SFXVolumeFader(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); 
    }
    public void BGMVolumeFader(float volume)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); // logScale 사용 유니티 볼륨값이
    }
    public void MasterVolumeFader(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SFXMuteSwitch()
    {
        if (!isSFXMute) isSFXMute = true;
        else            isSFXMute = false;
        
    }
    public void BGMMuteSwitch()
    {
        if (!player[(int)PlayerName.BGM1].mute)
        {
            player[(int)PlayerName.BGM1].mute = true;
            player[(int)PlayerName.BGM2].mute = true;
        }
        else
        {
            player[(int)PlayerName.BGM1].mute = false;
            player[(int)PlayerName.BGM2].mute = false;
        }
    }
    public void MasterMuteSwitch()
    {
        if (!isMasterMute)
        {
            Camera.main.GetComponent<AudioListener>().enabled = true;
            isMasterMute = true;
        }
        else
        { 
            Camera.main.GetComponent<AudioListener>().enabled = false;
            isMasterMute = false;
        }
    }

    public IEnumerator FadeOut(AudioSource player ,bool isStop = true)
    {
        
        player.volume -= 0.02f;
        yield return new WaitForSeconds(0.15f);
        if (player.volume > 0.01f)
            StartCoroutine(FadeOut(player));
        else
        {
            player.volume = 0.01f;
            if(isStop)
                player.Stop();
        }
       // Debug.Log(player.name);
    }
    public IEnumerator FadeIn(AudioSource player)
    {
        player.volume += 0.01f;
        yield return new WaitForSeconds(0.1f);
        if (player.volume <= 0.5f)
            StartCoroutine(FadeIn(player));
        else 
            player.volume = 0.5f;

        //Debug.Log(player.name);
    }

    IEnumerator ObjectPoolReturn(float time, GameObject gameObject)
    {yield return new WaitForSecondsRealtime(time); gameObject.SetActive(false); }
}

