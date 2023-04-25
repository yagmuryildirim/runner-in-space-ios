using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using TMPro;

public class AdManager : Singleton<AdManager>
{
    public GameObject rewardPopup;
    public GameObject adPopup;
    private BannerView bannerView;
    private RewardedAd rewardedAd;

    private new void Awake()
    {
        base.Awake();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        LoadRewardedAd();
    }

    public void DisableRewardPopup()
    {
        rewardPopup.SetActive(false);
    }

    #region Rewarded Ad

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
#if UNITY_IPHONE
        //TEST
        string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
        //REAL
#else
   string _adUnitId = "unused";
#endif
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();


        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                rewardedAd = ad;
            });
        //RegisterReloadHandler(rewardedAd);
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
    {
        // Reload the ad so that we can show another as soon as possible.
        LoadRewardedAd();
    };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    public bool CheckRewardedAd()
    {
        return false;
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd == null) LoadRewardedAd();
        if (rewardedAd != null && rewardedAd.CanShowAd())
            rewardedAd.Show((Reward reward) =>
            {
                adPopup.SetActive(false);
                rewardPopup.SetActive(true);
                LoadRewardedAd();
            });
    }

    #endregion

    #region Banner Ad

    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
#if UNITY_IPHONE
        //TEST
        string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
        //REAL
#else
        string _adUnitId = "unused";
#endif

        // If we already have a banner, destroy the old one.
        if (bannerView != null)
        {
            DestroyBannerAd();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);
    }

    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        bannerView.LoadAd(adRequest);
    }

    /// <summary>
    /// Destroys the banner ad.
    /// </summary>
    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
        CreateBannerView();
    }
    #endregion
}
