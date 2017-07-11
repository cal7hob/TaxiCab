using System;
using UnityEngine;
using System.Collections.Generic;
class CARevmobRewardedVideo : AdNetwork
{
    public Dictionary<String, String> appIds;
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        CARevmobAdManager.Instance.initialize(appIds, gameObjectName);
        CARevmobAdManager.Instance.createRewardedVideo();
    }

    public override bool showAd(int sceneID)
    {
        bool result = CARevmobAdManager.Instance.showRewardedVideo();
        CARevmobAdManager.Instance.createRewardedVideo();
        return result;
    }
    public override void requestAd()
    {
        CARevmobAdManager.Instance.createRewardedVideo();
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return CARevmobAdManager.Instance.IsRewardedVideoAvailable();
    }
}

