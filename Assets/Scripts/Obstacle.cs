using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTypes
{
    None,
    SpeedSlowly,
}

[RequireComponent(typeof(CircleCollider2D))]
public class Obstacle : MonoBehaviour
{
    public float moveSpeedPercent; // 0f ~ 1f
    public float time;

    public ObstacleTypes type;
    public float damage;

    private PlayerHealthSystem playerHealth;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.PlayerDetection(this);
            Destroy(gameObject);
        }
    }
}
