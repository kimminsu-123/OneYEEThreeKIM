using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGameover
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
        if (Instance != null)
            DestroyImmediate(gameObject);

        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        EventManager.Instance.AddListener(EventType.Gameover, OnGameStatusChanged);
    }

    void Update()
    {
        
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
                AudioManager.Instance.OneShotPlay(AudioManager.Instance.deadSound);
                IsGameover = true;
                StopAllCoroutines();
                break;
        }
    }
}
