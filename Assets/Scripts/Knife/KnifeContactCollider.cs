using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeContactCollider : MonoBehaviour
{
    public Action UnstabEvent;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.Stabbable_Tag) == true)
        {
            UnstabEvent.Invoke();
        }
    }
}
