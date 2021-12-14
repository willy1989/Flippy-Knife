using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private Rigidbody rigidBody;

    private bool readyToMove = false;

    private Vector3 movementForceVector = new Vector3(400f, 400f, 0f);
    private Vector3 rotateForceVector = new Vector3(0f, 0f, -350f);

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
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddTorque(rotateForceVector);
        rigidBody.AddForce(movementForceVector);
    }
}
