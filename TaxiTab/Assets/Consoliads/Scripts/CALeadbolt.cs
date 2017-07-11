#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using AppTrackerUnitySDK;
public class CALeadbolt : MonoBehaviour
{
    private float xCentre = Screen.width / 2;
    private float yCentre = Screen.height / 2;
    private bool hasInterstitial = false;
    private bool hasRewardedVideo = false;
    public bool initialized= false;
    private int count = -1;

    
    public void initialize(string appKey)
    {
        initialized = true;
        Screen.orientation = ScreenOrientation.AutoRotation; //to allow orientation other than portrait
#if UNITY_ANDROID
        AppTrackerAndroid.startSession(appKey);
        AppTrackerAndroid.onModuleClosedEvent += onModuleClosedEvent;
        AppTrackerAndroid.onModuleFailedEvent += onModuleFailedEvent;
        AppTrackerAndroid.onModuleLoadedEvent += onModuleLoadedEvent;
        AppTrackerAndroid.onModuleCachedEvent += onModuleCachedEvent;
        AppTrackerAndroid.onModuleClickedEvent += onModuleClickedEvent;
        AppTrackerAndroid.onMediaFinishedEvent += onMediaFinishedEvent; 
#endif
#if UNITY_IPHONE
		    AppTrackerIOS.startSession(appKey, false);
		    AppTrackerIOS.onModuleClosedEvent += onModuleClosedEvent;
		    AppTrackerIOS.onModuleFailedEvent += onModuleFailedEvent;
		    AppTrackerIOS.onModuleLoadedEvent += onModuleLoadedEvent;
		    AppTrackerIOS.onModuleCachedEvent += onModuleCachedEvent;
            AppTrackerIOS.onModuleClickedEvent += onModuleClickedEvent;
            AppTrackerIOS.onMediaFinishedEvent += onMediaFinishedEvent; 
#endif
    }

    public void cacheInterstitial()
    {
#if UNITY_ANDROID
        AppTrackerAndroid.loadModuleToCache("inapp");
#elif UNITY_IPHONE
                AppTrackerIOS.loadModuleToCache("inapp");
#endif
    }
    public bool showInterstitial()
    {
#if UNITY_ANDROID
        if (AppTrackerAndroid.isAdReady("inapp"))
        {
            AppTrackerAndroid.loadModule("inapp");
            return true;
        }
        else {
            hasInterstitial = false;
        }
#elif UNITY_IPHONE
            if(AppTrackerIOS.isAdReady("inapp")) {
				AppTrackerIOS.loadModule("inapp");
                //ConsoliAds.Instance.onInterstitialAdShown(AdNetworkType.LEADBOLTINTERSTITIAL);
                return true;
			} else {
				hasInterstitial = false;
			}
#endif
        return false;
    }
    public bool IsInterstitialAvailable()
    {
#if UNITY_ANDROID
        if (AppTrackerAndroid.isAdReady("inapp"))
        {
            return true;
        }
#elif UNITY_IPHONE
        if (AppTrackerIOS.isAdReady("inapp"))
        {
            return true;
        }
#endif
        return false;
    }
    public void cacheRewardedVideo()
    {
#if UNITY_ANDROID
        AppTrackerAndroid.loadModuleToCache("video");
#elif UNITY_IPHONE
            AppTrackerIOS.loadModuleToCache("video");
#endif
    }
    public bool showRewardedVideo()
    {
#if UNITY_ANDROID
        if (AppTrackerAndroid.isAdReady("video"))
        {
            AppTrackerAndroid.loadModule("video");
            return true;
        }
        else {
            hasRewardedVideo = false;
        }
#elif UNITY_IPHONE
            if (AppTrackerIOS.isAdReady("video"))
            {
                AppTrackerIOS.loadModule("video");
                //ConsoliAds.Instance.onRewardedVideoAdShown(AdNetworkType.LEADBOLTREWARDEDVIDEO);
                return true;
            }
            else {
                hasRewardedVideo = false;
            }
#endif
        return false;
    }

    public bool IsRewardedVideoAvailable()
    {
#if UNITY_ANDROID
        if (AppTrackerAndroid.isAdReady("video"))
        {
            return true;
        }
#elif UNITY_IPHONE
        if (AppTrackerIOS.isAdReady("video"))
        {
            return true;
        }
        
#endif
        return false;
    }
     void onModuleClosedEvent(string placement)
    {
        if (placement.Equals("inapp"))
        {
            ConsoliAds.Instance.onAdClosed(AdNetworkType.LEADBOLTINTERSTITIAL);
        }
        else if (placement.Equals("video"))
        {
            ConsoliAds.Instance.onAdClosed(AdNetworkType.LEADBOLTREWARDEDVIDEO);
        }
    }
     void onModuleFailedEvent(string placement, string error, bool cached)
    {
        if (cached)
        {
            // ad failed to cache
            if (placement.Equals("inapp"))
            {
                if (!error.Contains("network error"))
                {
                    ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.LEADBOLTINTERSTITIAL);

                }

            }
            else if (placement.Equals("video"))
            {
                if (!error.Contains("network error"))
                {
                    ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.LEADBOLTREWARDEDVIDEO);
                }
            }
        }
        else
        {
            //ad failed to load
        }
    }
     void onModuleLoadedEvent(string placement)
    {
        if (placement.Equals("inapp"))
        {
            ConsoliAds.Instance.onInterstitialAdShown(AdNetworkType.LEADBOLTINTERSTITIAL);

        }
        else if (placement.Equals("video"))
        {
            ConsoliAds.Instance.onRewardedVideoAdShown(AdNetworkType.LEADBOLTREWARDEDVIDEO);

        }
    }
     void onModuleCachedEvent(string placement)
    {
        if (placement.Equals("inapp"))
        {
            ConsoliAds.Instance.onAdRequested(AdNetworkType.LEADBOLTINTERSTITIAL);

        }
        else if (placement.Equals("video"))
        {
            ConsoliAds.Instance.onAdRequested(AdNetworkType.LEADBOLTREWARDEDVIDEO);

        }
    }
     void onModuleClickedEvent(string placement)
    {
        if (placement.Equals("inapp"))
        {
            ConsoliAds.Instance.onAdClick(AdNetworkType.LEADBOLTINTERSTITIAL);

        }
        else if (placement.Equals("video"))
        {
            ConsoliAds.Instance.onAdClick(AdNetworkType.LEADBOLTREWARDEDVIDEO);
        }
    }
     void onMediaFinishedEvent(bool viewCompleted)
    {
        if (viewCompleted)
        {
            // User finished watching the video successfully
            ConsoliAds.Instance.onRewardedVideoAdCompleted(AdNetworkType.LEADBOLTREWARDEDVIDEO);

        }
        else {
            // Video wasn't fully watched by the user.
        }
    }
}

