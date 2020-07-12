﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float H
    {
        get;
        private set;
    }

    public float V
    {
        get;
        private set;
    }

    public bool IsDash
    {
        get;
        private set;
    }

    public bool Interation
    {
        get;
        private set;
    }

    public bool IsMove
    {
        get
        {
            if (H != 0f || V != 0f)
                return true;

            return false;
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsGameover)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.Pause = !GameManager.Instance.Pause;
        }

        if (GameManager.Instance.Pause)
            return;

        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        IsDash = Input.GetKey(KeyCode.LeftShift) && IsMove;

        Interation = Input.GetKeyDown(KeyCode.Z);
    }
}
