using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    [SerializeField] private KnifeContactCollider knifeContactCollider;

    private KnifeMovement knifeMovement;

    private Collider[] colliders;

    private bool stabbed = false;


    private void Awake()
    {
        colliders = GetComponents<Collider>();
        knifeMovement = GetComponentInParent<KnifeMovement>();
        knifeContactCollider.UnstabEvent += Unstab;
    }

    private void Unstab()
    {
        stabbed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Stabbable_Tag) == true && stabbed == false)
        {
            stabbed = true;
            knifeMovement.FreezeMovement();
            SoundManager.Instance.PlayStabSound();
        }
    }
}
