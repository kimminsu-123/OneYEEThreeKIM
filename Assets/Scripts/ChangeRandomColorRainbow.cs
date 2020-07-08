using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RainbowTypes
{
    AllSituation,
    OneSituation,
}

public class ChangeRandomColorRainbow : MonoBehaviour
{
    public float time;
    public RainbowTypes type;

    private bool isRainbow = false;
    private float timer;
    private SpriteRenderer sr;
    private Color startColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    void Start()
    {
        EventManager.Instance.AddListener(EventType.OnChangedRainbow, OnGameStatusChanged);
    }

    void Update()
    {
        switch (type)
        {
            case RainbowTypes.AllSituation:
                SetRandomColor();
                break;

            case RainbowTypes.OneSituation:
                if (!isRainbow)
                    return;
                SetRandomColor();
                break;
        }
    }

    private Color GetRandomColor()
    {
        Color random = new Color
        (
            Random.Range(0.3f, 1f),
            Random.Range(0.3f, 1f),
            Random.Range(0.3f, 1f),
            1f
        );

        return random;
    }

    private void SetRandomColor()
    { 
        timer += Time.deltaTime * GameManager.Instance.TimeScale;
        if(timer >= time)
        {
            var newColor = GetRandomColor();
            sr.color = newColor;

            timer = 0f;
        }
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
                break;
            case EventType.OnChangedRainbow:
                //게임 매니저에서 받아옴
                isRainbow =(bool) args;
                if (!isRainbow)
                {
                    sr.color = startColor;
                }
                break;
        }
    }
}
