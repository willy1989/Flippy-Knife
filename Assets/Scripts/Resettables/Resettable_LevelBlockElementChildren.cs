using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Resettable_LevelBlockElementChildren : Resettable
{
    private Vector3 originalPosition;

    private Quaternion originalRotation;

    new private Rigidbody rigidbody;

    private void Awake()
    {
        SetUp();
    }

    private void OnDisable()
    {
        ResetGameObject();
    }

    private void SetUp()
    {
        originalPosition = transform.localPosition;

        originalRotation = transform.localRotation;

        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void ResetGameObject()
    {
        transform.position = originalPosition;

        transform.rotation = originalRotation;

        rigidbody.isKinematic = false;

        rigidbody.velocity = Vector3.zero;
    }
}

