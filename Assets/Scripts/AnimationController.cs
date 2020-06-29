using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string name;

    private PlayerInput input;
    private PlayerMovement movement;
    private Animator animator;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        animator.SetBool("IsWalk", input.IsMove && movement.CanMove);
        animator.SetBool("IsRun", input.IsDash && movement.CanMove);

        animator.SetFloat("Horizontal", input.H);
        animator.SetFloat("Vertical", input.V);

        if (movement.CanMove)
        {
            var dir = movement.GetDirection();
            animator.SetFloat("DirectionH", dir.x);
            animator.SetFloat("DirectionV", dir.y);
        }

        animator.speed = Mathf.Max(movement.currSpeed / movement.defaultSpeed, 1f);
    }
}
