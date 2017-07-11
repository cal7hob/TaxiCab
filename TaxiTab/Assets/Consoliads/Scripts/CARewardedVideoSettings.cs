using UnityEngine;
using System; 
using System.Collections;
using GoogleMobileAds.Api;

[Serializable]
public class CARewardedVideoSettings
{
    public bool skipFirst = false;
    public AdNetworkTypeRewardedVideo[] networkList;
    public AdNetworkTypeRewardedVideo failOver = AdNetworkTypeRewardedVideo.EMPTY;
    private bool isFirst = true;

    public bool IsFirst
    {
        get
        {
            bool val = isFirst;
            isFirst = false;
            return val;
        }
    }

    private int count = 0;

    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
}
