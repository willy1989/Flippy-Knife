using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private KnifeAnimator knifeAnimator;

    [SerializeField] private Vector3 movementForceVector;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private bool movementAllowed = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public void SetUpSword()
    {
        knifeAnimator = GetComponent<KnifeAnimator>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
        InputManager.Instance.ActionButtonPressedEvent += PrepareForJump;
    }

    private void FixedUpdate()
    {
        if(readyToMove == true && movementAllowed == true)
        {
            Move();
            readyToMove = false;
        }
    }

    private void PrepareForJump()
    {
        readyToMove = true;
    }

    private void Move()
    {
        if (rigidBody.constraints == RigidbodyConstraints.FreezeAll)
            UnFreezeMovement();

        knifeAnimator.PlaySliceAnimation();
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(movementForceVector);
        SoundManager.Instance.PlayJumpSound();
    }

    public void FreezeMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        knifeAnimator.DisableAnimation();
    }

    private void UnFreezeMovement()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
                                RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        knifeAnimator.EnableAnimation();
    }

    public void DisableMovement()
    {
        movementAllowed = false;
        FreezeMovement();
    }

    public void EnableMovement()
    {
        movementAllowed = true;
        UnFreezeMovement();
    }

    public void ResetToStartPosition()
    {
        knifeAnimator.PlayIdleAnimation();
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
