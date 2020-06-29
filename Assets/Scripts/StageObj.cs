using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StageObj : MonoBehaviour
{
    public float alpha = 0.5f;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("HideCol") )
        {
            ChangeAlpha(alpha);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HideCol"))
        {
            ChangeAlpha(1f);
        }
    }

    private void ChangeAlpha(float alpha)
    {
        Color newColor = sr.color;
        newColor.a = alpha;
        sr.color = newColor;
    }
}
