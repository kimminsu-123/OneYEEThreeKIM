
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public enum EntityCategory
    {
        None,
        Player,
        Npc
    }
    public EntityCategory category = EntityCategory.None;

    public float maxHealth;
    public float currHealth = 0;
    public float CurrHealth
    {
        get
        {
            return currHealth;
        }

        protected set
        {
            currHealth = Mathf.Clamp(value, 0f, maxHealth);
            ChangeHealthUI();
            Die();
        }
    }

    protected SpriteRenderer m_Sr;
    protected Transform cashedTr;
    protected Rigidbody2D cashedRg;

    protected void Awake()
    {
        m_Sr = GetComponent<SpriteRenderer>();
        cashedTr = GetComponent<Transform>();
        cashedRg = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        CurrHealth = maxHealth;
    }

    public void Die()
    {
        if(CurrHealth <= 0f)
        {
            EventManager.Instance.PostNitification(EventType.Gameover, this);

            Destroy(gameObject);
        }
    }

    protected virtual void ChangeHealthUI()
    {

    }
}
