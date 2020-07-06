using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip deadSound;
    public AudioClip eatSound;

    public AudioClip nightBGM;
    public AudioClip dayBGM;

    public AudioSource source;
    public AudioSource sourceBGM;

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    public void OneShotPlay(AudioClip clip, float vScale = 1f)
    {
        source.PlayOneShot(clip, vScale);
    }

    public void StopSound()
    {
        source.Stop();
    }

    public void StopSoundBGM()
    {
        sourceBGM.Stop();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }

    public bool IsPlayingBGM()
    {
        return sourceBGM.isPlaying;
    }

    public void Play(AudioClip clip)
    {
        if (clip == sourceBGM.clip)
            return;

        sourceBGM.clip = clip;
        sourceBGM.Play();
    }

    public void SetVolume(float v)
    {
        sourceBGM.volume = v;
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / FadeTime;
            yield return null;
        }
        source.Stop();
    }
    public IEnumerator FadeIn(float FadeTime, AudioClip clip)
    {
        while (sourceBGM.volume > 0)
        {
            sourceBGM.volume -= Time.deltaTime / FadeTime;
            yield return null;
        }

        Play(clip);
        sourceBGM.volume = 0f;
        while (sourceBGM.volume < 1)
        {
            sourceBGM.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
