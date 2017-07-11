using UnityEngine;
using System.Collections;

class CAHeyzapVideo : AdNetwork
{

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        try
        {
            CAHeyzapAdManager.initialize(appKey);
            CAHeyzapAdManager.fetchRewardedVideo();
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Heyzap Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }

    public override bool showAd(int sceneID)
    {
        bool showResult = false;

        showResult = CAHeyzapAdManager.showRewardedVideo();
        CAHeyzapAdManager.fetchRewardedVideo();

        return showResult;

    }

    public override bool IsAdAvailable(int sceneID)
    {
         return CAHeyzapAdManager.IsRewardedVideoAvailable();
    }
    public override void requestAd()
    {
        CAHeyzapAdManager.fetchRewardedVideo();
    }
}
