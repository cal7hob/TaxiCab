using UnityEngine;
using System.Collections;

class CAApplovinRewardedVideo : AdNetwork  {
	
	public override void initialize(string gameObjectName, string uniqueDeviceID){
        AppLovin.SetSdkKey(appKey);
        AppLovin.InitializeSdk();
        AppLovin.SetUnityAdListener(gameObjectName);
        AppLovin.LoadRewardedInterstitial();


    }

	public override bool showAd(int sceneID){
        if (AppLovin.IsIncentInterstitialReady())
        {
            AppLovin.ShowRewardedInterstitial();
            return true;
        }
        else {
            Debug.Log("APPLOVIN: NO REWARDED VIDEO");
            // No rewarded ad is available.  Perform failover logic...
        }
        AppLovin.LoadRewardedInterstitial();
        return false;
    }
    public override void requestAd()
    {
        AppLovin.LoadRewardedInterstitial();
    }
    public override bool IsAdAvailable(int sceneID)
    {
        return AppLovin.IsIncentInterstitialReady();
    }
}
