using UnityEngine;
using System;
using System.Runtime.InteropServices;

#if UNITY_ANDROID
public class RevMobAndroidBanner : RevMobBanner {
	private AndroidJavaObject javaObject;
	private bool customBanner;


	public RevMobAndroidBanner(AndroidJavaObject activity, AndroidJavaObject listener,int x, int y, int w, int h, AndroidJavaObject session)  {
		this.javaObject = session;
		int finalPosition;
		if(x != 0 || y != 0 || w != 0 || h != 0) {
			this.customBanner = true;
			finalPosition = 1;

			Debug.Log("BCRS createCustomBanner");
			this.javaObject.Call("createCustomBanner", activity, listener, finalPosition, x, y, w, h);
		}
		else {
			this.customBanner = false;
			Debug.Log("BCRS createBanner");
			this.javaObject.Call("createBanner", activity, listener);
		}
		
	}

	public override void Show() {
		if(this.customBanner == false) {

		Debug.Log("BCRS showBanner");
			this.javaObject.Call("showBanner");
		}
		else{
		Debug.Log("BCRS showCustomBanner");
			this.javaObject.Call("showCustomBanner");
			} 
    }

    public override void Hide() {
		Debug.Log("BCRS hideBanner");
		if(this.customBanner == false) this.javaObject.Call("hideBanner");
		else this.javaObject.Call("hideCustomBanner");
    }

	public override void Release() {
		Debug.Log("BCRS releaseBanner");
		if(this.customBanner == false) this.javaObject.Call("releaseBanner");
		else this.javaObject.Call("releaseCustomBanner");
	}
}
#endif