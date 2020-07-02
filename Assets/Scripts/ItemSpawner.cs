using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    public int itemSpawnCount;

    private int suffleCount;
    private List<HealItem> spawnedItems = new List<HealItem>();

    private void Awake()
    {
        suffleCount = spawnPoints.Length * 100;
    }

    private void Start()
    {
        Init();

        EventManager.Instance.AddListener(EventType.OnDay, OnGameStatusChanged);
    }

    private void Init()
    {
        Suffle();
        CreateItem();
    }

    private void Suffle()
    {
        for(int i = 0; i < suffleCount; i++)
        {
            int rhs = Random.Range(0, spawnPoints.Length);
            int lhs = Random.Range(0, spawnPoints.Length);

            var temp = spawnPoints[rhs];
            spawnPoints[rhs] = spawnPoints[lhs];
            spawnPoints[lhs] = temp;
        }
    }

    private void CreateItem()
    {
        for(int i = 0; i < itemSpawnCount; i++)
        {
            int randItem = Random.Range(0, itemPrefabs.Length);
            GameObject item = Instantiate(itemPrefabs[randItem], spawnPoints[i]);
            item.transform.position = item.transform.parent.position;
            item.transform.rotation = item.transform.parent.rotation;

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
