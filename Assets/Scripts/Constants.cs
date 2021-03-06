using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    // Tags
    public const string Stabbable_Tag = "Stabbable";
    public const string Cuttable_Tag = "Cuttable";
    public const string DeadlyToKnife_Tag = "DeadlyToKnife";
    public const string BonusBlock_Tag = "BonusBlock";

    // Player prefs
    public const string TotalMoney_PlayerPrefs = "TotalMoney";
    public const string LevelsUnlocked = "LevelsUnlocked";

    // Animation
    public const string KnifeSlice_Trigger = "Slice";

    public const string BladeIdle_AnimationState = "Blade idle state";

    public const string BladeSlice_AnimationState = "Blade slice state";

    public const string FollowCamera_State = "Follow camera";
    public const string StartCamera_State = "Start camera";
    public const string LevelEndCamera_State = "End camera";
}
