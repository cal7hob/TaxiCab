using UnityEngine;
using System.Collections;

class CAHeyzapInterstitial : AdNetwork
{

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        try
        {
            CAHeyzapAdManager.initialize(appKey);
            CAHeyzapAdManager.fetchInterstitial();
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Heyzap Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }

    public override bool showAd(int sceneID)
    {
        bool showResult = false;
        showResult =  CAHeyzapAdManager.showInterstitial();
        CAHeyzapAdManager.fetchInterstitial();
        return showResult;
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return CAHeyzapAdManager.IsInterstitialAvailable();
    }
    public override void requestAd()
    {
        CAHeyzapAdManager.fetchInterstitial();
    }
}
