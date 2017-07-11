using UnityEngine;
using System.Collections;
using Heyzap;
public static class CAHeyzapAdManager
{
    public static void initialize(string appkey)
    {


        HeyzapAds.Start(appkey, HeyzapAds.FLAG_NO_OPTIONS);
        HeyzapAds.ShowDebugLogs();
        //this is how you can set ad display listeners in your own script 
        HZBannerAd.SetDisplayListener(bannerDisplayListener);

        HZInterstitialAd.SetDisplayListener(interstitialDisplayListener);

        HZIncentivizedAd.SetDisplayListener(videoDisplayListener);
        

        //HeyzapAds.SetNetworkCallbackListener(networkCallbackListner);

        // this.bannerControls.SetActive(false);
        //this.nonBannerControls.SetActive(true);

        // UI defaults
        // this.bannerPosition = HZBannerShowOptions.POSITION_TOP;
        // this.SelectedAdType = AdType.Interstitial;
        // HeyzapAds.HideDebugLogs();
    }

    public static bool showInterstitial()
    {
        if (HZInterstitialAd.IsAvailable())
        {
            HZInterstitialAd.Show();
            return true;
        }
        return false;
    }
    public static void fetchInterstitial()
    {
        HZInterstitialAd.Fetch();
    }
    public static bool IsInterstitialAvailable()
    {
        if (HZInterstitialAd.IsAvailable())
        {
            return true;
        }
        return false;
    }
    public static bool showRewardedVideo()
    {
        if (HZIncentivizedAd.IsAvailable())
        {
            HZIncentivizedAd.Show();
            return true;
        }
        return false;
    }
    public static void fetchRewardedVideo()
    {
        HZIncentivizedAd.Fetch();

    }
    public static bool IsRewardedVideoAvailable()
    {
        if (HZIncentivizedAd.IsAvailable())
        {
            return true;
        }
        return false;
    }
    public static void showBanner(HZBannerShowOptions bannerOptions)
    {
        HZBannerAd.ShowWithOptions(bannerOptions);
    }


    public static void networkCallbackListner(string network, string callback)
    {
    }
    public static void bannerDisplayListener(string adState, string adTag)
    {
        if (adState == "loaded")
        {
            Rect dimensions = new Rect();
            HZBannerAd.GetCurrentBannerDimensions(out dimensions);
            Debug.Log(string.Format("    (x,y): ({0},{1}) - WxH: {2}x{3}", dimensions.x, dimensions.y, dimensions.width, dimensions.height));
        }
    }
    public static void interstitialDisplayListener(string adState, string adTag)
    {

        if (adState.Equals("show"))
        {
            ConsoliAds.Instance.onInterstitialAdShown(AdNetworkType.HEYZAPINTERSTITIAL);
        }
        if (adState.Equals("failed"))
        {
            // Sent when you call `show`, but there isn't an ad to be shown.
            ConsoliAds.Instance.onAdShowFailed(AdNetworkType.HEYZAPINTERSTITIAL);

        }
        if (adState.Equals("hide"))
        {
            ConsoliAds.Instance.onAdClosed(AdNetworkType.HEYZAPINTERSTITIAL);
        }
        if (adState.Equals("click"))
        {
            ConsoliAds.Instance.onAdClick(AdNetworkType.HEYZAPINTERSTITIAL);

        }
        if (adState.Equals("available"))
        {
            // Sent when an ad has been loaded and is ready to be displayed,
            //   either because we autofetched an ad or because you called
            //   `Fetch`.
            ConsoliAds.Instance.onAdRequested(AdNetworkType.HEYZAPINTERSTITIAL);

        }
        if (adState.Equals("fetch_failed"))
        {
            ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.HEYZAPINTERSTITIAL);
        }
    }
    public static void videoDisplayListener(string adState, string adTag)
    {
        if (adState.Equals("show"))
        {
            ConsoliAds.Instance.onVideoAdShown(AdNetworkType.HEYZAPVIDEO);
        }
        if (adState.Equals("click"))
        {
            ConsoliAds.Instance.onAdClick(AdNetworkType.HEYZAPVIDEO);
        }
        if (adState.Equals("failed"))
        {
            // Sent when you call `show`, but there isn't an ad to be shown.
            ConsoliAds.Instance.onAdShowFailed(AdNetworkType.HEYZAPVIDEO);

        }
        if (adState.Equals("hide"))
        {
            ConsoliAds.Instance.onAdClosed(AdNetworkType.HEYZAPVIDEO);
        }
        if (adState.Equals("available"))
        {
            // Sent when an ad has been loaded and is ready to be displayed,
            //   either because we autofetched an ad or because you called
            //   `Fetch`.
            ConsoliAds.Instance.onAdRequested(AdNetworkType.HEYZAPVIDEO);
        }
        if (adState.Equals("fetch_failed"))
        {
            ConsoliAds.Instance.onAdRequestFailed(AdNetworkType.HEYZAPVIDEO);
        }
    }
}
