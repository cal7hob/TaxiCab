using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class CAAdmob
{
    public BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    private NativeExpressAdView nativeExpressAdView;
    private bool interstitialShowed = false;
    private bool interstitialStopped = false;
    private int interstitialFailedCounter = 0;



    private static string outputMessage = "";

    public void initRewardedBasedVideo()
    {
        // Get singleton reward based video ad reference.
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }


    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    public void RequestBanner(string bannerAdUnitID, AdSize adSize, AdPosition adPosition)
    {
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(bannerAdUnitID, adSize, adPosition);
        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdLoaded += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    public void RequestInterstitial(string adUnitID)
    {
        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitID);
        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                //.AddTestDevice(AdRequest.TestDeviceSimulator)
                //.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
                //.AddKeyword("game")
                //.SetGender(Gender.Male)
                //.SetBirthday(new DateTime(1985, 1, 1))
                .TagForChildDirectedTreatment(ConsoliAds.Instance.ChildDirected) //yet to be tested
                .AddExtra("color_bg", "9B30FF")
                .Build();
    }

    public void RequestRewardBasedVideo(string rewardedVideoAdUnitID)
    {
        rewardBasedVideo.LoadAd(createAdRequest(), rewardedVideoAdUnitID);
    }

    public void RequestNativeExpressAdView()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1072772517";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 native express ad at the top of the screen.
        nativeExpressAdView = new NativeExpressAdView(adUnitId, new AdSize(320, 150), AdPosition.Top);
        // Load a banner ad.
        nativeExpressAdView.LoadAd(new AdRequest.Builder().Build());
    }

    public bool ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            return true;
        }
        else
        {
        }
        return false;
    }
    public bool IsInterstitialLoaded()
    {

        if (interstitial.IsLoaded())
        {
            return true;
        }
        return false;
    }

    public bool ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            return true;
        }
        else
        {
        }
        return false;
    }
    
    public bool IsRewardedVideoLoaded()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            return true;
        }
        return false;
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    { 
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onAdRequested(AdNetworkType.ADMOBINTERSTITIAL);


    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

        if (args.Message.Contains("Network Error") == false)
        {
            ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.ADMOBINTERSTITIAL);
        }
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onInterstitialAdShown(AdNetworkType.ADMOBINTERSTITIAL);
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onAdClosed(AdNetworkType.ADMOBINTERSTITIAL);

    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onAdClick(AdNetworkType.ADMOBINTERSTITIAL);
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onAdRequested(AdNetworkType.ADMOBREWARDEDVIDEO);
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (args.Message.Contains("Network Error") == false)
        {
            ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.ADMOBREWARDEDVIDEO);
        }
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onRewardedVideoAdShown(AdNetworkType.ADMOBREWARDEDVIDEO);
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onAdClosed(AdNetworkType.ADMOBREWARDEDVIDEO);

    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount; 
        ConsoliAds.Instance.onRewardedVideoAdCompleted(AdNetworkType.ADMOBREWARDEDVIDEO);

    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        ConsoliAds.Instance.onRewardedVideoAdClicked(AdNetworkType.ADMOBREWARDEDVIDEO);

    }

    #endregion
}
