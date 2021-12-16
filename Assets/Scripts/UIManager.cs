using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelEndUI;
    [SerializeField] private Text totalMoneyText;
    [SerializeField] private Text bonusMoneyEarned;

    public static UIManager Instance;

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

    public void ToggleStartUI(bool OnOff)
    {
        startUI.SetActive(OnOff);
    }

    public void ToggleGameOverUI(bool OnOff)
    {
        gameOverUI.SetActive(OnOff);
    }

    public void ToggleLevelEndUI(bool OnOff)
    {
        levelEndUI.SetActive(OnOff);
    }

    public void UpdateBonusMoneyEarned()
    {
        bonusMoneyEarned.text = "+$"+(ScoreManager.Instance.BonusMultiplier* ScoreManager.Instance.CurrentScore).ToString();
    }

    public void UpdateTotalMoneyText()
    {
        totalMoneyText.text = "$" + ScoreManager.Instance.TotalMoney.ToString();
    }
}
