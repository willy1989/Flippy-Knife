using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cuttable : MonoBehaviour, IReset
{
    private Rigidbody[] halves;

    private BoxCollider boxCollider;

    private Vector3[] halvesPositions = new Vector3[2];

    private Quaternion[] halvesRotations = new Quaternion[2];

    private void Awake()
    {
        halves = GetComponentsInChildren<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        SetUp();
    }

    private void OnDisable()
    {
        ResetGameObject();
    }

    private void SetUp()
    {
        halvesPositions[0] = halves[0].transform.localPosition;
        halvesPositions[1] = halves[1].transform.localPosition;

        halvesRotations[0] = halves[0].transform.localRotation;
        halvesRotations[1] = halves[1].transform.localRotation;
    }

    public void CutInHalf()
    {
        boxCollider.enabled = false;

        foreach (Rigidbody rigidbody in halves)
        {
            rigidbody.isKinematic = false;
        }

        halves[0].AddForce(Vector3.forward * 200f);
        halves[1].AddForce(Vector3.back * 200f);
    }

    public void ResetGameObject()
    {
        boxCollider.enabled = true;

        halves[0].transform.localPosition = halvesPositions[0];
        halves[1].transform.localPosition = halvesPositions[1];

        halves[0].transform.localRotation = halvesRotations[0];
        halves[1].transform.localRotation = halvesRotations[1];

        halves[0].velocity = Vector3.zero;
        halves[1].velocity = Vector3.zero;

        foreach (Rigidbody rigidbody in halves)
        {
            rigidbody.isKinematic = true;
        }
    }
}
