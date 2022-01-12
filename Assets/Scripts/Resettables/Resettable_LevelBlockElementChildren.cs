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
        rigidbody = GetComponent<Rigidbody>();

        originalPosition = transform.localPosition;

        originalRotation = transform.localRotation;
    }

    protected override void ResetGameObject()
    {
        rigidbody.isKinematic = true;

        rigidbody.velocity = Vector3.zero;

        transform.localPosition = originalPosition;

        transform.localRotation = originalRotation;
    }
}

