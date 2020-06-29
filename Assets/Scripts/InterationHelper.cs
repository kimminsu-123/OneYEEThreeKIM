using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class InterationHelper : MonoBehaviour
{
    public float offsetDistanceX = 1f;
    public float offsetDistanceY = 1f;
    public Vector2 size = new Vector2(2, 2);

    public LayerMask checkLayer;

    private SpriteRenderer sr;
    private PlayerInput input;
    private PlayerMovement movement;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(input.Interation)
            CheckObject();
    }

    private void CheckObject()
    {
        var items = Physics2D.OverlapBoxAll(GetCenter(), size, 0f, checkLayer);
        if (items.Length <= 0)
            return;

        var linqItems = (from item in items
                        orderby item
                        select item).ToArray();

        var nearItem = linqItems[0].GetComponent<HealItem>();
        if (nearItem != null)
        {
            nearItem.Eat();
        }
    }
    
    private Vector3 GetCenter()
    {
        Vector3 center = transform.position;

        var xOffset = 0f;
        var yOffset = 0f;

        switch (movement.DirEnum)
        {
            case Direction.Up:
                yOffset = offsetDistanceY * 2f;
                break;
            case Direction.Down:
                break;
            case Direction.Horizontal:
                xOffset = GetX() * offsetDistanceX;
                yOffset = offsetDistanceY;
                break;
        }

        center.x += xOffset;
        center.y += yOffset;

        return center;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 center = GetCenter();

        Gizmos.DrawWireCube(center, size);
    }

    private float GetX()
    {
        return sr.flipX ? 1f : -1;
    }
}
