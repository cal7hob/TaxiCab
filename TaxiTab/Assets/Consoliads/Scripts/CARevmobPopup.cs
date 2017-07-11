using System;
using UnityEngine;
using System.Collections.Generic;
class CARevmobPopup : AdNetwork
{
    public Dictionary<String, String> appIds;
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        Debug.Log("ADNETWORK: INITIALIZE REVMOBVIDEO");
        CARevmobAdManager.Instance.initialize(appIds, gameObjectName);
        CARevmobAdManager.Instance.createPopup();
    }

    public override bool showAd(int sceneID)
    {
        bool result = CARevmobAdManager.Instance.showPopup();
        CARevmobAdManager.Instance.createPopup();
        return result;
    }
    public override void requestAd()
    {
        CARevmobAdManager.Instance.createPopup();
    }
}

