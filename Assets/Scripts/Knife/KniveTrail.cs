using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class KniveTrail : MonoBehaviour
{
    [SerializeField] private Rigidbody knifeRigidBody;

    private TrailRenderer trailRenderer;

    private float minEmittingVelocity = 5f;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        EmitTrail();
    }

    private void EmitTrail()
    {
        if(knifeRigidBody.angularVelocity.magnitude > minEmittingVelocity)
            trailRenderer.emitting = true;
        else
            trailRenderer.emitting = false;
    }
}
