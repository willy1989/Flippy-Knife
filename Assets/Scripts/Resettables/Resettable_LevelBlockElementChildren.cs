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
        rigidbody = GetComponent<Rigidbody>();

        originalPosition = transform.localPosition;

        originalRotation = transform.localRotation;
    }

    public override void ResetGameObject()
    {
        rigidbody.isKinematic = true;

        rigidbody.velocity = Vector3.zero;

        transform.localPosition = originalPosition;

        transform.localRotation = originalRotation;
    }
}

