using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] soundEffects;
    [SerializeField] private AudioSource[] backgroundMusic;

    private int musicIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!backgroundMusic[musicIndex].isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void PlaySFX(int i)
    {
        if (i < soundEffects.Length)
        {
            soundEffects[i].pitch = Random.Range(0.85f, 1.15f);
            soundEffects[i].Play();
        }
    }

    public void StopSFX(int i)
    {
        if (i < soundEffects.Length) soundEffects[i].Stop();
    }

    public void PlayRandomMusic()
    {
        musicIndex = Random.Range(0, backgroundMusic.Length);
        PlayBackgroundMusic(musicIndex);
    }

    public void PlayBackgroundMusic(int j)
    {
        StopBackgroundMusic();
        backgroundMusic[j].Play();
    }

    public void StopBackgroundMusic()
    {
        for (int i = 0; i < backgroundMusic.Length; i++)
        {
            backgroundMusic[i].Stop();
        }
    }
}
