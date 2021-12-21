using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Animator cameraAnimator;

    public static CameraManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }

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
