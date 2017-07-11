using System;
using UnityEngine;
using System.Collections.Generic;
class CARevmobVideo : AdNetwork
{
    public Dictionary<String, String> appIds;
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        CARevmobAdManager.Instance.initialize(appIds, gameObjectName);
        CARevmobAdManager.Instance.createVideo();
    }

    public override bool showAd(int sceneID)
    {
        bool result = CARevmobAdManager.Instance.showVideo();
        CARevmobAdManager.Instance.createVideo();
        return result;
    }
    public override void requestAd()
    {
        CARevmobAdManager.Instance.createVideo();
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return CARevmobAdManager.Instance.IsVideoAvailable();
    }
}

