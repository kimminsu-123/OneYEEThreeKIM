using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBGM : MonoBehaviour
{
    private AudioSource source;

    private static SourceBGM instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
    }

    public void OneShotPlay(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void Play(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void Pause(bool isPause)
    {
        if (isPause)
            source.Pause();
        else
            source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }
}
