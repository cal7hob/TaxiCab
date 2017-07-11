using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class CAChartBoostRewardedVideo : AdNetwork
{

    public string appSignature { get; set; }

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {

        //delegate event methods
        Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
        Chartboost.didClickRewardedVideo += didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
        Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
        Chartboost.didCloseRewardedVideo  += didCloseRewardedVideo; 
        Chartboost.didDismissRewardedVideo += didDismissRewardedVideo; 
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;

#if (UNITY_ANDROID || UNITY_IPHONE)
        if (!Chartboost.isInitialized())
        { 
            Chartboost.CreateWithAppId(appKey, appSignature);
            Chartboost.setAutoCacheAds(true);
        }
        Chartboost.cacheRewardedVideo(CBLocation.Default);

#endif
    }

    public override bool showAd(int sceneID)
    {
        //CBLocation location = getCBLocation((SceneTypes)sceneID);
        CBLocation location = CBLocation.Default;
        bool result = true;
#if (UNITY_ANDROID || UNITY_IPHONE)
        if (Chartboost.hasRewardedVideo(location))
        {
            Chartboost.showRewardedVideo(location);
        }
        else {
            result = false;
        }

        Chartboost.cacheRewardedVideo(location);
#endif
        return result;
    }

    public override void requestAd()
    {
        Chartboost.cacheRewardedVideo(CBLocation.Default);
    }


    public override bool IsAdAvailable(int sceneID)
    {
        //CBLocation location = getCBLocation((SceneTypes)sceneID);
        CBLocation location = CBLocation.Default;

        if (Chartboost.hasRewardedVideo(location))
        {
            return true;
        }
        return false;
    }

    void didCacheRewardedVideo(CBLocation location)
    {
        ConsoliAds.Instance.onAdRequested(type);
    }
    void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error)
    {

        if(error != CBImpressionError.InternetUnavailable)
        {
            ConsoliAds.Instance.onAdRequestFailed(type);
        } 
    }
    void didDisplayRewardedVideo(CBLocation location)
    {
        ConsoliAds.Instance.onRewardedVideoAdShown(type);
    }
    void didClickRewardedVideo(CBLocation location)
    {
        ConsoliAds.Instance.onAdClick(type);

    }
    void didCloseRewardedVideo(CBLocation location)
    {
        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheRewardedVideo(CBLocation.Default);
    }
    void didDismissRewardedVideo(CBLocation location)
    {
        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheRewardedVideo(CBLocation.Default);
    }
    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        ConsoliAds.Instance.onRewardedVideoAdCompleted(type);
    }
}
