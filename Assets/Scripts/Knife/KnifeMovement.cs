using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private CutKnife cutKnife;

    [SerializeField] private Vector3 movementForce;

    [SerializeField] private Vector3 pushBackForce;

    [SerializeField] private Vector3 torqueImpulse;

    [SerializeField] private Vector3 baseTorque;

    private float maxAngularVelocity = 12f;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private bool movementAllowed = false;

    private float autoTorqueCountDownShortDuration = 2f;

    private float autoTorqueCountDownLongDuration = 4f;

    private float jumpCountDownDuration = 0.75f;

    private float jumpCurrentCountDown;

    private float autoTorqueCurrentCountDown;

    private bool pushBackReady = false;

    private float baseAngularDrag = 2f;

    private float increasedAngularDrag = 15f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public void SetUpSword()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxAngularVelocity = maxAngularVelocity;
        rigidBody.angularDrag = baseAngularDrag;
        autoTorqueCurrentCountDown = autoTorqueCountDownLongDuration;
        InputManager.Instance.ActionButtonPressedEvent += PrepareForJump;
    }

    private void FixedUpdate()
    {
        if (movementAllowed == false)
            return;

        if (cutKnife.CutInARow == true)
        {
            return;
        }  

        if (pushBackReady == true)
        {
            PushBack();
            return;
        }

        if(readyToMove == true)
        {
            Move();
            Rotate();
            jumpCurrentCountDown = jumpCountDownDuration;
            autoTorqueCurrentCountDown = autoTorqueCountDownLongDuration;
            readyToMove = false;
        }

        if (transform.localRotation.eulerAngles.z > 250 && 
            transform.localRotation.eulerAngles.z < 275 &&
            jumpCurrentCountDown <= 0)
        {
            rigidBody.angularDrag = increasedAngularDrag;
        }
            
        else
        {
            rigidBody.angularDrag = baseAngularDrag;
        }

        BaseTorque();

        AutoTorque();

        autoTorqueCurrentCountDown -= Time.fixedDeltaTime;
        jumpCurrentCountDown -= Time.fixedDeltaTime;
    }

    private void PrepareForJump()
    {
        readyToMove = true;
    }

    private void Move()
    {
        if (rigidBody.constraints == RigidbodyConstraints.FreezeAll)
            UnFreezeMovement();

        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(movementForce * Time.fixedDeltaTime);
        SoundManager.Instance.PlayJumpSound();
    }

    private void Rotate()
    {
            rigidBody.AddTorque(torqueImpulse * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void BaseTorque()
    {
            rigidBody.AddTorque(baseTorque * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void AutoTorque()
    {
        if (autoTorqueCurrentCountDown <= 0 && transform.localRotation.eulerAngles.z > 200 && transform.localRotation.eulerAngles.z < 250)
        {
            rigidBody.velocity = Vector3.zero;
            Rotate();
            autoTorqueCurrentCountDown = autoTorqueCountDownShortDuration;
        }
    }

    public void PreparePushBack()
    {
        pushBackReady = true;
    }

    private void PushBack()
    {
        rigidBody.velocity = Vector3.zero;

        rigidBody.angularDrag = baseAngularDrag;

        rigidBody.AddForce(pushBackForce * Time.fixedDeltaTime);

        pushBackReady = false;
    }

    public void FreezeMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void UnFreezeMovement()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
                                RigidbodyConstraints.FreezeRotationY;
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
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
