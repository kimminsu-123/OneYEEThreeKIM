using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerInfoPanel playerInfoPanel;
    public GameoverPanel gameoverPanel;
    public DelayPanel delayPanel;
    public GameInfoPanel gameInfoPanel;
    public PausePanel pausePanel;

    private static UIManager instance;
    public static UIManager Instance
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

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.Gameover, GameStatusChanged);
        EventManager.Instance.AddListener(EventType.EatFoodBegin, GameStatusChanged);
        EventManager.Instance.AddListener(EventType.OnTimeChange, GameStatusChanged);
        EventManager.Instance.AddListener(EventType.OnPause, GameStatusChanged);
    }

    private void GameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                var info = args as ItemInfo;
                if (info == null)
                    return;
                delayPanel.OnEatDelay(info);
                break;
            case EventType.EatFoodEnd:
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                gameoverPanel.gameObject.SetActive(true);
                break;
            case EventType.OnTimeChange:
                gameInfoPanel.SetTimeDayOrNight();
                break;
            case EventType.OnPause:
                //pausePanel setactive true
                var pause = (bool)args;
                pausePanel.ShowAndHideOption(pausePanel.pausePanelTr, pause);

                if (!pause)
                    pausePanel.ShowAndHideOption(pausePanel.optionPanelTr, pause);
                    
                break;
        }
    }
}
