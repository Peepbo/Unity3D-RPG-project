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

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioMixer mixer;
    protected SoundManager() { }
    private Dictionary<string, AudioClip> soundBank = new Dictionary<string, AudioClip>();
    private AudioSource bgmPlayer;
    private AudioSource ambPlayer;
    public SoundInfo[] soundData;
    bool isSFXMute = false;
    //bool isBGMMute = false;
    bool isMasterMute = false;
    //float BGMVolume = 0f;
    //float SFXVolume = 0f;
    //float masterVolume = 0f;
    private void Start()
    {
        SoundBankInit();
        PlayerInit();
    }

    private void SoundBankInit()
    {
        for (int i = 0; i < soundData.Length; i++)
        {
            soundBank.Add(soundData[i].name, soundData[i].clip);
        }
    }

    private void PlayerInit()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        ambPlayer = gameObject.AddComponent<AudioSource>();


        ambPlayer.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        ambPlayer.playOnAwake = false;
        ambPlayer.loop = true;
        bgmPlayer.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = 0.5f;
    }

    public void SFXPlay(string clipName, Vector3 position)
    {
        
        GameObject _speaker = ObjectPool.SharedInstance.GetPooledObject("Sound");
        _speaker.SetActive(true);
        _speaker.transform.position = position;
        AudioSource _audioSource = _speaker.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        _audioSource.playOnAwake = false;
        _audioSource.clip = soundBank[clipName];
        _audioSource.spatialBlend = 0.7f;
        if (isSFXMute)
        {
            _audioSource.mute = true;
        }
        _audioSource.Play();
        StartCoroutine(ObjectPoolReturn(_audioSource.clip.length, _speaker));

    }
    IEnumerator ObjectPoolReturn(float time, GameObject gameObject)
    { yield return new WaitForSeconds(time); gameObject.SetActive(false); }

    public void AMBPlay(string clipName, float volume = 0.5f)
    {
        ambPlayer.clip = soundBank[clipName];
        ambPlayer.volume = volume;
        ambPlayer.Play();

    }
    //앰비사운드도 만들어서 넣기

    public void BGMPlay(string clipName)
    {
        bgmPlayer.clip = soundBank[clipName];
        bgmPlayer.Play();
    }
    public void SFXVolumeMixer(float volume)
    {
         //   SFXVolume = volume;
            mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); 
    }
    public void BGMVolumeMixer(float volume)
    {
           // BGMVolume = volume;
            mixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); // logScale 사용 유니티 볼륨값이
    }

    public void MasterVolume(float volume)
    {
        //masterVolume = volume;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SFXMuteSwitch()
    {
        if (!isSFXMute)
        {
           // mixer.SetFloat("SFXVolume", Mathf.Log10(0.0001f) * 20);
            isSFXMute = true;
        }
        else
        {
           // mixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolume) * 20);
            isSFXMute = false;
        }
        
    }
    public void BGMMuteSwitch()
    {
        if (!bgmPlayer.mute)
        {
            //  mixer.SetFloat("MasterVolume", Mathf.Log10(0.0001f) * 20);
            bgmPlayer.mute = true;
            //isBGMMute = true;
        }
        else
        {
           // mixer.SetFloat("MasterVolume", Mathf.Log10(BGMVolume) * 20);
            //isBGMMute = false;
            bgmPlayer.mute = false;
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

}
