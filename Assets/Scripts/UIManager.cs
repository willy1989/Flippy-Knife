using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI elements")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject levelEndUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Text totalMoneyText;
    [SerializeField] private Text bonusMoneyEarned;
    [SerializeField] private Button watchRewardAdButton;

    private void Awake()
    {
        SetInstance();
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

    public void ToggleShopUI(bool OnOff)
    {
        shopUI.SetActive(OnOff);
    }

    public void UpdateBonusMoneyEarned()
    {
        bonusMoneyEarned.text = "+ "+(ScoreManager.Instance.CurrentScore).ToString();
    }

    public void UpdateTotalMoneyText()
    {
        totalMoneyText.text = ScoreManager.Instance.TotalMoney.ToString();
    }

    public void ToggleShowRewardAdButton(bool OnOff)
    {
        watchRewardAdButton.gameObject.SetActive(OnOff);
    }
}
