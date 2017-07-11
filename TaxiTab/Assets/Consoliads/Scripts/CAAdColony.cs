using UnityEngine;
using System.Collections;

class CAAdColony : AdNetwork
{

    public string adColonyZoneID;
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        // Assign any AdColony Delegates before calling Configure
        AdColony.OnVideoFinished = this.OnAdColonyVideoFinished;
        AdColony.OnAdAvailabilityChange = this.AdAvailabilityChangeDelegate;

        // If you wish to use a the customID feature, you should call  that now.
        // Then, configure AdColony:
        AdColony.Configure
            (
                ConsoliAds.Instance.bundleVersion, // Arbitrary app version and Android app store declaration.
                appKey,   // ADC App ID from adcolony.com
                adColonyZoneID
                );
    }

    public override bool showAd(int sceneID)
    {
        // Check to see if a video is available in the zone.
        if (AdColony.IsVideoAvailable(adColonyZoneID))
        {
            // Call AdColony.ShowVideoAd with that zone to play an interstitial video.
            // Note that you should also pause your game here (audio, etc.) AdColony will not
            // pause your app for you.
            AdColony.ShowVideoAd(adColonyZoneID);
            return true;
        }
        else {
            return false;
        }
    }

    public override bool IsAdAvailable(int sceneID)
    {
        if (AdColony.IsVideoAvailable(adColonyZoneID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnAdColonyVideoFinished(bool ad_was_shown)
    {
        if (ad_was_shown)
        {
            ConsoliAds.Instance.onVideoAdShown(type);

        }
    }
    public void AdAvailabilityChangeDelegate(bool available, string zoneId)
    {
        if (available)
        {
            ConsoliAds.Instance.onAdRequested(type);
        }
        else
        {
            ConsoliAds.Instance.onAdRequestFailed(type);
        }
    }

}
