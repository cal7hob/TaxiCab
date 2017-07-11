using System;
using UnityEngine;
using System.Collections.Generic;
class CALeadboltRewardedVideo : AdNetwork
{
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        try
        {
            if (ConsoliAds.Instance.leadbolt.initialized == false)
            {
                ConsoliAds.Instance.leadbolt.initialize(appKey);
            }
            ConsoliAds.Instance.leadbolt.cacheRewardedVideo();
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Leadbolt Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }

    public override bool showAd(int sceneID)
    {
        bool result = ConsoliAds.Instance.leadbolt.showRewardedVideo();
        ConsoliAds.Instance.leadbolt.cacheRewardedVideo();
        return result;
    }
    public override void requestAd()
    {
        ConsoliAds.Instance.leadbolt.cacheRewardedVideo();
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return ConsoliAds.Instance.leadbolt.IsRewardedVideoAvailable();
    }
}

