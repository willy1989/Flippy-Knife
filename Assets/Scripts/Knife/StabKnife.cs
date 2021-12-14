using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.Stabbable_Tag) == true)
            knifeMovement.FreezeMovement();
    }
}
