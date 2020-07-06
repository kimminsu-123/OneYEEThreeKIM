using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Opening,
    Story,
    EatFoodBegin,
    EatFoodEnd,
    Gameover,
    OnTimeChange,
    OnDay,
    OnChangedRainbow,
}

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;

    }


    public delegate void OnEvent(EventType et, Component sender, object args = null);
    private Dictionary<EventType, List<OnEvent>> listeners =
        new Dictionary<EventType, List<OnEvent>>();

    public void AddListener(EventType et, OnEvent listener)
    {
        if (!listeners.ContainsKey(et))
        {
            listeners.Add(et, new List<OnEvent>());
        }
        listeners[et].Add(listener);
    }

    public void PostNitification(EventType et, Component sender, object args = null)
    {
        if (listeners.ContainsKey(et))
        {
            var list = listeners[et];
            foreach (var listener in list)
            {
                listener?.Invoke(et, sender, args);
            }
        }
    }

    public void RemoveListener(EventType et, OnEvent listener) 
    {
        if (listeners.ContainsKey(et))
        {
            listeners[et].Remove(listener);
        }
    }
}
