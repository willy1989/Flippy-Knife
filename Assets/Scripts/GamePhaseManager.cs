using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private KnifeDeath knifeDeath;

    [SerializeField] private KnifeMovement knifeMovement;

    [SerializeField] private LevelBuilder levelBuilder;

    [Header("Buttons")]

    [SerializeField] private Button gameOverRestartButton;

    [SerializeField] private Button levelEndRestartButton;

    [SerializeField] private Button enableMovementButton;

    private void Awake()
    {
        gameOverRestartButton.onClick.RemoveAllListeners();
        gameOverRestartButton.onClick.AddListener(ResetGame);

        levelEndRestartButton.onClick.RemoveAllListeners();
        levelEndRestartButton.onClick.AddListener(ResetGame);

        enableMovementButton.onClick.RemoveAllListeners();
        enableMovementButton.onClick.AddListener(StartGame);

        knifeDeath.DeathEvent += GameOver;
    }

    private void Start()
    {
        knifeMovement.DisableMovement();
        uiManager.UpdateTotalMoneyText();
        levelBuilder.CreateLevel();
    }

    public void ResetGame()
    {
        uiManager.ToggleStartUI(OnOff: true);
        uiManager.ToggleGameOverUI(OnOff: false);
        uiManager.ToggleLevelEndUI(OnOff: false);
        knifeMovement.ResetToStartPosition();
        scoreManager.ResetMoneyEarnedThisRound();
        uiManager.UpdateTotalMoneyText();
        levelBuilder.ResetLevelBuilder();
        levelBuilder.CreateLevel();
    }

    public void StartGame()
    {
        uiManager.ToggleStartUI(OnOff: false);
        knifeMovement.EnableMovement();
    }

    public void GameOver()
    {
        uiManager.UpdateTotalMoneyText();
        uiManager.ToggleGameOverUI(OnOff: true);
        knifeMovement.DisableMovement();
    }

    public void ReachedLevelEnd()
    {
        uiManager.ToggleLevelEndUI(OnOff: true);
        uiManager.UpdateBonusMoneyEarned();
    }
}
