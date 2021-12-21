using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private Animator kniveAnimator;

    [Header("Sword stat")]

    [SerializeField] private Vector3 movementForceVector;

    [SerializeField] private float rotationSpeed;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private bool movementAllowed = false;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public void SetUpSword()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        kniveAnimator.speed = rotationSpeed;
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

        kniveAnimator.SetTrigger(Constants.KnifeSlice_Trigger);
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(movementForceVector);
        SoundManager.Instance.PlayJumpSound();
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

        kniveAnimator.speed = rotationSpeed;
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
        kniveAnimator.Play("Knife blade idle");
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
