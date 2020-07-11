using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip rainbowSound;

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

        source = GameObject.FindWithTag("EffectSource").GetComponent<AudioSource>();
        sourceBGM = GameObject.FindWithTag("BGMSource").GetComponent<AudioSource>();
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.OnPause, GameStatusChanged);
        Play(dayBGM, true);
    }

    public void OneShotPlay(AudioClip clip, float vScale = 1f)
    {
        source.PlayOneShot(clip, vScale);
    }

    public void OneShotPlay(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void StopSound()
    {
        source.Stop();
        source.clip = null;
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
    public void PlayEffect(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }



    public void StopSoundBGM()
    {
        sourceBGM.Stop();
    }

    public bool IsPlayingBGM()
    {
        return sourceBGM.isPlaying;
    }

    public void Play(AudioClip clip, bool isStart)
    {
        if (clip == sourceBGM.clip && !isStart)
            return;
        sourceBGM.clip = clip;
        StopSoundBGM();
        sourceBGM.Play();
    }

    public void SetVolumeBGM(float v)
    {
        sourceBGM.volume = v;
    }
    public void SetVolumeEffectSound(float v)
    {
        source.volume = v;
    }

    private IEnumerator FadeOut(float FadeTime)
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / FadeTime * GameManager.Instance.TimeScale;
            yield return null;
        }
        source.Stop();
    }
    public IEnumerator FadeIn(float FadeTime, AudioClip clip)
    {
        if (GameManager.Instance.IsRainbow)
            yield break ;

        while (sourceBGM.volume > 0)
        {
            sourceBGM.volume -= Time.deltaTime / FadeTime * GameManager.Instance.TimeScale;
            yield return null;
        }

        Play(clip, false);
        sourceBGM.volume = 0f;
        while (sourceBGM.volume < 1)
        {
            sourceBGM.volume += Time.deltaTime / FadeTime * GameManager.Instance.TimeScale;
            yield return null;
        }
    }

    private void GameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
            case EventType.OnTimeChange:
                break;
            case EventType.OnPause:
                var pause = (bool)args;
                if (pause)
                {
                    source.Pause();
                    sourceBGM.Pause();
                    break;
                }
                source.UnPause();
                sourceBGM.UnPause();
                //pausePanel setactive true
                break;
        }
    }
}
