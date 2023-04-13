using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> music;
    [SerializeField] private AudioClip uiSound;
    public static AudioController Instance;
    private AudioSource source;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        PlayRandomMusic();

    }
    private void PlayRandomMusic()
    {
        source.clip = music[UnityEngine.Random.Range(0, music.Count)];
        GetComponent<AudioSource>().Play();
        Invoke(nameof(PlayRandomMusic), GetComponent<AudioSource>().clip.length);
    }
    public void PlayClip(AudioClip clip) => source.PlayOneShot(clip);
    public void PlayUI() => PlayClip(uiSound);
}
