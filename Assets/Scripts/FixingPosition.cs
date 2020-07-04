using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectTypes
{
    Item,
    Obstacle
}
[ExecuteInEditMode]
public class FixingPosition : MonoBehaviour
{
    public LayerMask layer;
    public ObjectTypes type;

    private float radius;
    private CircleCollider2D col;
    private Rect smallRect;
    private bool isHere = false;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();

        radius = col.radius;
        col.enabled = false;
    }

    private void Update()
    {
        var checkedObj = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (checkedObj != null && !isHere && checkedObj.gameObject.GetInstanceID() != gameObject.GetInstanceID())
        {
            SetPos();
        }

        else
        {
            isHere = true;
            col.enabled = true;
        }
    }

    private void SetPos()
    {
        SetRect();

        var x = Random.Range(smallRect.xMin, smallRect.xMax);
        var y = Random.Range(smallRect.yMin, smallRect.yMax);
        var pos = new Vector2(x, y);

        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    private void SetRect()
    {
        switch (type)
        {
            case ObjectTypes.Item:
                smallRect = GameObject.FindWithTag("ItemSpawner").GetComponent<ItemSpawner>().smallRect;
                break;

            case ObjectTypes.Obstacle:
                smallRect = GameObject.FindWithTag("ObSpawner").GetComponent<ObSpawner>().smallRect;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, col.radius);
    }
}
