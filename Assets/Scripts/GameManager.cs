using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rainbowTime = 5f;

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

    public bool IsRainbow
    {
        get;
        private set;
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

    void Update()
    {
        if (IsGameover && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
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

                    AudioManager.Instance.Play(AudioManager.Instance.rainbowSound);
                    StartCoroutine(RainbowTimer());
                }
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                AudioManager.Instance.OneShotPlay(AudioManager.Instance.deadSound);
                IsGameover = true;
                StopAllCoroutines();
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
