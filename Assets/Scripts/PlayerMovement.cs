using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Up,
    Down,
    Horizontal,
}
[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{
    [Header("대쉬 게이지")]
    public float maxDashGauge;
    public float dashPerSec;
    private float currDashGauge;
    public float CurrDashGauge
    {
        get
        {
            return currDashGauge;
        }
        private set
        {
            currDashGauge = value;
            UIManager.Instance.playerInfoPanel.UpdateDashGauge(CurrDashGauge / maxDashGauge);
        }
    }
    public float feelDashPerSec;
    private float feelDashTimer;

    [Header("이동")]
    private float currSpeed;
    public float CurrSpeed
    {
        get
        {
            return currSpeed;
        }
        private set
        {
            currSpeed = value;
        }
    }
    public float defaultSpeed;
    public float dashSpeed;

    private PlayerInput playerInput;
    private Rigidbody2D cachedRg;
    private Transform cachedTr;
    private SpriteRenderer sr;

    public bool CanMove {
        get;
        private set;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        cachedRg = GetComponent<Rigidbody2D>();
        cachedTr = GetComponent<Transform>();

        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        CurrDashGauge = maxDashGauge;
        CanMove = true;

        EventManager.Instance.AddListener(EventType.EatFoodBegin, OnGameStatusChanged);
        EventManager.Instance.AddListener(EventType.EatFoodEnd, OnGameStatusChanged);

        saveSpeed = defaultSpeed;
        saveDash = dashSpeed;
    }

    void Update()
    {
        FeelDash();
        SetDirection();
    }

    private void FixedUpdate()
    {
        //movement
        if (!CanMove)
        {
            cachedRg.velocity = Vector2.zero;
            return;
        }

        CurrSpeed = defaultSpeed;
        
        if (playerInput.IsDash)
        {
            Dash();
        }

        Move();
    }

    private void Move()
    {
        var h = playerInput.H;
        var v = playerInput.V;

        var dir = new Vector2(h, v);
        var velocity = dir * CurrSpeed;

        if (h != 0f)
            sr.flipX = h > 0f ? true : false;

        cachedRg.velocity = velocity;
    }

    private void Dash()
    {
        //Check DashGauge
        if (CurrDashGauge <= 0)
            return;

        //ChangeCurrSpeed
        CurrSpeed = dashSpeed;

        //Update Gauge
        var dashValue = dashPerSec * Time.fixedDeltaTime;
        CurrDashGauge = Mathf.Max(CurrDashGauge - dashValue, 0f);
    }

    private void FeelDash()
    {
        //Check Dash
        if (playerInput.IsDash)
        {
            feelDashTimer = 0f;
            return;
        }

        //Check Timer
        feelDashTimer += Time.deltaTime;
        if (feelDashTimer < 1f)
            return;

        //Feel Dash
        var feelAmount = feelDashPerSec * Time.deltaTime;
        CurrDashGauge = Mathf.Min(CurrDashGauge + feelAmount, maxDashGauge);
    }

    private void OnGameStatusChanged(EventType et, Component sender, object args = null)
    {
        switch (et)
        {
            case EventType.Opening:
                break;
            case EventType.EatFoodBegin:
                CanMove = false;
                break;
            case EventType.EatFoodEnd:
                CanMove = true;
                break;
            case EventType.Story:
                break;
            case EventType.Gameover:
                break;
        }
    }

    private Vector2 currDir;
    public Vector2 GetDirection()
    {
        if(playerInput.H != 0f || playerInput.V != 0f)
        {
            currDir = new Vector2(playerInput.H, playerInput.V);
        }

        return currDir;
    }

    public Direction DirEnum
    {
        get;
        private set;
    }
    public void SetDirection()
    {
        var dir = GetDirection();

        if(dir.y > 0f)
        {
            var yAbs = Mathf.Abs(dir.y);
            var xAbs = Mathf.Abs(dir.x);
            DirEnum = yAbs < xAbs ? Direction.Horizontal : Direction.Up;
        }

        else
        {
            var yAbs = Mathf.Abs(dir.y);
            var xAbs = Mathf.Abs(dir.x);
            DirEnum = yAbs < xAbs? Direction.Horizontal : Direction.Down;
        }
    }
    private float minSpeed;
    private float minDash;

    [HideInInspector]
    public float saveSpeed;
    [HideInInspector]
    public float saveDash;
    public void ChangeSpeed(float speed, float dash)
    {
        defaultSpeed = Mathf.Max(speed, minSpeed);
        dashSpeed = Mathf.Max(dash, minDash);
    }

    public void SetMinSpeed(float speed, float dash)
    {
        minSpeed = speed;
        minDash = dash;
    }
}
