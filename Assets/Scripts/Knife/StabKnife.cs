using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    private KnifeMovement knifeMovement;

    private Collider[] colliders;

    private void Awake()
    {
        colliders = GetComponents<Collider>();
        knifeMovement = GetComponentInParent<KnifeMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Stabbable_Tag) == true)
        {
            knifeMovement.FreezeMovement();
            SoundManager.Instance.PlayStabSound();
        }
    }
}
