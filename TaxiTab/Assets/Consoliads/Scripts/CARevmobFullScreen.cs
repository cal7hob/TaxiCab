using System;
using UnityEngine;
using System.Collections.Generic;
    class CARevmobFullScreen : AdNetwork
    {
    public Dictionary<String, String> appIds;
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        CARevmobAdManager.Instance.initialize(appIds,gameObjectName);
        CARevmobAdManager.Instance.createFullScreen();
    }

    public override bool showAd(int sceneID)
    {
        bool result = CARevmobAdManager.Instance.showFullScreen();
        CARevmobAdManager.Instance.createFullScreen();
        return result;
    }

    public override void requestAd()
    {
        CARevmobAdManager.Instance.createFullScreen();
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return CARevmobAdManager.Instance.IsFullScreenAvailable();
    }
}

