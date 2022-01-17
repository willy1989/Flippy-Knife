using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnife : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    [SerializeField] private Animator kniveAnimator;

    private bool bonusBlockHitThisRun = false;

    private Collider[] colliders;

    private void Awake()
    {
        colliders = GetComponents<Collider>();
    }

    private void Update()
    {
        ToggleColliders();
    }

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

    /// <summary>
    /// We disable the colliders when the blade is spinning,
    /// to prevent the following scenario from happening:
    /// The blade was stabbed in an platform,
    /// when the player flips the knife again, it will immediately get stuck again,
    /// as the colliders will collide with the platform right away.
    /// So by disabling the colliders we let the blade jump free, 
    /// for at least the duration of the flip animation
    /// </summary>
    private void ToggleColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = !kniveAnimator.GetCurrentAnimatorStateInfo(0).IsName(Constants.BladeSlice_AnimationState);
        }
    }

    public void Reset()
    {
        bonusBlockHitThisRun = false;
    }
}
