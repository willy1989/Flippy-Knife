using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [SerializeField] private Animator kniveAnimator;

    [SerializeField] private CutKnife cutKnife;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private bool movementAllowed = false;

    private Vector3 movementForceVector = new Vector3(200f, 400f, 0f);
    private Vector3 rotateForceVector = new Vector3(0f, 0f, -200f);

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        cutKnife.TriggerEnterWithCuttableEvent += StartIdleAnimation;
        cutKnife.TriggerExitWithCuttableEvent += StartSliceAnimation;
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

        kniveAnimator.SetBool(Constants.KnifeSlice_Bool, true);

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
    
    private void StartIdleAnimation()
    {
        kniveAnimator.SetBool(Constants.KnifeSlice_Bool, false);
    }

    private void StartSliceAnimation()
    {
        kniveAnimator.SetBool(Constants.KnifeSlice_Bool, true);
    }

    public void ResetToStartPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        kniveAnimator.SetTrigger(Constants.KnifeIdle_Trigger);
    }
}
