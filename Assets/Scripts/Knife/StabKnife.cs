using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private GamePhaseManager gamePhaseManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Stabbable_Tag) == true)
            knifeMovement.FreezeMovement();

        else if (other.CompareTag(Constants.BonusBlock_Tag) == true)
        {
            knifeMovement.DisableMovement();
            scoreManager.AddBonusMoney(other.GetComponent<BonusBlock>().Multiplier);
            gamePhaseManager.ReachedLevelEnd();
        }
            
    }
}
