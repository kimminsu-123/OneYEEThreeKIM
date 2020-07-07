using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject uniqeItemPrefab;
    public float uniqeItemSpawnPercentage = 1.0f;

    public GameObject[] itemPrefabs;

    public int itemSpawnCount;

    private List<HealItem> spawnedItems = new List<HealItem>();

    public Rect bigRect;
    public Rect smallRect;

    private FocusCamera01 cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<FocusCamera01>();
    }

    private void Start()
    {
        Init();

        EventManager.Instance.AddListener(EventType.OnDay, OnGameStatusChanged);
    }

    private void Init()
    {
        bigRect = cam.bigRect;

        CreateItem();
    }

    private void SetSmallRectRange(HealItem currItem)
    {
        var col = currItem.GetComponent<Collider2D>();

        var halfHeight = col.bounds.size.y / 2f;
        var halfWidth = col.bounds.size.x / 2f;

        smallRect.x = bigRect.x + halfWidth;
        smallRect.width = bigRect.width - halfWidth * 2f;

        smallRect.y = bigRect.y + halfHeight;
        smallRect.height = bigRect.height - halfHeight * 2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(smallRect.center, smallRect.size);
    }

    private void CreateItem()
    {
        for(int i = 0; i < itemSpawnCount; i++)
        {
            GameObject item;
            float uniqeRand = Random.Range(0f, 100f);
            if (uniqeRand <= uniqeItemSpawnPercentage)
            {
                item = Instantiate(uniqeItemPrefab);
            }
            else
            {
                int randItem = Random.Range(0, itemPrefabs.Length);
                item = Instantiate(itemPrefabs[randItem]);
            }

            SetSmallRectRange(item.GetComponent<HealItem>());

            var x = Random.Range(smallRect.xMin, smallRect.xMax);
            var y = Random.Range(smallRect.yMin, smallRect.yMax);
            var pos = new Vector2(x, y);

            item.transform.position = pos;
            item.transform.rotation = Quaternion.identity;

            spawnedItems.Add(item.GetComponent<HealItem>());
        }
    }

    private void RemoveItem()
    {
        foreach(var item in spawnedItems)
            Destroy(item.gameObject);

        spawnedItems.Clear();
    }

    private void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                break;
            case EventType.EatFoodEnd:
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
            case EventType.OnDay:
                RemoveItem();
                Init();
                break;
            case EventType.OnTimeChange:
                break;
        }
    }
}
