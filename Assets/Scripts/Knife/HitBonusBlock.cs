using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBonusBlock : MonoBehaviour
{
    private KnifeMovement knifeMovement;

    private BoxCollider boxCollider;

    private bool bonusBlockHitThisRun = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        knifeMovement = GetComponentInParent<KnifeMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.BonusBlock_Tag) == true && bonusBlockHitThisRun == false)
        {
            knifeMovement.DisableMovement();
            ScoreManager.Instance.MutiplyCurrentScore(other.GetComponent<BonusBlock>().Multiplier);
            GamePhaseManager.Instance.ReachedLevelEnd();
            SoundManager.Instance.PlayWinSound();
            bonusBlockHitThisRun = true;
        }
    }

    public void Reset()
    {
        bonusBlockHitThisRun = false;
    }
}
