using UnityEngine;
using System; 
using System.Collections;
using GoogleMobileAds.Api;

[Serializable]
public class CABannerSettings {

     

    public Boolean enabled;
    public AdNetworkTypeBanner adType = AdNetworkTypeBanner.ADMOBBANNER;

    public AdmobBannerSize size = AdmobBannerSize.SmartBanner;
    public AdPosition position = AdPosition.Top;
}
