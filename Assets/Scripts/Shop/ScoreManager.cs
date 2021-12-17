using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int TotalMoney
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.TotalMoney_PlayerPrefs, 0);
        }

        set
        {
            PlayerPrefs.SetInt(Constants.TotalMoney_PlayerPrefs, value);
        }
    }

    public int CurrentScore { get; private set; }

    public int BonusMultiplier { get; private set; }

    public static ScoreManager Instance;

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
    }

    public void IncreaseTotalMoney(int points)
    {
        TotalMoney += points;
    }

    public void IncreaseCurrentScore(int points)
    {
        CurrentScore += points;
    }

    public void AddBonusMoney(int multiplier)
    {
        BonusMultiplier = multiplier;
        TotalMoney += CurrentScore * BonusMultiplier;
    }
    
    public void ResetMoneyEarnedThisRound()
    {
        BonusMultiplier = 0;
        CurrentScore = 0;
    }
}