using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Button restartButton;

    [SerializeField] private Button enableMovementButton;

    [SerializeField] private KnifeDeath knifeDeath;

    [SerializeField] private KnifeMovement knifeMovement;

    private void Awake()
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(ResetGame);

        enableMovementButton.onClick.RemoveAllListeners();
        enableMovementButton.onClick.AddListener(StartGame);

        knifeDeath.DeathEvent += GameOver;
    }

    private void Start()
    {
        knifeMovement.DisableMovement();
    }

    public void ResetGame()
    {
        uiManager.ToggleStartUI(OnOff: true);
        uiManager.ToggleGameOverUI(OnOff: false);
        knifeMovement.ResetToStartPosition();
    }

    public void StartGame()
    {
        uiManager.ToggleStartUI(OnOff: false);
        knifeMovement.EnableMovement();
    }

    public void GameOver()
    {
        uiManager.ToggleGameOverUI(OnOff: true);
        knifeMovement.DisableMovement();
    }
}
