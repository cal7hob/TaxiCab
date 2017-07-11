using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CAScene { 
	public SceneTypes sceneType; 
    public CAInterstitialAndVideoSettings interstitialAndVideo;
    public CARewardedVideoSettings rewardedVideo;

    public CANativeAdSettings native;
    public CABannerSettings banner; 
	
}
