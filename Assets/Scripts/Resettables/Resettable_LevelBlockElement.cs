using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Resettable_LevelBlockElement : Resettable
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
    }

    public override void ResetGameObject()
    {
        boxCollider.enabled = true;
    }
}
