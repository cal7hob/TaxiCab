using UnityEngine;
using System; 
using System.Collections;
using GoogleMobileAds.Api;

[Serializable]
public class CANativeAdSettings {

     

    public Boolean enabled;
    public NativeAds adType = NativeAds.ADMOBNATIVEAD;

    public AdPosition position;
    public int width;
    public int height;
}
