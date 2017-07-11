using System;
using UnityEngine;
using System.Collections.Generic;
class CALeadboltInterstitial : AdNetwork
{
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        try
        {
            if (ConsoliAds.Instance.leadbolt.initialized == false)
            {
                ConsoliAds.Instance.leadbolt.initialize(appKey);
            }
            ConsoliAds.Instance.leadbolt.cacheInterstitial();
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Leadbolt Exception: " + ex.Message + " " + ex.StackTrace);
        }

    }

    public override bool showAd(int sceneID)
    {
        bool result = ConsoliAds.Instance.leadbolt.showInterstitial();
        ConsoliAds.Instance.leadbolt.cacheInterstitial();
        return result;
    }
    public override void requestAd()
    {
        ConsoliAds.Instance.leadbolt.cacheInterstitial();
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return ConsoliAds.Instance.leadbolt.IsInterstitialAvailable();
    }
}

