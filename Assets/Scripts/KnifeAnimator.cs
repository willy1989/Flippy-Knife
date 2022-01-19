using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float animationSpeed = 2;

    public bool SliceAnimationOnGoing 
    {
        get
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(Constants.BladeSlice_AnimationState);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlaySliceAnimation()
    {
        animator.SetTrigger(Constants.KnifeSlice_Trigger);
    }

    public void PlayIdleAnimation()
    {
        animator.Play(Constants.BladeIdle_AnimationState);
    }

    public void DisableAnimation()
    {
        animator.speed = 0;
    }

    public void EnableAnimation()
    {
        animator.speed = animationSpeed;
    }
}


