using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private Button watchRewardAdButton;

    private bool testMode = false;

    private const string gameId = "4542369";

    private const string interstitialAdPlacementID = "Interstitial_Android";
    private const string bannerAdPlacementID = "Banner_Android";
    private const string rewardedAdPlacementID = "Rewarded_Android";

    private const int bannerAdTryToShowInterval = 2;
    private float showBannerAdTimeOut = 120f;

    private const float interstitialAdCountDown = 60f;
    private float currentInterstitialAdCountDown = interstitialAdCountDown;
    private IEnumerator interstitialAdCoroutine;

    private void Awake()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);

        Advertisement.Banner.Load(bannerAdPlacementID);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerAdd());
        
        Advertisement.Load(interstitialAdPlacementID);
        ResetInterstitialAdCountDown();

        Advertisement.Load(rewardedAdPlacementID);
        watchRewardAdButton.onClick.RemoveAllListeners();
        watchRewardAdButton.onClick.AddListener(ShowRewardAd);
    }

    private IEnumerator ShowBannerAdd()
    {
        while (Advertisement.IsReady(bannerAdPlacementID) == false)
        {
            showBannerAdTimeOut -= bannerAdTryToShowInterval;

            if(showBannerAdTimeOut < 0f)
                yield break;

            yield return new WaitForSeconds(bannerAdTryToShowInterval);
        }

        Advertisement.Banner.Show(bannerAdPlacementID);
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAdPlacementID) && currentInterstitialAdCountDown <= 0f)
        {
            Advertisement.Show(interstitialAdPlacementID);
        }
    }

    public void ShowRewardAd()
    {
        if (Advertisement.IsReady(rewardedAdPlacementID) == true)
            Advertisement.Show(rewardedAdPlacementID);
    }

    private void ResetInterstitialAdCountDown()
    {
        if (interstitialAdCoroutine != null)
            StopCoroutine(interstitialAdCoroutine);

        currentInterstitialAdCountDown = interstitialAdCountDown;

        interstitialAdCoroutine = startInterstitialAdCountDown();

        StartCoroutine(interstitialAdCoroutine);
    }

    private IEnumerator startInterstitialAdCountDown()
    {
        while (currentInterstitialAdCountDown > 0f)
        {
            currentInterstitialAdCountDown -= Time.deltaTime;
            yield return null;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == interstitialAdPlacementID && (showResult == ShowResult.Finished ||
                                                      showResult == ShowResult.Skipped))
        {
            ResetInterstitialAdCountDown();
        }

        if (placementId == rewardedAdPlacementID && showResult == ShowResult.Finished)
        {
            // Grant reward. Doubles the money earned
            ScoreManager.Instance.MutiplyCurrentScore(multiplier:2);
            UIManager.Instance.UpdateBonusMoneyEarned();
            UIManager.Instance.ToggleShowRewardAdButton(OnOff: false);
            ResetInterstitialAdCountDown();
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log(placementId + " ads are ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Add error message: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log(placementId + " ad started showing");
    }
}
