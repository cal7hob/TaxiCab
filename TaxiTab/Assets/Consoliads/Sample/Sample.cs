using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ChartboostSDK;

public class Sample : MonoBehaviour
{
    CAAdmob admob;
    // Use this for initialization 
    void Start()
    {
		admob = new CAAdmob();
      //  ConsoliAds.Instance.ShowInterstitial(0);
        //loading ad for consoliads scene
        //ConsoliAds.Instance.LoadAdForScene(10);
    }

    void OnEnable()
    {
        ConsoliAds.Instance.ShowBanner(0);
        SetupEvents();
    }
    void SetupEvents()
    {
        // Listen to all impression-related events
        ConsoliAds.onInterstitialAdShownEvent += onInterstitialAdShown;
        ConsoliAds.onVideoAdShownEvent += onVideoAdShown;
        ConsoliAds.onRewardedVideoAdShownEvent += onRewardedVideoAdShown;
        ConsoliAds.onPopupAdShownEvent += onPopupAdShown;
        ConsoliAds.onRewardedVideoAdCompletedEvent += onRewardedVideoCompleted;

    }
    public void onRewardedVideoCompleted()
    {
        Debug.Log("Sample: Event received : Rewarded Video Complete");
    }
    public void logScreen()
    {

        ConsoliAds.Instance.LogScreen("Sample_New");
    }
    public void logScreenGA()
    {
        ConsoliAds.Instance.googleAnalytics.LogScreen("Main_Menu");
    }
    public void startTest()
    {
        //ConsoliAds.Instance.startTesting();
    }
    // ------------------------------------------------------------------------------------------------------------------
    public void showNativAd()
    {
        admob.RequestNativeExpressAdView();
    }
    public void showAd(int sceneID)
    {
        ConsoliAds.Instance.ShowInterstitial(sceneID);
        /*
        if (ConsoliAds.Instance.IsAdAvailable(sceneID))
        {
            Debug.Log("Sample: Ad Available calling showAd");
            ConsoliAds.Instance.ShowAd(sceneID);
        }
        else
        {
            Debug.Log("Sample: Ad not Available");
            ConsoliAds.Instance.LoadAdForScene(sceneID);

            ConsoliAds.Instance.ShowAd(sceneID);
        }
        */
    }
    public void showRewardedVideo(int sceneID)
    {
		ConsoliAds.Instance.ShowRewardedVideo (sceneID);
    }
    public void loadAdmobNativeAd()
    {
        ConsoliAds.Instance.LoadNativeAd(0);
    }
    public void hideAdmobNativeAd()
    {
        ConsoliAds.Instance.HideNativeAd();
    }
    public void showAdmobNativeAd()
    {
        ConsoliAds.Instance.ShowNativeAd();
    }
    public void hideAllAds()
    {
        ConsoliAds.Instance.hideAllAds();
    }
    public void showAdmobBanner()
    {
        ConsoliAds.Instance.ShowBanner(1);
    }
    public void hideAdmobBanner()
    {
        ConsoliAds.Instance.HideBanner();
    }

    public void openFirebaseScene()
    {
        Application.LoadLevel("firebase_sample");
    }
    void onInterstitialAdShown()
    {
        Debug.Log("Sample: onInterstitialAdShown called");
    }
    void onVideoAdShown()
    {
        Debug.Log("Sample: onVideoAdShown called");
    }
    void onRewardedVideoAdShown()
    {
        Debug.Log("Sample: onRewardedVideoAdShown called");
    }
    void onPopupAdShown()
    {
    }
}
