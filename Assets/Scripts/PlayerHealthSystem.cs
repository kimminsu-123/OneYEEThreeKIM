using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthSystem : LivingEntity
{
    public VolumeProfile postProcessingOfHealth;

    public float maxVignette = 0.3f;
    public float minusHealthPerSec;

    private Vignette vignette;
    private static PlayerHealthSystem instance;
    private void Awake()
    {
        base.Awake();

        if (instance != null)
            DestroyImmediate(gameObject);

        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        base.Start();
        postProcessingOfHealth.TryGet(out vignette);
        EventManager.Instance.AddListener(EventType.EatFoodEnd, OnGameStatusChanged);
    }

    void Update()
    {
        DecreaseHealth();
    }

    private void DecreaseHealth()
    {
        CurrHealth -= Time.deltaTime * minusHealthPerSec;
        var value = Mathf.Clamp(maxVignette - (currHealth / maxHealth), 0f, maxVignette);
        vignette.intensity.value = value;
    }

    protected override void ChangeHealthUI()
    {
        if (GameManager.Instance.IsGameover)
            return;

        base.ChangeHealthUI();

        var nomalHungry = CurrHealth / maxHealth;
        UIManager.Instance.playerInfoPanel.UpdateHungryGauge(nomalHungry);
    }

    private void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        var info = args as ItemInfo;
        if (info == null)
            return;
         
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                CurrHealth += info.healAmount;
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                CurrHealth = 0f;
                ChangeHealthUI();
                break;
        }
    }

}
