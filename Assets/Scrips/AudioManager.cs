using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource menuMusic, levelSelect;
    [SerializeField] private List<AudioSource> bgm;
    [SerializeField] private List<SFXSounds> sfxs;
    [SerializeField] private AudioSource prefab;

    [Serializable]
    private struct SFXSounds
    {
        public SFXEnum Name;
        public AudioSource AudioSource;
    }

    private AudioSource bgmPlaying;
    private void Awake()
    {
        var audioManager = ServiceLocator.GetService<AudioManager>();

        if(audioManager != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        ServiceLocator.RegisterService<AudioManager>(this);
    }

    public void PlayMenuMusic()
    {
        if(bgmPlaying != null)
        {
            bgmPlaying.Stop();
        }

        bgmPlaying = menuMusic;

        bgmPlaying.Play();
    }

    public void PlayLevelSelectMusic()
    {
        if (bgmPlaying != null)
        {
            bgmPlaying.Stop();
        }

        bgmPlaying = levelSelect;

        bgmPlaying.Play();
    }

    public void PlayBGM()
    {
        if (bgmPlaying != null)
        {
            bgmPlaying.Stop();
        } 

        bgmPlaying = ListTools.GetRandomEntryFromList<AudioSource>(bgm);

        bgmPlaying.Play();
    }

    public void PlaySFX(SFXEnum soundName)
    {
        var foundAudio = GetSFX(soundName);
        foundAudio.Stop();
        foundAudio.Play();
    }

    private AudioSource GetSFX(SFXEnum soundName)
    {
        return sfxs.Find(s => s.Name == soundName).AudioSource;
    }
}
