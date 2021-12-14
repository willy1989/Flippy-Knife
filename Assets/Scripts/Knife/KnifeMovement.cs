using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private Vector3 movementForceVector = new Vector3(200f, 400f, 0f);
    private Vector3 rotateForceVector = new Vector3(0f, 0f, -200f);

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        inputManager.ActionButtonPressedEvent += PrepareForJump;
    }

    private void FixedUpdate()
    {
        if(readyToMove == true)
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

        rigidBody.velocity = Vector3.zero;
        rigidBody.AddTorque(rotateForceVector);
        rigidBody.AddForce(movementForceVector);
    }

    public void FreezeMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void UnFreezeMovement()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
