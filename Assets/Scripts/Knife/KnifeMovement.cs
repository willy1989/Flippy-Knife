using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Animator kniveAnimator;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private bool movementAllowed = false;

    private Vector3 movementForceVector = new Vector3(200f, 400f, 0f);

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
        inputManager.ActionButtonPressedEvent += PrepareForJump;
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

        kniveAnimator.SetTrigger(Constants.KnifeSlice_Trigger);
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(movementForceVector);
    }

    public void FreezeMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        kniveAnimator.speed = 0;
    }

    private void UnFreezeMovement()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
                                RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        kniveAnimator.speed = 1;
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
