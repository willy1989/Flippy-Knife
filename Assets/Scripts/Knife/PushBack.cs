using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    private void OnTriggerEnter(Collider other)
    {
        knifeMovement.PreparePushBack();
    }
}
