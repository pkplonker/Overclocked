using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioSource sfxSource;
    [SerializeField] private AudioMixer mixer;
    public static AudioControl Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void OnValidate()=> musicSource = GetComponent<AudioSource>();
    public void SetVolumeMaster(float v) => SetVolume("MasterVol", v);
    public void SetVolumeMusic(float v) => SetVolume("MusicVol", v);
    public void SetVolumeSFX(float v) => SetVolume("SFXVol", v);

    private void SetVolume(string channel, float v) => mixer.SetFloat(channel, Mathf.Log10(v) * 20);
}
