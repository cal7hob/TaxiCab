using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class CAChartBoost : AdNetwork
{

    public string appSignature { get; set; }

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {

        //delegate event methods
        Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
        Chartboost.didDismissInterstitial += didDismissInterstitial;
        Chartboost.didCloseInterstitial += didCloseInterstitial;
        Chartboost.didClickInterstitial += didClickInterstitial;
        Chartboost.didCacheInterstitial += didCacheInterstitial;
        Chartboost.didDisplayInterstitial += didDisplayInterstitial;

#if (UNITY_ANDROID || UNITY_IPHONE)
        if (!Chartboost.isInitialized())
        {
            Chartboost.CreateWithAppId(appKey, appSignature);
            Chartboost.setAutoCacheAds(true);
        }
        Chartboost.cacheInterstitial(CBLocation.Default);

#endif
    }

    public override bool showAd(int sceneID)
    {
        //CBLocation location = getCBLocation((SceneTypes)sceneID);
        CBLocation location = CBLocation.Default;
        bool result = true;
#if (UNITY_ANDROID || UNITY_IPHONE)
        if (Chartboost.hasInterstitial(location))
        {
            Chartboost.showInterstitial(location);
        }
        else {
            result = false;
            Chartboost.cacheInterstitial(location);
        }
#endif
        return result;
    }
    public override void requestAd()
    {

        Chartboost.cacheInterstitial(CBLocation.Default);
    }

    public override bool IsAdAvailable(int sceneID)
    {
        //CBLocation location = getCBLocation((SceneTypes)sceneID);
        CBLocation location = CBLocation.Default;

        if (Chartboost.hasInterstitial(location))
        {
            return true;
        }
        return false;
    }

    // Called after an interstitial has been displayed on the screen.
    void didDisplayInterstitial(CBLocation location)
    {
		#if UNITY_IPHONE
        ConsoliAds.Instance.onInterstitialAdShown(type);
		#endif
    }

    // Called after an interstitial has been loaded from the Chartboost API
    // servers and cached locally.
    void didCacheInterstitial(CBLocation location)
    {

        ConsoliAds.Instance.onAdRequested(type);
    }

    // Called after an interstitial has attempted to load from the Chartboost API
    // servers but failed.
    void didFailToLoadInterstitial(CBLocation location, CBImpressionError error)
    {
        if (error != CBImpressionError.InternetUnavailable)
        {
            ConsoliAds.Instance.onAdRequestFailed(type);

        }
    }

    // Called after an interstitial has been dismissed.
    void didDismissInterstitial(CBLocation location)
    {
		#if UNITY_ANDROID
		ConsoliAds.Instance.onInterstitialAdShown(type);
		#endif

        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheInterstitial(CBLocation.Default);

    }

    // Called after an interstitial has been closed.
    void didCloseInterstitial(CBLocation location)
    {
        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheInterstitial(CBLocation.Default);
    }

    // Called after an interstitial has been clicked.
    void didClickInterstitial(CBLocation location)
    {
		#if UNITY_ANDROID
		ConsoliAds.Instance.onInterstitialAdShown(type);
		#endif
        ConsoliAds.Instance.onAdClick(type);
    }

}
