using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class CAChartBoostMoreApps : AdNetwork {

	public string appSignature { get; set;}

	public override void initialize(string gameObjectName, string uniqueDeviceID){

        //delegate event methods
        Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
        Chartboost.didDismissMoreApps += didDismissMoreApps;
        Chartboost.didCloseMoreApps += didCloseMoreApps;
        Chartboost.didClickMoreApps += didClickMoreApps;
        Chartboost.didCacheMoreApps += didCacheMoreApps;
        Chartboost.didDisplayMoreApps += didDisplayMoreApps;

#if (UNITY_ANDROID || UNITY_IPHONE)
        if (!Chartboost.isInitialized())
        {
            Chartboost.CreateWithAppId(appKey, appSignature);
            Chartboost.setAutoCacheAds(true);
        }
        Chartboost.cacheMoreApps(CBLocation.Default);

#endif
    }

    public override bool showAd(int sceneID){
		//CBLocation location = getCBLocation ((SceneTypes)sceneID);
        CBLocation location = CBLocation.Default;

        bool result = true;
        #if (UNITY_ANDROID || UNITY_IPHONE)
        if (Chartboost.hasMoreApps(location)) {
			Chartboost.showMoreApps(location);
		} else {
			result = false;
            Chartboost.cacheMoreApps(location);
		}
		#endif
		return result;
	}
    public override void requestAd()
    {
        Chartboost.cacheMoreApps(CBLocation.Default);
    }

    public override bool IsAdAvailable(int sceneID)
    {
        CBLocation location = getCBLocation((SceneTypes)sceneID);
        if (Chartboost.hasMoreApps(CBLocation.Default))
        {
            return true;
        }
        return false;
    }

    // Called after a MoreApps page has been displayed on the screen.
    void didDisplayMoreApps(CBLocation location) {
        ConsoliAds.Instance.onInterstitialAdShown(type);
    }

    // Called after a MoreApps page has been loaded from the Chartboost API
    // servers and cached locally.
    void didCacheMoreApps(CBLocation location) {
        ConsoliAds.Instance.onAdRequested(type);
    }

    // Called after a MoreApps page has been dismissed.
    void didDismissMoreApps(CBLocation location) {
        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheMoreApps(CBLocation.Default);

    }

    // Called after a MoreApps page has been closed.
    void didCloseMoreApps(CBLocation location) {
        ConsoliAds.Instance.onAdClosed(type);
        Chartboost.cacheMoreApps(CBLocation.Default);

    }

    // Called after a MoreApps page has been clicked.
    void didClickMoreApps(CBLocation location) {
        ConsoliAds.Instance.onAdClick(type);
    }

    // Called after a MoreApps page attempted to load from the Chartboost API
    // servers but failed.
    void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
        if(error != CBImpressionError.InternetUnavailable)
        {
            ConsoliAds.Instance.onAdRequestFailed(type);
        }
    }
}
