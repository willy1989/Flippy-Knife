using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseManager : Singleton<GamePhaseManager>
{
    [SerializeField] private AdsManager adsManager;

    [Header("Cameras")]

    [SerializeField] private CinemachineVirtualCamera kniveFollowCamera;

    [SerializeField] private CinemachineVirtualCamera startCamera;

    [SerializeField] private CinemachineVirtualCamera levelEndCamera;

    [SerializeField] private Transform knifeSpawnPosition;

    [Header("Buttons")]

    [SerializeField] private Button gameOverRestartButton;

    [SerializeField] private Button levelEndRestartButton;

    [SerializeField] private Button enableMovementButton;

    public GameObject KnifePrefab;

    private GameObject currentKnife;

    private StabKnife stabKnife;

    private HitBonusBlock hitBonusBlock;

    private KnifeDeath knifeDeath;

    private KnifeMovement knifeMovement;

    private void Awake()
    {
        SetInstance();
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

        CameraManager.Instance.SwitchToFollowCamera();
    }

    public void ResetGame()
    {
        UIManager.Instance.ToggleStartUI(OnOff: true);
        UIManager.Instance.ToggleGameOverUI(OnOff: false);
        UIManager.Instance.ToggleLevelEndUI(OnOff: false);
        knifeMovement.ResetToStartPosition();
        ScoreManager.Instance.AddCurrentScoreToTotalMoney();
        ScoreManager.Instance.ResetCurrentScore();
        UIManager.Instance.UpdateTotalMoneyText();
        LevelBuilder.Instance.ResetLevelBuilder();
        LevelBuilder.Instance.CreateLevel();
        CameraManager.Instance.SwitchToStartCamera();
        hitBonusBlock.Reset();
        adsManager.ShowInterstitialAd();
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
        UIManager.Instance.ToggleShowRewardAdButton(OnOff: true);
        UIManager.Instance.UpdateBonusMoneyEarned();
        LevelBuilder.Instance.IncrementLevelUnlocked();
        CameraManager.Instance.SwitchToLevelEndCamera();
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
        levelEndCamera.Follow = currentKnife.transform;
        knifeMovement = currentKnife.GetComponent<KnifeMovement>();
        knifeDeath = currentKnife.GetComponentInChildren<KnifeDeath>();
        stabKnife = currentKnife.GetComponentInChildren<StabKnife>();
        hitBonusBlock = currentKnife.GetComponentInChildren<HitBonusBlock>();
        knifeMovement.SetUpSword();
        knifeMovement.DisableMovement();
        knifeDeath.DeathEvent += GameOver;
    }
}
