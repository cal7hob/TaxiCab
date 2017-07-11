using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class CASupersonicRewardedVideo : AdNetwork
{
    private int userCredits = 0;
    private string deviceID;

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        deviceID = uniqueDeviceID;
        CASupersonicManager.Instance.startSupersonic();
        Supersonic.Agent.initRewardedVideo(appKey, uniqueDeviceID);
        // Add OW Events
        SupersonicEvents.onRewardedVideoInitSuccessEvent += RewardedVideoInitSuccessEvent;
        SupersonicEvents.onRewardedVideoInitFailEvent += RewardedVideoInitFailEvent;
        SupersonicEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        SupersonicEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        SupersonicEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        SupersonicEvents.onVideoAvailabilityChangedEvent += VideoAvailabilityChangedEvent;
        SupersonicEvents.onVideoStartEvent += VideoStartEvent;
        SupersonicEvents.onVideoEndEvent += VideoEndEvent;
    }

    public override bool showAd(int sceneID)
    {
        if (Supersonic.Agent.isRewardedVideoAvailable())
        {
            Supersonic.Agent.showRewardedVideo();

            return true;
        }
        return false;
    }

    public override bool IsAdAvailable(int sceneID)
    {
        if (Supersonic.Agent.isRewardedVideoAvailable())
        {
            return true;
        }
        return false;
    }
    void RewardedVideoInitSuccessEvent()
    {
    }

    void VideoAvailabilityChangedEvent(bool canShowAd)
    {
        if (canShowAd)
        {
            ConsoliAds.Instance.onAdRequested(type);
        }
        else
        {
            ConsoliAds.Instance.onAdRequestFailed(type);
        }
    }

    void RewardedVideoInitFailEvent(SupersonicError error)
    {
    }

    void RewardedVideoAdOpenedEvent()
    {
        ConsoliAds.Instance.onRewardedVideoAdShown(type);
    }

    void RewardedVideoAdRewardedEvent(SupersonicPlacement ssp)
    {
        userCredits = userCredits + ssp.getRewardAmount();

        ConsoliAds.Instance.onRewardedVideoAdCompleted(type);


    }

    void RewardedVideoAdClosedEvent()
    {
        if (Supersonic.Agent.isRewardedVideoAvailable())
        {
            ConsoliAds.Instance.onAdRequested(type);
        }
    }

    void VideoStartEvent()
    {
    }

    void VideoEndEvent()
    {
    }


}
