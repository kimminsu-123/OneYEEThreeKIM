using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeSystem : MonoBehaviour
{
    public Light2D playerDarkLight;
    public Light2D globalLight;

    public float dayTime_Minute = 3f;
    public float changeSpeed = 2f;

    public bool isDay = true;

    private float dayTime_Sec;
    private float timer = 0f;
    private bool isGameover = false;

    private void Awake()
    {
        dayTime_Sec = dayTime_Minute * 60f;
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.Gameover, OnGameStatusChanged);
    }

    void Update()
    {
        Timer();
        ChangeDayTime();
    }

    private void Timer()
    {
        if (isGameover)
            return;

        timer += Time.deltaTime;
        if (timer >= dayTime_Sec)
        {
            timer = 0f;
            isDay = !isDay;
            if (isDay)
            {
                EventManager.Instance.PostNitification(EventType.OnDay, this);
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
                break;
        }
    }
}
