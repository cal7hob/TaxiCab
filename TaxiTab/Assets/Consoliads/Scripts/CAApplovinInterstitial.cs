using UnityEngine;
using System.Collections;

class CAApplovinInterstitial : AdNetwork  {
	
	public override void initialize(string gameObjectName, string uniqueDeviceID){
        AppLovin.SetSdkKey(appKey);
        AppLovin.InitializeSdk();
        AppLovin.SetUnityAdListener(gameObjectName);
        AppLovin.PreloadInterstitial();


    }

	public override bool showAd(int sceneID){
        if (AppLovin.HasPreloadedInterstitial())
        {
            // An ad is currently available, so show the interstitial.
            AppLovin.ShowInterstitial();
            return true;
        }
        AppLovin.PreloadInterstitial();
        return false;
    }
    public override void requestAd()
    {
        AppLovin.PreloadInterstitial();
    }
    public override bool IsAdAvailable(int sceneID)
    {
        return AppLovin.HasPreloadedInterstitial();
    }

}
