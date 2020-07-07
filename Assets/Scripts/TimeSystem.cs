using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeSystem : MonoBehaviour
{
    public float fadeTime=0.5f;

    public Light2D playerDarkLight;
    public Light2D globalLight;

    public float dayTime_Minute = 3f;
    public float changeSpeed = 2f;

    public bool isDay = true;

    private float dayTime_Sec;
    private float timer = 0f;
    private bool isGameover = false;
    private float volume = 1f;

    public int hour = 6;
    private float timeSpeed;
    private readonly float dayOnSec = 86400;
    private float secPerMinute;
    private float clock = 0f;

    private void Awake()
    {
        dayTime_Sec = dayTime_Minute * 60f;

        secPerMinute = dayOnSec / dayTime_Sec / 2f;
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.Gameover, OnGameStatusChanged);
    }

    void Update()
    {
        if (isGameover)
            return;

        Timer();
        ChangeDayTime();
        ClockTimer();
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= dayTime_Sec)
        {
            timer = 0f;
            isDay = !isDay;
            volume = 0f;
            if (isDay)
            {
                EventManager.Instance.PostNitification(EventType.OnDay, this);
                StartCoroutine(AudioManager.Instance.FadeIn(fadeTime, AudioManager.Instance.dayBGM));
            }
            else
            {
                StartCoroutine(AudioManager.Instance.FadeIn(fadeTime, AudioManager.Instance.nightBGM));
            }
            EventManager.Instance.PostNitification(EventType.OnTimeChange, this);
        }
    }

    private void ChangeDayTime()
    {
        if (isDay)
        {
            SetDay();
            return;
        }

        SetNight();
    }

    private void SetNight()
    {
        float intensity = playerDarkLight.intensity;
        intensity = Mathf.Clamp(intensity + Time.deltaTime * changeSpeed, 0f, 1f);
        playerDarkLight.intensity = intensity;

        intensity = globalLight.intensity;
        intensity = Mathf.Clamp(intensity - Time.deltaTime * changeSpeed, 0f, 1f);
        globalLight.intensity = intensity;
    }

    private void SetDay()
    {
        float intensity = globalLight.intensity;
        intensity = Mathf.Clamp(intensity + Time.deltaTime * changeSpeed, 0f, 1f);
        globalLight.intensity = intensity;

        intensity = playerDarkLight.intensity;
        intensity = Mathf.Clamp(intensity - Time.deltaTime * changeSpeed, 0f, 1f);
        playerDarkLight.intensity = intensity;
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
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                SetNight();
                isGameover = true;
                AudioManager.Instance.StopSoundBGM();
                AudioManager.Instance.StopSound();
                break;
        }
    }

    private void ClockTimer()
    {
        clock += Time.deltaTime * (secPerMinute / 60f);
        Debug.Log(clock);
        if(clock >= 60)
        {
            clock = 0f;
            hour += 1;
            if(hour >= 24)
            {
                hour = 0;
            }
        }

        UIManager.Instance.gameInfoPanel.SetClockTime(hour, (int)clock);
    }
}
