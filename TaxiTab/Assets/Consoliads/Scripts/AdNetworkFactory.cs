using System;
using UnityEngine;
using System.Collections.Generic;
public class AdNetworkFactory
{
    public static string amRateUsURL = "http://www.amazon.com/gp/mas/dl/android?p=";
    public static string gpRateUsURL = "https://play.google.com/store/apps/details?id=" ;

    public string moreFunURL;
    public string rateUsURL;
    public string admobBannerID { get; private set; }
    private string admobInterstitialID;
    private string admobRewardedVideoID;
    public string admobNativeAdID { get; private set; }
    private string heyzapID;
    private string chartboostID;
    private string chartboostSignature;
    private string adColonyAppID;
    private string adColonyZoneID;
    private string unityAdsID;
    private string leadboltAppKey;
    private string supersonicAppKey;
    private string appLovinID;
    private string consoliadsAppKey;
    private Dictionary<String, String> revmobIds;
    public AdNetwork getAdNetworkInstance(AdNetworkType type)
    {
        AdNetwork adNetwork;
        switch (type)
        {
            case AdNetworkType.CONSOLIADS:
                adNetwork = new CAInterstitial();
                adNetwork.appKey = consoliadsAppKey;
                adNetwork.type = type;
                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = false;
                return adNetwork;

            case AdNetworkType.CHARTBOOST:
                CAChartBoost chartboost = new CAChartBoost();
                chartboost.appKey = chartboostID;
                chartboost.type = type;
                chartboost.appSignature = chartboostSignature;

                chartboost.hasClickCallback = true;
                chartboost.hasRequestCallback = true;
                return (AdNetwork)chartboost;
            case AdNetworkType.CHARTBOOSTMOREAPPS:
                CAChartBoostMoreApps chartboostMoreApps = new CAChartBoostMoreApps();
                chartboostMoreApps.appKey = chartboostID;
                chartboostMoreApps.type = type;
                chartboostMoreApps.appSignature = chartboostSignature;

                chartboostMoreApps.hasClickCallback = true;
                chartboostMoreApps.hasRequestCallback = true;
                return (AdNetwork)chartboostMoreApps;
            case AdNetworkType.CHARTBOOSTREWARDEDVIDEO:
                CAChartBoostRewardedVideo chartboostRewardedVideo = new CAChartBoostRewardedVideo();
                chartboostRewardedVideo.appKey = chartboostID;
                chartboostRewardedVideo.type = type;
                chartboostRewardedVideo.isRewardedAd = true;
                chartboostRewardedVideo.appSignature = chartboostSignature;

                chartboostRewardedVideo.hasClickCallback = true;
                chartboostRewardedVideo.hasRequestCallback = true;
                return (AdNetwork)chartboostRewardedVideo;
            case AdNetworkType.ADMOBINTERSTITIAL:
                adNetwork = new CAAdmobInterstitial();
                adNetwork.appKey = admobInterstitialID;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.ADMOBREWARDEDVIDEO:
                adNetwork = new CAAdmobRewardedVideo();
                adNetwork.appKey = admobRewardedVideoID;
                adNetwork.isRewardedAd = true;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork; 
            case AdNetworkType.LEADBOLTINTERSTITIAL:
                adNetwork = new CALeadboltInterstitial();
                adNetwork.appKey = leadboltAppKey;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.LEADBOLTREWARDEDVIDEO:
                adNetwork = new CALeadboltRewardedVideo();
                adNetwork.appKey = leadboltAppKey;
                adNetwork.isRewardedAd = true;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.HEYZAPINTERSTITIAL:
                adNetwork = new CAHeyzapInterstitial();
                adNetwork.appKey = heyzapID;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.HEYZAPVIDEO:
                adNetwork = new CAHeyzapVideo();
                adNetwork.appKey = heyzapID;
                adNetwork.type = type;

                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.REVMOBFULLSCREEN:
                CARevmobFullScreen revmobFullScreen = new CARevmobFullScreen();
                revmobFullScreen.appIds = revmobIds;
                revmobFullScreen.type = type;

                revmobFullScreen.hasClickCallback = true;
                revmobFullScreen.hasRequestCallback = true;
                return (AdNetwork)revmobFullScreen;
            case AdNetworkType.REVMOBVIDEO:
                CARevmobVideo revmobVideo = new CARevmobVideo();
                revmobVideo.appIds = revmobIds;
                revmobVideo.type = type;
                revmobVideo.hasClickCallback = true;
                revmobVideo.hasRequestCallback = true;
                return (AdNetwork)revmobVideo;
            case AdNetworkType.REVMOBREWARDEDVIDEO:
                CARevmobRewardedVideo revmobRewardedVideo = new CARevmobRewardedVideo();
                revmobRewardedVideo.appIds = revmobIds;
                revmobRewardedVideo.type = type;
                revmobRewardedVideo.isRewardedAd = true;
                revmobRewardedVideo.hasClickCallback = true;
                revmobRewardedVideo.hasRequestCallback = true;
                return (AdNetwork)revmobRewardedVideo;
            case AdNetworkType.UNITYADS:
                adNetwork = new CAUnityAds();
                adNetwork.appKey = unityAdsID;
                adNetwork.type = type;
                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = false;
                return adNetwork;
            case AdNetworkType.ADCOLONY:
                CAAdColony adcolony = new CAAdColony();
                adcolony.appKey = adColonyAppID;
                adcolony.adColonyZoneID = adColonyZoneID;
                adcolony.type = type;
                adcolony.hasClickCallback = false;
                adcolony.hasRequestCallback = true;
                return (AdNetwork)adcolony;
            case AdNetworkType.SUPERSONICINTERSTITIAL:
                adNetwork = new CASupersonicInterstitial();
                adNetwork.appKey = supersonicAppKey;
                adNetwork.type = type;
                adNetwork.hasClickCallback = true;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.SUPERSONICOFFERWALL:
                adNetwork = new CASupersonicOfferwall();
                adNetwork.appKey = supersonicAppKey;
                adNetwork.type = type;
                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = false;
                return adNetwork;
            case AdNetworkType.SUPERSONICREWARDEDVIDEO:
                adNetwork = new CASupersonicRewardedVideo();
                adNetwork.appKey = supersonicAppKey;
                adNetwork.isRewardedAd = true;
                adNetwork.type = type;

                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.APPLOVININTERSTITIAL:
                adNetwork = new CAApplovinInterstitial();
                adNetwork.appKey = appLovinID;
                adNetwork.type = type;
                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.APPLOVINREWARDEDVIDEO:
                adNetwork = new CAApplovinRewardedVideo();
                adNetwork.appKey = appLovinID;
                adNetwork.isRewardedAd = true;
                adNetwork.type = type;

                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = true;
                return adNetwork;
            case AdNetworkType.UNITYADSREWARDEDVIDEO:
                adNetwork = new CAUnityAdsRewardedVideo();
                adNetwork.appKey = unityAdsID;
                adNetwork.isRewardedAd = true;
                adNetwork.type = type;

                adNetwork.hasClickCallback = false;
                adNetwork.hasRequestCallback = false;
                return adNetwork;

        }
        return null;
    }
    public void setupAdNetworkIDs()
    {
#if UNITY_ANDROID
        if (ConsoliAds.Instance.platform == Platform.Amazon)
        {
            rateUsURL = ConsoliAds.Instance.adIDList.gpRateUsURL;
            moreFunURL = rateUsURL + "&showAll=1";
            admobBannerID = ConsoliAds.Instance.adIDList.amAdmobBannerAdUnitID;
            admobInterstitialID = ConsoliAds.Instance.adIDList.amAdmobInterstitialAdUnitID;
            admobRewardedVideoID = ConsoliAds.Instance.adIDList.amAdmobRewardedVideoAdUnitID;
            admobNativeAdID = ConsoliAds.Instance.adIDList.amAdmobNativeAdUnitID;
            leadboltAppKey = ConsoliAds.Instance.adIDList.amLeadboltAppKey;
            heyzapID = ConsoliAds.Instance.adIDList.amHeyzapID;
            chartboostID = ConsoliAds.Instance.adIDList.amChartboostAppID;
            chartboostSignature = ConsoliAds.Instance.adIDList.amChartboostAppSignature;
            adColonyAppID = ConsoliAds.Instance.adIDList.amAdcolonyAppID;
            adColonyZoneID = ConsoliAds.Instance.adIDList.amAdcolonyZoneID;
            revmobIds = new Dictionary<String, String>() {
                           { "Android", ConsoliAds.Instance.adIDList.amRevmobMediaID},
                        };
            unityAdsID = ConsoliAds.Instance.adIDList.amUnityadsAppID;
            supersonicAppKey = ConsoliAds.Instance.adIDList.amSupersonicAppKey;
            appLovinID = ConsoliAds.Instance.adIDList.amApplovinID;
            consoliadsAppKey = ConsoliAds.Instance.adIDList.amConsoliadsAppKey;
        }
        else
        {
            moreFunURL = ConsoliAds.Instance.adIDList.gpMoreAppsURL;
            //rateUsURL = gpRateUsURL + ConsoliAds.Instance.bundleIdentifier;
            rateUsURL = ConsoliAds.Instance.adIDList.gpRateUsURL;
            admobBannerID = ConsoliAds.Instance.adIDList.gpAdmobBannerAdUnitID;
            admobInterstitialID = ConsoliAds.Instance.adIDList.gpAdmobInterstitialAdUnitID;
            admobRewardedVideoID = ConsoliAds.Instance.adIDList.gpAdmobRewardedVideoAdUnitID;
            admobNativeAdID = ConsoliAds.Instance.adIDList.gpAdmobNativeAdUnitID;
            leadboltAppKey = ConsoliAds.Instance.adIDList.gpLeadboltAppKey;
            heyzapID = ConsoliAds.Instance.adIDList.gpHeyzapID;
            chartboostID = ConsoliAds.Instance.adIDList.gpChartboostAppID;
            chartboostSignature = ConsoliAds.Instance.adIDList.gpChartboostAppSignature;
            adColonyAppID = ConsoliAds.Instance.adIDList.gpAdcolonyAppID;
            adColonyZoneID = ConsoliAds.Instance.adIDList.gpAdcolonyZoneID;
            revmobIds = new Dictionary<String, String>() {
                           { "Android", ConsoliAds.Instance.adIDList.gpRevmobMediaID},
                        };
            unityAdsID = ConsoliAds.Instance.adIDList.gpUnityadsAppID;
            supersonicAppKey = ConsoliAds.Instance.adIDList.gpSupersonicAppKey;
            appLovinID = ConsoliAds.Instance.adIDList.gpApplovinID;
            consoliadsAppKey = ConsoliAds.Instance.adIDList.gpConsoliadsAppKey;

        }
#elif UNITY_IPHONE
        moreFunURL = ConsoliAds.Instance.adIDList.asMoreAppsURL;
        rateUsURL = ConsoliAds.Instance.adIDList.asRateUsURL;
        admobBannerID = ConsoliAds.Instance.adIDList.asAdmobBannerAdUnitID;
        admobInterstitialID = ConsoliAds.Instance.adIDList.asAdmobInterstitialAdUnitID;
        admobRewardedVideoID = ConsoliAds.Instance.adIDList.asAdmobRewardedVideoAdUnitID;
        admobNativeAdID = ConsoliAds.Instance.adIDList.asAdmobNativeAdUnitID;
        leadboltAppKey = ConsoliAds.Instance.adIDList.asLeadboltAppKey;
        heyzapID = ConsoliAds.Instance.adIDList.asHeyzapID;
        chartboostID = ConsoliAds.Instance.adIDList.asChartboostAppID;
        chartboostSignature = ConsoliAds.Instance.adIDList.asChartboostAppSignature;
        adColonyAppID = ConsoliAds.Instance.adIDList.asAdcolonyAppID;
        adColonyZoneID = ConsoliAds.Instance.adIDList.asAdcolonyZoneID;
        revmobIds = new Dictionary<String, String>() {
                               { "IOS", ConsoliAds.Instance.adIDList.asRevmobMediaID},
                            };
        unityAdsID = ConsoliAds.Instance.adIDList.asUnityadsAppID;
        supersonicAppKey = ConsoliAds.Instance.adIDList.asSupersonicAppKey;
        appLovinID = ConsoliAds.Instance.adIDList.asApplovinID;
        consoliadsAppKey = ConsoliAds.Instance.adIDList.asConsoliadsAppKey;
#endif
    }


}
