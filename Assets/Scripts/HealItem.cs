using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public string name;
    public float healAmount;
    public float eatTime = 2f;
    public bool isRainbow = false;
}

[RequireComponent(typeof(CircleCollider2D))]
public class HealItem : MonoBehaviour
{
    public ItemInfo info;

    protected void Start()
    {
        EventManager.Instance.AddListener(EventType.EatFoodEnd, OnGameStatusChanged);
    }

    public void Pick()
    {

    }

    public void Eat()
    {
        EventManager.Instance.PostNitification(EventType.EatFoodBegin, this, info);
    }

    protected virtual void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                var info = args as ItemInfo;
                if(info != null && this.info == info)
                    gameObject.SetActive(false);
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
        }
    }

    protected void OnDestroy()
    {
        EventManager.Instance.PostNitification(EventType.EatFoodEnd, this, new ItemInfo
        {
            name = name,
            eatTime = 0f,
            healAmount = 0f
        });
    }
}
