using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if UNITY_IPHONE
public class RevMobIosBanner : RevMobBanner {
	private float x;
	private float y;
	private float width;
	private float height;
	private bool customBanner;

	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_showBanner();

	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_loadBanner(string placementId, ScreenOrientation[] orientations);

	[DllImport("__Internal")]
	private static extern void RevMobUnityiOSBinding_hideBanner();

	[DllImport("__Internal")]
	private static extern void RevMobUnityiOSBinding_releaseBannerAd();


	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_showCustomBanner();

	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_loadCustomBanner(string placementId, ScreenOrientation[] orientations, float x, float y, float width, float height);

	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_hideCustomBanner();


	[DllImport("__Internal")]
    private static extern void RevMobUnityiOSBinding_releaseCustomBannerAd();

	private ScreenOrientation[] orientationsArray;
	private string placementId;

	public RevMobIosBanner(ScreenOrientation[] orientations, float x, float y, float width, float height) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.orientationsArray = orientations;
		if (this.orientationsArray != null)
			configureOrientationsArray ();
		if(x == 0 && y == 0 && width == 0 && height == 0){
			this.customBanner = false;
			RevMobUnityiOSBinding_loadBanner(null,orientations);

		}else{
			this.customBanner = true;
			RevMobUnityiOSBinding_loadCustomBanner(null,orientations,x,y,width,height);
		}
	}

	private void configureOrientationsArray(){
		int upperbound = ((int)orientationsArray.GetUpperBound(0));
		ScreenOrientation[] newArray = new ScreenOrientation[4];
		for (int index = 0; index < 4; index++){
			if (index <= upperbound)
				newArray [index] = orientationsArray [index];
			else
				newArray [index] = 0;
		}
		this.orientationsArray = newArray;
	}

	public override void Show() {
		if(this.customBanner == true)
			RevMobUnityiOSBinding_showCustomBanner();
		else
			RevMobUnityiOSBinding_showBanner();
	}

	public override void Hide() {
		if(this.customBanner == true)
			RevMobUnityiOSBinding_hideCustomBanner();
		else
			RevMobUnityiOSBinding_hideBanner();

	}

	public override void Release() {
		this.Hide();
		if(this.customBanner == true)
			RevMobUnityiOSBinding_releaseCustomBannerAd();
		else
			RevMobUnityiOSBinding_releaseBannerAd();

	}

}
#endif
