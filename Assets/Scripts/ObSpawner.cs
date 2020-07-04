using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSpawner : MonoBehaviour
{
    public GameObject ObPrefab;
    public Transform[] spawnPoints;
    public float regenTime = 15f;
    public float moveSpeedPercent;

    public int itemSpawnCount = 10;

    private int suffleCount;
    private List<Obstacle> spawnedObs = new List<Obstacle>();

    private PlayerMovement playerMovement;

    private void Awake()
    {
        suffleCount = spawnPoints.Length * 100;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Init();
        playerMovement.SetMinSpeed(playerMovement.defaultSpeed * moveSpeedPercent, playerMovement.dashSpeed * moveSpeedPercent);
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
            GameObject obs = Instantiate(ObPrefab, spawnPoints[index]);
            obs.transform.position = obs.transform.parent.position;
            obs.transform.rotation = obs.transform.parent.rotation;

            spawnedObs.Add(obs.GetComponent<Obstacle>());
            obs.GetComponent<Obstacle>().moveSpeedPercent = moveSpeedPercent;
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
            GameObject obs = Instantiate(ObPrefab, spawnPoints[i]);
            obs.transform.position = obs.transform.parent.position;
            obs.transform.rotation = obs.transform.parent.rotation;

            spawnedObs.Add(obs.GetComponent<Obstacle>());
            obs.GetComponent<Obstacle>().moveSpeedPercent = moveSpeedPercent;

            index = i;
        }
    }
}
