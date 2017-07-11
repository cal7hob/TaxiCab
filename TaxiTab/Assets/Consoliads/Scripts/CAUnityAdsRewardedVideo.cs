using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

class CAUnityAdsRewardedVideo : AdNetwork
{

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        initUnityAds();
    }

    public override bool showAd(int sceneID)
    {
        initUnityAds();
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        if (Advertisement.isSupported && Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", options);
            return true;
        }
        else {
            return false;
        }
    }
    private void initUnityAds()
    {

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(appKey);
        }

    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                ConsoliAds.Instance.onRewardedVideoAdShown(type);
                ConsoliAds.Instance.onRewardedVideoAdCompleted(type);
                break;
            case ShowResult.Skipped:
                ConsoliAds.Instance.onRewardedVideoAdShown(type);
                break;
            case ShowResult.Failed:
                ConsoliAds.Instance.onAdShowFailed(type);
                break;
        }
    }

    public override bool IsAdAvailable(int sceneID)
    {
        if (Advertisement.isSupported && Advertisement.IsReady())
        {
            return true;
        }
        else {
            return false;
        }
    }
}
