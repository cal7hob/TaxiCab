using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


public abstract class RevMob {
	protected static readonly string REVMOB_VERSION = "9.2.1";
	protected string gameObjectName;


	public class ADTYPE {
		public static string BANNER = "Banner";
		public static string FULLSCREEN = "Fullscreen";
		public static string VIDEO = "Video";
		public static string LINK = "Link";
		public static string REWARDED_VIDEO = "RewardedVideo";
		public static string POP_UP = "PopUp";
	}
	
	public abstract void PrintEnvironmentInformation();
	public abstract void SetTimeoutInSeconds(int timeout);
	public abstract bool IsDevice();

	public abstract void SetUserAgeRangeMin(int minAge);
	public abstract void SetUserAgeRangeMax(int maxAge);

	public abstract void ShowFullscreen(string placementId);
	public abstract RevMobFullscreen CreateFullscreen(string placementId);
	public abstract RevMobFullscreen CreateVideo(string placementId);
	public abstract RevMobFullscreen CreateRewardedVideo(string placementId);
	public abstract RevMobLink OpenButton(string placementId);
	public abstract RevMobLink CreateButton(string placementId);
#if UNITY_ANDROID
	public abstract RevMobBanner CreateBanner(int leftMargin, int topMargin, int w, int h);
	public abstract void ShowBanner();
#elif UNITY_IPHONE
	public abstract RevMobBanner CreateBanner(float x, float y, float width, float height, ScreenOrientation[] orientations);
	public abstract RevMobPopup ShowPopup(string placementId);
	public abstract RevMobPopup CreatePopup(string placementId);
	public abstract void ShowBanner(ScreenOrientation[] orientations);
#endif
	public abstract void HideBanner();
	public abstract void ReleaseBanner();
	public abstract RevMobLink OpenLink(string placementId);
	public abstract RevMobLink CreateLink(string placementId);


	public static RevMob Start(Dictionary<string, string> appIds) {
		return Start(appIds, null);
	}

	public static RevMob Start(Dictionary<string, string> appIds, string gameObjectName) {
		Debug.Log("Creating RevMob Session");
#if UNITY_EDITOR
		Debug.Log("It Can't run in Unity Editor. Only in iOS or Android devices.");
		return null;
#elif UNITY_ANDROID
		RevMob session = new RevMobAndroid(appIds["Android"], gameObjectName);
		return session;
#elif UNITY_IPHONE
		RevMob session = new RevMobIos(appIds["IOS"], gameObjectName);
		return session;
#else
		return null;
#endif
	}

	public void ShowFullscreen() {
		this.ShowFullscreen(null);
	}

	public RevMobFullscreen CreateFullscreen() {
		return this.CreateFullscreen(null);
	}

	public RevMobFullscreen CreateVideo() {
		return this.CreateVideo(null);
	}

	public RevMobFullscreen CreateRewardedVideo() {
		return this.CreateRewardedVideo(null);
	}

	public RevMobLink OpenButton() {
		return this.OpenButton(null);
	}

	public RevMobLink CreateButton() {
		return this.CreateButton(null);
	}

#if UNITY_ANDROID
	public RevMobBanner CreateBanner() {
		return this.CreateBanner(0, 0, 0, 0);
	}

#elif UNITY_IPHONE
	public RevMobBanner CreateBanner() {
		return this.CreateBanner(0, 0, 0, 0, null);
	}


    public RevMobBanner CreateBanner(ScreenOrientation[] orientations) {
	    return this.CreateBanner(0, 0, 0, 0, orientations);
	}

	public RevMobPopup ShowPopup() {
		return this.ShowPopup(null);
	}

	public RevMobPopup CreatePopup() {
		return this.CreatePopup(null);
	}
#endif

	public RevMobLink OpenLink() {
		return this.OpenLink(null);
	}

	public RevMobLink CreateLink() {
		return this.CreateLink(null);
	}

}

