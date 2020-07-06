﻿using System.Collections;
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
    private PlayerMovement playerMovement;

    
    private void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        base.Start();
        postProcessingOfHealth.TryGet(out vignette);
        EventManager.Instance.AddListener(EventType.EatFoodEnd, OnGameStatusChanged);
        EventManager.Instance.AddListener(EventType.EatFoodBegin, OnGameStatusChanged);
    }

    void Update()
    {
        DecreaseHealth();
        PlayerSlowly();
    }

    public void TakeDamage(float damage)
    {
        CurrHealth -= damage;
        var value = Mathf.Clamp(maxVignette - (CurrHealth / maxHealth), 0f, maxVignette);
        vignette.intensity.value = value;
    }

    private void DecreaseHealth()
    {
        CurrHealth -= Time.deltaTime * minusHealthPerSec;
        var value = Mathf.Clamp(maxVignette - (CurrHealth / maxHealth), 0f, maxVignette);
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
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                if (AudioManager.Instance.IsPlaying())
                    return;

                AudioManager.Instance.OneShotPlay(AudioManager.Instance.eatSound);
                break;
            case EventType.EatFoodEnd:
                if(AudioManager.Instance != null)
                {
                    AudioManager.Instance.StopSound();
                }
                var info = args as ItemInfo;
                if (info == null)
                    return;
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

    private Obstacle currObs;
    private float speed;
    private float dash;
    private bool isSlowly = false;
    public void PlayerDetection(Obstacle obs)
    {
        currObs = obs;

        
        TakeDamage(currObs.damage);
        switch (obs.type)
        {
            case ObstacleTypes.SpeedSlowly:
                speed = playerMovement.saveSpeed;
                dash = playerMovement.saveDash;
                isSlowly = true;
                playerMovement.ChangeSpeed(speed * currObs.moveSpeedPercent, dash * currObs.moveSpeedPercent);
                break;
        }
    }

    private float slowTimer = 0f;
    private void PlayerSlowly()
    {
        if (isSlowly)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= currObs.time)
            {
                slowTimer = 0f;
                isSlowly = false;
                playerMovement.ChangeSpeed(speed , dash );
            }
        }
    }

    public void Heal(float amount)
    {
        CurrHealth += amount;
    }
}
