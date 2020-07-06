using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float rainbowTime = 5f;

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
            SceneManager.LoadScene(0);
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
                var item = args as ItemInfo;
                if (item == null)
                    return;

                if (item.isRainbow)
                {
                    IsRainbow = item.isRainbow;
                    EventManager.Instance.PostNitification(EventType.OnChangedRainbow, this, IsRainbow);
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
    }
}
