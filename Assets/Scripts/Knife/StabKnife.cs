using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    private bool bonusBlockHitThisRun = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Stabbable_Tag) == true)
        {
            knifeMovement.FreezeMovement();
            SoundManager.Instance.PlayStabSound();
        }
 
        else if (other.CompareTag(Constants.BonusBlock_Tag) == true && bonusBlockHitThisRun == false)
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
