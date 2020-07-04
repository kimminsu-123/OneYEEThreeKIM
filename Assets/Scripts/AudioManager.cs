using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip deadSound;
    public AudioClip eatSound;

    private AudioSource source;

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
        if (Instance)
            DestroyImmediate(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        source = Camera.main.GetComponent<AudioSource>();
    }

    public void OneShotPlay(AudioClip clip, float vScale = 1f)
    {
        source.PlayOneShot(clip, vScale);
    }

    public void StopSound()
    {
        source.Stop();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}
