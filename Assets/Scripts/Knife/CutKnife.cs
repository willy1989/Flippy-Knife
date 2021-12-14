using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutKnife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Cuttable_Tag) == true)
            other.GetComponent<Cuttable>().CutInHalf();
    }
}
