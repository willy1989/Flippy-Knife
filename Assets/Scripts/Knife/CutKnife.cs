using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutKnife : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Cuttable_Tag) == true)
        {
            other.GetComponent<Cuttable>().CutInHalf();
            ScoreManager.Instance.IncreaseTotalMoney(points: 1);
            ScoreManager.Instance.IncreaseCurrentScore(points: 1);
            UIManager.Instance.UpdateTotalMoneyText();

            particleSystem.Play();
        }
    }
}
