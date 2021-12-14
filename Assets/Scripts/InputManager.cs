using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    public bool ActionButtonIsPressed { get; private set; } = false;

    private void Update()
    {
        ActionButtonIsPressed = UpdateActionButtonStatus();
    }

    private bool UpdateActionButtonStatus()
    {
        if (Input.touchCount < 1)
            return false;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            return true;

        return false;
    }
}
