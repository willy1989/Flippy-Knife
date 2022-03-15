using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDeath : MonoBehaviour
{
    public Action DeathEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.DeadlyToKnife_Tag) == true)
        {
            DeathEvent();
            SoundManager.Instance.PlayLoseSound();
        }   
    }
}
