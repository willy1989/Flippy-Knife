using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private bool testMode = true;

    private const string gameId = "4542369";

    private const string interstitialAdPlacementID = "Interstitial_Android";
    private const string bannerAdPlacementID = "Banner_Android";
    private const string rewardedAdPlacementID = "Rewarded_Android";

    private const int bannerAdCountDown = 2;
    private const float interstitialAdCountDown = 20f;
    private float currentInterstitialAdCountDown = interstitialAdCountDown;

    private float showBannerAdTimeOut = 120f;

    private bool AwardAdsShowedThisRun = false;


    private void Awake()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);

        
        Advertisement.Banner.Load(bannerAdPlacementID);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerAdd());

        //Advertisement.Load(rewardedAdPlacementID);
        Advertisement.Load(interstitialAdPlacementID);
        StartCoroutine(ReStartInterstitialAdCountDown());
    }

    private IEnumerator ShowBannerAdd()
    {
        while (Advertisement.IsReady(bannerAdPlacementID) == false)
        {
            showBannerAdTimeOut -= bannerAdCountDown;

            if(showBannerAdTimeOut < 0f)
                yield break;

            yield return new WaitForSeconds(bannerAdCountDown);
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

    private IEnumerator ReStartInterstitialAdCountDown()
    {
        currentInterstitialAdCountDown = interstitialAdCountDown;

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
            StartCoroutine(ReStartInterstitialAdCountDown());
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
