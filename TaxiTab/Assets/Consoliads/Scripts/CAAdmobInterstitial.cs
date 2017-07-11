using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CAAdmobInterstitial : AdNetwork
{
    private CAAdmob admob = new CAAdmob(); 
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    { 
        try { 
            admob.RequestInterstitial(appKey); //requesting ad to show for the first time
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Admob Exception: " + ex.Message + " " + ex.StackTrace);
        }
        

    }
    public override bool showAd(int sceneID)
    {
        
        bool result = admob.ShowInterstitial();
        admob.RequestInterstitial(appKey); //request a new ad for next time
        return result; 
    }
    public override void requestAd()
    {
        admob.RequestInterstitial(appKey); //requesting ad to show for the first time
    }

    public override bool IsAdAvailable(int sceneID)
    {
        return admob.IsInterstitialLoaded();
    }
}
