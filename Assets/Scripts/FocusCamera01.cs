using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FocusCamera01 : MonoBehaviour
{
    public GameObject go;

    public float pixelPerUnit = 100;

    public Rect bigRect;
    public Rect smallRect;

    private Vector2 startPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        smallRect = new Rect(bigRect.center, bigRect.size);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        startPos = go.transform.position;
        cam.transform.position = startPos;
    }

    void Update()
    {
        SetSmallRectRange();

        CameraMove();
    }

    private void CameraMove()
    {
        if (go != null)
        {
            var targetPos = go.transform.position - Vector3.forward;

            targetPos.x = Mathf.Clamp(targetPos.x, smallRect.xMin, smallRect.xMax);
            targetPos.y = Mathf.Clamp(targetPos.y, smallRect.yMin, smallRect.yMax);

            cam.transform.position = targetPos;
        }
    }

    private void SetSmallRectRange()
    {
        var halfHeightCam = cam.orthographicSize;
        var halfWidthCam = cam.aspect * halfHeightCam;

        smallRect.x = bigRect.x + halfWidthCam;
        smallRect.width = bigRect.width - halfWidthCam * 2f;

        smallRect.y = bigRect.y + halfHeightCam;
        smallRect.height = bigRect.height - halfHeightCam * 2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bigRect.center, bigRect.size);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(smallRect.center, smallRect.size);
    }
}
