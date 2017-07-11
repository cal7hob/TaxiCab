using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMobileAds.Api;
class CAAdmobNativeAd
{

    private NativeExpressAdView nativeExpressAdView = null; 
    ~CAAdmobNativeAd()  // destructor
    {
        // cleanup statements...
        nativeExpressAdView.Destroy();
    }
    public void LoadAd(string appKey, int width, int height, AdPosition position)
    { 
        if(nativeExpressAdView != null)
        {
            nativeExpressAdView.Destroy();
        }
        // Create native express ad.
        nativeExpressAdView = new NativeExpressAdView(appKey, new AdSize(width, height), position);
        //nativeExpressAdView = new NativeExpressAdView(appKey, new AdSize(320, 150), AdPosition.Top);
        // Load ad.
        nativeExpressAdView.LoadAd(new AdRequest.Builder().Build());
       
    } 
    public void Show()
    {
        //show the hidden native ad
        if(nativeExpressAdView != null)
        {
            nativeExpressAdView.Show();
        }
    }
    public void Hide()
    {
        //hides the current native ad
        if (nativeExpressAdView != null)
        {
            nativeExpressAdView.Hide();
        }
    }
    public void Destroy()
    {
        if (nativeExpressAdView != null)
        {
            nativeExpressAdView.Destroy();
        }
    }
}
