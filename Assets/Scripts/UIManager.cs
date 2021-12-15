using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [Header("UI elements")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelEndUI;
    [SerializeField] private Text totalMoneyText;
    [SerializeField] private Text bonusMoneyEarned;

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
        bonusMoneyEarned.text = "+$"+(scoreManager.BonusMultiplier*scoreManager.CurrentScore).ToString();
    }

    public void UpdateTotalMoneyText()
    {
        totalMoneyText.text = "$" + scoreManager.TotalMoney.ToString();
    }
}
