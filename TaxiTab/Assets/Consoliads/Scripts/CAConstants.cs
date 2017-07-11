using UnityEngine;
using System.Collections;

public static class CAConstants
{
    public const string ConsoliAdsBaseURL = "https://sdk.consoliads.com/admin"; 
    public const string ConsoliAdsSyncURL = ConsoliAdsBaseURL + "/analytics/syncUserDevice";
    public const string sendNetworkStatsURL = ConsoliAdsBaseURL + "/analytics/saveNetworkStats";
    public const string ConsoliAdsConfigURL = ConsoliAdsBaseURL + "/json/syncApp";
    public const string REQUEEST_LOADED = "loaded";
    public const string REQUEEST_FAILED = "failed";

    public const string ConsoliAdsVersion = "1.2.1";
    public static string networkErrorMsg = "Sorry! There was a Network Error";

}


public enum SceneTypes
{

    MainMenu = 1,
    SelectionScene = 2,
    FinalScene = 3,
    OnSuccess = 4,
    OnFailure = 5,
    OnPause = 6,
    StoreScene = 7,
    Gameplay = 8,
    MidScene1 = 9,
    MidScene2 = 10,
    MidScene3 = 11,
    AppExit = 12,
    LoadingScene1 = 13,
    LoadingScene2 = 14,
    RewardedVideo = 15
};
public enum AdNetworkType
{
    EMPTY = -1,
    ADMOBINTERSTITIAL = 0,
    LEADBOLTINTERSTITIAL = 2,
    LEADBOLTREWARDEDVIDEO = 4,
    CHARTBOOST = 5,
    CHARTBOOSTMOREAPPS = 6,
    HEYZAPINTERSTITIAL = 7,
    REVMOBFULLSCREEN = 8,
    ADCOLONY = 9,
    REVMOBVIDEO = 11,
    CONSOLIADS = 19,
    UNITYADS = 10,
    SUPERSONICINTERSTITIAL = 12,
    SUPERSONICOFFERWALL = 14,
    SUPERSONICREWARDEDVIDEO = 15,
    APPLOVININTERSTITIAL = 18,
    APPLOVINREWARDEDVIDEO = 20,
    ADMOBREWARDEDVIDEO = 21,
    HEYZAPVIDEO = 22,
    REVMOBREWARDEDVIDEO = 24,
    CHARTBOOSTREWARDEDVIDEO = 25,
    UNITYADSREWARDEDVIDEO = 26
}; public enum AdNetworkTypeInterstitialAndVideo
{
    EMPTY = -1,
    ADMOBINTERSTITIAL = 0,
    LEADBOLTINTERSTITIAL = 2,
    CHARTBOOST = 5,
    CHARTBOOSTMOREAPPS = 6,
    HEYZAPINTERSTITIAL = 7,
    REVMOBFULLSCREEN = 8,
    ADCOLONY = 9,
    REVMOBVIDEO = 11,
    CONSOLIADS = 19,
    UNITYADS = 10,
    SUPERSONICINTERSTITIAL = 12,
    SUPERSONICOFFERWALL = 14,
    APPLOVININTERSTITIAL = 18,
    HEYZAPVIDEO = 22,
};
public enum AdNetworkTypeRewardedVideo
{
    EMPTY = -1,
    LEADBOLTREWARDEDVIDEO = 4,
    SUPERSONICREWARDEDVIDEO = 15,
    APPLOVINREWARDEDVIDEO = 20,
    ADMOBREWARDEDVIDEO = 21,
    REVMOBREWARDEDVIDEO = 24,
    CHARTBOOSTREWARDEDVIDEO = 25,
    UNITYADSREWARDEDVIDEO = 26
};


public enum NativeAds
{
    ADMOBNATIVEAD = 27
}
public enum AdNetworkTypeBanner
{
    ADMOBBANNER = 28
}
public enum Platform
{
    Google = 41,
    Apple = 42,
    Amazon = 43
}
public enum AdmobBannerSize
{
    Banner = 1,
    MediumRectangle = 2,
    IABBanner = 3,
    Leaderboard = 4,
    SmartBanner = 5
}
public enum Analytics
{
    NoAnalytics = 0,
    Google = 16,
    Flurry = 17

}
public enum AdNetworkQueueType
{
    RoundRobin = 1,
    Priority = 2
}

public enum AdValueType
{
    AdmobBannerAdUnitID = 1,
    AdmobInterstitialAdUnitID = 2,
    LeadboltAppKey = 3,
    RevmobMediaID = 4,
    HeyzapID = 5,
    ChartboostAppID = 6,
    ChartboostAppSignature = 7,
    AdColonyAppID = 8,
    AdColonyZoneID = 9,
    UnityAdsID = 10,
    SupersonicAppKey = 11,
    GoogleAnalyticsTrackingCode = 12,
    FlurryAnalyticsAppKey = 13,
    AppLovinID = 14,
    OguryInterstitialAPIKey = 15,
    ConsoliadsAppKey = 16,
    AdmobRewardedVideoAdUnitID = 17,
    AdmobAppID = 18,
    AdmobNativeAdID = 19
}


public enum CAAnalytics
{
    FireBaseAnalytics = 1,
    GoogleAnalytics = 2,
    FlurryAnalytics = 3
}
