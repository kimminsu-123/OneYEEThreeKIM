using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerMovement movement;
    private Animator animator;

    private bool isRainbow;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EventType.OnChangedRainbow, OnGameStatusChanged);
    }

    void Update()
    {
        animator.SetBool("IsWalk", input.IsMove && movement.CanMove);
        animator.SetBool("IsRun", input.IsDash && movement.CanMove);

        animator.SetFloat("Horizontal", input.H);
        animator.SetFloat("Vertical", input.V);

        animator.SetBool("isRainbow", isRainbow);

        if (movement.CanMove)
        {
            var dir = movement.GetDirection();
            animator.SetFloat("DirectionH", dir.x);
            animator.SetFloat("DirectionV", dir.y);
        }

        animator.speed = Mathf.Max(movement.CurrSpeed / movement.defaultSpeed, 1f);
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
            case EventType.OnChangedRainbow:
                isRainbow = (bool)args;
                break;
        }
    }

    //레인보우 이벤트 받기
    //레인보우 변수 true로 설정

}
