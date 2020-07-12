using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rainbowTime = 5f;

    public ParticleSystem leafEffect;

    public TimeSystem timeSystem;

    public float TimeScale
    {
        get;
        private set;
    }

    public bool IsGameover
    {
        get;
        private set;
    }

    private bool isRainbow;
    public bool IsRainbow
    {
        get
        {
            return isRainbow;
        }
        private set
        {
            isRainbow = value;

            if(isRainbow == false && AudioManager.Instance.source.clip == AudioManager.Instance.rainbowSound)
            {
                if (timeSystem.isDay)
                {
                    AudioManager.Instance.Play(AudioManager.Instance.dayBGM, false);
                }
                else
                {
                    AudioManager.Instance.Play(AudioManager.Instance.nightBGM, false);
                }
            }
        }
    }

    private bool isPause;
    public bool Pause
    {
        get
        {
            return isPause;
        }
        set
        {
            isPause = value;
            TimeScale = isPause ? 0f : 1f;
            if (isPause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                leafEffect.Pause();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                leafEffect.Play();
            }

            EventManager.Instance.PostNitification(EventType.OnPause, this, isPause);
        }
    }

    private static GameManager instance;
    public static GameManager Instance
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
        Pause = false;
    }

    void Start()
    {
        EventManager.Instance.AddListener(EventType.Gameover, OnGameStatusChanged);
        EventManager.Instance.AddListener(EventType.EatFoodEnd, OnGameStatusChanged);
    }

    public void Restart()
    {
        SceneLoadHelper.instance.LoadLevel(1);
        Pause = false;
    }

    public void MainMenu()
    {
        SceneLoadHelper.instance.LoadLevel(0);
    }

    private void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                var item = args as ItemInfo;
                if (item == null)
                    return;

                if (item.isRainbow)
                {
                    IsRainbow = item.isRainbow;
                    EventManager.Instance.PostNitification(EventType.OnChangedRainbow, this, IsRainbow);

                    AudioManager.Instance.Play(AudioManager.Instance.rainbowSound, false);
                    StartCoroutine(RainbowTimer());
                }
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                AudioManager.Instance.OneShotPlay(AudioManager.Instance.deadSound);
                AudioManager.Instance.StopSoundBGM();
                IsGameover = true;
                StopAllCoroutines();
                leafEffect.Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    private IEnumerator RainbowTimer()
    {
        yield return new WaitForSeconds(rainbowTime);

        IsRainbow = false;
        EventManager.Instance.PostNitification(EventType.OnChangedRainbow, this, IsRainbow);

        var clip = timeSystem.isDay ? AudioManager.Instance.dayBGM : AudioManager.Instance.nightBGM;
        AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeIn(0.4f, clip));
    }
}
