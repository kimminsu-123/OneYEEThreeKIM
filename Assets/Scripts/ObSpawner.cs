using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSpawner : MonoBehaviour
{
    public GameObject[] ObPrefabs;
    public float regenTime = 15f;
    public float moveSpeedPercent;
    public int itemSpawnCount = 10;
    public int maxSpawn = 20;

    private List<Obstacle> spawnedObs = new List<Obstacle>();
    private PlayerMovement playerMovement;
    private FocusCamera01 cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<FocusCamera01>();
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
        bigRect = cam.bigRect;
        Create();
    }

    private float regenTimer = 0f;
    private int index = 0;
    private void CreateObOnce()
    {
        regenTimer += Time.deltaTime;
        if(regenTimer >= regenTime)
        {
            CreateObstacle();

            if(spawnedObs.Count > maxSpawn)
            {
                spawnedObs.RemoveAt(0);
            }
            regenTimer = 0f;
        }
    }

    private void Create()
    {
        for (int i = 0; i < itemSpawnCount; i++)
        {
            CreateObstacle();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(smallRect.center, smallRect.size);
    }

    private void CreateObstacle()
    {
        int randObs = Random.Range(0, ObPrefabs.Length);

        var x = Random.Range(smallRect.xMin, smallRect.xMax);
        var y = Random.Range(smallRect.yMin, smallRect.yMax);
        var pos = new Vector2(x, y);

        GameObject obs = Instantiate(ObPrefabs[randObs]);
        SetSmallRectRange(obs.GetComponent<Obstacle>());

        obs.transform.position = pos;
        obs.transform.rotation = Quaternion.identity;

        spawnedObs.Add(obs.GetComponent<Obstacle>());
        obs.GetComponent<Obstacle>().moveSpeedPercent = moveSpeedPercent;
    }

    public Rect bigRect;
    public Rect smallRect;
    private void SetSmallRectRange(Obstacle obstacle)
    {
        var col = obstacle.GetComponent<Collider2D>();

        var halfHeight = col.bounds.size.y / 2f;
        var halfWidth = col.bounds.size.x / 2f;

        smallRect.x = bigRect.x + halfWidth;
        smallRect.width = bigRect.width - halfWidth * 2f;

        smallRect.y = bigRect.y + halfHeight;
        smallRect.height = bigRect.height - halfHeight * 2f;
    }
}
