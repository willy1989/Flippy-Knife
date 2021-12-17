using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseManager : MonoBehaviour
{
    public GameObject KnifePrefab;

    private GameObject currentKnife;

    [SerializeField] private CinemachineVirtualCamera kniveFollowCamera;

    [SerializeField] private Transform knifeSpawnPosition;

    private KnifeDeath knifeDeath;

    private KnifeMovement knifeMovement;

    [Header("Buttons")]

    [SerializeField] private Button gameOverRestartButton;

    [SerializeField] private Button levelEndRestartButton;

    [SerializeField] private Button enableMovementButton;

    public static GamePhaseManager Instance;

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

    private void Start()
    {
        SpawnNewKnife();

        UIManager.Instance.UpdateTotalMoneyText();
        LevelBuilder.Instance.CreateLevel();

        gameOverRestartButton.onClick.RemoveAllListeners();
        gameOverRestartButton.onClick.AddListener(ResetGame);

        levelEndRestartButton.onClick.RemoveAllListeners();
        levelEndRestartButton.onClick.AddListener(ResetGame);

        enableMovementButton.onClick.RemoveAllListeners();
        enableMovementButton.onClick.AddListener(StartGame);
    }

    public void ResetGame()
    {
        UIManager.Instance.ToggleStartUI(OnOff: true);
        UIManager.Instance.ToggleGameOverUI(OnOff: false);
        UIManager.Instance.ToggleLevelEndUI(OnOff: false);
        knifeMovement.ResetToStartPosition();
        ScoreManager.Instance.ResetMoneyEarnedThisRound();
        UIManager.Instance.UpdateTotalMoneyText();
        LevelBuilder.Instance.ResetLevelBuilder();
        LevelBuilder.Instance.CreateLevel();
    }

    public void StartGame()
    {
        UIManager.Instance.ToggleStartUI(OnOff: false);
        knifeMovement.EnableMovement();
    }

    public void GameOver()
    {
        UIManager.Instance.UpdateTotalMoneyText();
        UIManager.Instance.ToggleGameOverUI(OnOff: true);
        knifeMovement.DisableMovement();
    }

    public void ReachedLevelEnd()
    {
        UIManager.Instance.ToggleLevelEndUI(OnOff: true);
        UIManager.Instance.UpdateBonusMoneyEarned();
        LevelBuilder.Instance.IncrementLevelUnlocked();
    }

    public void ChangeKnife(GameObject newKnifePrefab)
    {
        Destroy(currentKnife);
        KnifePrefab = newKnifePrefab;
    }

    public void SpawnNewKnife()
    {
        currentKnife = Instantiate(KnifePrefab, knifeSpawnPosition.position, Quaternion.identity);
        kniveFollowCamera.Follow = currentKnife.transform;
        knifeMovement = currentKnife.GetComponent<KnifeMovement>();
        knifeDeath = currentKnife.GetComponentInChildren<KnifeDeath>();
        knifeMovement.SetUpSword();
        knifeMovement.DisableMovement();
        knifeDeath.DeathEvent += GameOver;
    }
}
