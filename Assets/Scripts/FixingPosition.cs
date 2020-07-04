using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingPosition : MonoBehaviour
{
    private ItemSpawner spawner;
    private Rect smallRect;

    private void Awake()
    {
        spawner = GameObject.FindWithTag("ItemSpawner").GetComponent<ItemSpawner>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        smallRect = spawner.smallRect;
        if (collision.tag == "StageObj")
        {
            SetPos();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        smallRect = spawner.smallRect;
        if (collision.collider.tag == "Item" || collision.collider.tag == "StageObj")
        {
            SetPos();
        }   
    }

    private void SetPos()
    {
        var x = Random.Range(smallRect.xMin, smallRect.xMax);
        var y = Random.Range(smallRect.yMin, smallRect.yMax);
        var pos = new Vector2(x, y);

        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }
}
