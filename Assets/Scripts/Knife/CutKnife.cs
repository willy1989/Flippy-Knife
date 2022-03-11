using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutKnife : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;

    private int cutsInRowCount = 0;

    private int cutThreshold = 3;

    private float cutTime = 0.5f;

    public bool CutInARow = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Cuttable_Tag) == true)
        {
            other.GetComponent<Cuttable>().CutInHalf();
            ScoreManager.Instance.IncreaseTotalMoney(points: 1);
            ScoreManager.Instance.IncreaseCurrentScore(points: 1);
            UIManager.Instance.UpdateTotalMoneyText();

            if (CutInARow == false && cutsInRowCount == 0)
                StartCoroutine(CountCut());

            cutsInRowCount++;
            particleSystem.Play();
        }
    }

    private IEnumerator CountCut()
    {
        yield return new WaitForSeconds(cutTime);

        if (cutsInRowCount >= cutThreshold)
        {
            CutInARow = true;
            cutsInRowCount = 0;

            StartCoroutine(CountCut());

            yield break;
        }

        CutInARow = false;
    }
}
