using UnityEngine;
using System.Collections;

class CAInterstitial : AdNetwork  {
	
	public override void initialize(string gameObjectName, string uniqueDeviceID){
		if (!CAWrapper.initialized) {
            CAWrapper.init(appKey, gameObjectName, uniqueDeviceID); 
		}
	}

	public override bool showAd(int sceneID){
		return CAWrapper.showInterstitial (sceneID);
	}

    public override bool IsAdAvailable(int sceneID)
    {
        return CAWrapper.hasInterstitialForScene(sceneID);
    }
    public override void LoadAdForScene(int sceneID)
    {
        CAWrapper.loadInterstitialForScene(sceneID);
    }
	public void sendStatsOnPause(string deviceID)
	{
		CAWrapper.sendStatsOnPause(deviceID);
	}
}
