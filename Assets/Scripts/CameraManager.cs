using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Animator cameraAnimator;

    private void Awake()
    {
        SetInstance();

        cameraAnimator = GetComponent<Animator>();
    }

    public void SwitchToLevelEndCamera()
    {
        cameraAnimator.Play(Constants.LevelEndCamera_State);
    }

    public void SwitchToFollowCamera()
    {
        cameraAnimator.Play(Constants.FollowCamera_State);
    }

    public void SwitchToStartCamera()
    {
        cameraAnimator.Play(Constants.StartCamera_State);
    }
}
