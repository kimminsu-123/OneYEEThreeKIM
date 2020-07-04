using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushRoom : HealItem
{
    public float poisonDamage = 30;
    public float poisonPersent = 50;

    public float heal = 70;

    private PlayerHealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
    }

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    protected override void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                var info = args as ItemInfo;
                if (info != null && this.info == info)
                {
                    int persent = Random.Range(1, 101);
                    if(persent <= poisonPersent)
                    {
                        healthSystem.TakeDamage(poisonDamage + info.healAmount);
                    }
                    else
                    {
                        healthSystem.Heal(heal - info.healAmount) ;
                    }
                }

                
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
        }

        base.OnGameStatusChanged(et, sender, args);
    }
}
