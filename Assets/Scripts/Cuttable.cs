using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cuttable : MonoBehaviour
{
    private Rigidbody[] halves;

    private BoxCollider boxCollider;

    private void Awake()
    {
        halves = GetComponentsInChildren<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
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
}
