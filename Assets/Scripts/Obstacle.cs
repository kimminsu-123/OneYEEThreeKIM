using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTypes
{
    None,
    SpeedSlowly,
}

[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour
{
    public float moveSpeedPercent; // 0f ~ 1f
    public float time;

    public ObstacleTypes type;
    public float damage;

    private PlayerHealthSystem playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerHealth.PlayerDetection(this);
    }
}
