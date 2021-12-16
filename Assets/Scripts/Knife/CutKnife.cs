using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutKnife : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private UIManager uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Cuttable_Tag) == true)
        {
            other.GetComponent<Cuttable>().CutInHalf();
            scoreManager.IncreaseTotalMoney(points: 1);
            scoreManager.IncreaseCurrentScore(points: 1);
            uiManager.UpdateTotalMoneyText();
        }
    }
}
