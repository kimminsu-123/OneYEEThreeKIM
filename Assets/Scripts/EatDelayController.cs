using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatDelayController : MonoBehaviour
{
    private Image m_delay;

    private float timer = 0f;
    private float time;

    private bool isTimer = false;

    private ItemInfo currItemInfo;

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.EatFoodEnd, GameStatusChanged);
    }

    private void Awake()
    {
        m_delay = GetComponent<Image>();
    }

    private void OnEnable()
    {
        timer = time;
        m_delay.fillAmount = 1f;
        isTimer = true;
    }

    private void OnDisable()
    {
        isTimer = false;
        timer -= time;

        if (currItemInfo != null)
            EventManager.Instance.PostNitification(EventType.EatFoodEnd, this, currItemInfo);
    }

    private void Update()
    {
        if (!isTimer)
            return;

        timer -= Time.deltaTime * GameManager.Instance.TimeScale;
        m_delay.fillAmount = timer / time;

        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTime(ItemInfo info)
    {
        currItemInfo = info;
        time = info.eatTime;
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
                var info = args as ItemInfo;
                if (info != null)
                {
                    if (info != currItemInfo && info.eatTime <= 0f)
                    {
                        timer = info.eatTime;
                        currItemInfo = info;
                    }
                }
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
            case EventType.OnTimeChange:
                break;
        }
    }
}
