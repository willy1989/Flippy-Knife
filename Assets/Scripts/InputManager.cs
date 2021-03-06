using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Action ActionButtonPressedEvent;

    private void Awake()
    {
        SetInstance();
    }

    private void Update()
    {
        UpdateActionButtonStatus();
    }

    private void UpdateActionButtonStatus()
    {
        if (Input.touchCount < 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            ActionButtonPressedEvent();
    }
}
