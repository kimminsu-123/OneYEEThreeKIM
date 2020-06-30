using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSpawner : MonoBehaviour
{
    public GameObject ObPrefab;
    public Transform[] spawnPoints;
    public float regenTime = 15f;

    public int itemSpawnCount = 10;

    private int suffleCount;
    private List<HealItem> spawnedObs = new List<HealItem>();

    private void Awake()
    {
        suffleCount = spawnPoints.Length * 100;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CreateObOnce();
    }

    private void Init()
    {
        Suffle();
        Create();
    }

    private float regenTimer = 0f;
    private int index = 0;
    private void CreateObOnce()
    {
        regenTimer += Time.deltaTime;
        if(regenTimer >= regenTime)
        {
            GameObject item = Instantiate(ObPrefab, spawnPoints[index]);
            item.transform.position = item.transform.parent.position;
            item.transform.rotation = item.transform.parent.rotation;

            spawnedObs.Add(item.GetComponent<HealItem>());

            index++;

            if(spawnedObs.Count > spawnPoints.Length)
            {
                index = 0;
                spawnedObs.RemoveAt(0);
            }
            regenTimer = 0f;
        }
    }

    private void Suffle()
    {
        for (int i = 0; i < suffleCount; i++)
        {
            int rhs = Random.Range(0, spawnPoints.Length);
            int lhs = Random.Range(0, spawnPoints.Length);

            var temp = spawnPoints[rhs];
            spawnPoints[rhs] = spawnPoints[lhs];
            spawnPoints[lhs] = temp;
        }
    }

    private void Create()
    {
        for (int i = 0; i < itemSpawnCount; i++)
        {
            GameObject item = Instantiate(ObPrefab, spawnPoints[i]);
            item.transform.position = item.transform.parent.position;
            item.transform.rotation = item.transform.parent.rotation;

            spawnedObs.Add(item.GetComponent<HealItem>());

            index = i;
        }
    }
}
