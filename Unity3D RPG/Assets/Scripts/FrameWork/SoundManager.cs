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
    private AudioSource sfxPlayer;
    private AudioSource ambPlayer;
    public SoundInfo[] soundData;
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
        sfxPlayer = gameObject.AddComponent<AudioSource>();
        ambPlayer = gameObject.AddComponent<AudioSource>();

        sfxPlayer.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        sfxPlayer.spatialBlend = 1.0f;

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
        _audioSource.playOnAwake = false;
        _audioSource.clip = soundBank[clipName];
        _audioSource.spatialBlend = 0.7f;
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
    public void VolumeMixer(float volume, bool isBGM)
    {
        if (isBGM)
            mixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); // logScale 사용 유니티 볼륨값이
        else
            mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void MasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    //public void MuteSFX()
    //{
    //    //mixer.FindMatchingGroups("SFX")[0]
    //}

}
