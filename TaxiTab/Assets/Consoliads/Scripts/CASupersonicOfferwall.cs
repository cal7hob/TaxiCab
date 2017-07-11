using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

class CASupersonicOfferwall : AdNetwork  {
    private int userCredits = 0;

    public override void initialize(string gameObjectName, string uniqueDeviceID){
        CASupersonicManager.Instance.startSupersonic();
        Supersonic.Agent.initOfferwall(appKey, uniqueDeviceID);
        // Add OW Events
        SupersonicEvents.onOfferwallInitSuccessEvent += OfferwallInitSuccessEvent;
        SupersonicEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
        SupersonicEvents.onOfferwallInitFailEvent += OfferwallInitFailEvent;
        SupersonicEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
        SupersonicEvents.onOfferwallShowFailEvent += OfferwallShowFailEvent;
        SupersonicEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
        SupersonicEvents.onGetOfferwallCreditsFailEvent += GetOfferwallCreditsFailEvent;
    }

	public override bool showAd(int sceneID){
        if (Supersonic.Agent.isOfferwallAvailable())
        {
            Supersonic.Agent.showOfferwall();
            return true;
        }
        else {
        }
        return false;
    }

    public override bool IsAdAvailable(int sceneID)
    {
        if (Supersonic.Agent.isOfferwallAvailable())
        {
            return true;
        }
        return false;
    }
    void OfferwallInitSuccessEvent()
    {
    }

    void OfferwallInitFailEvent(SupersonicError error)
    {
    }

    void OfferwallOpenedEvent()
    {
        ConsoliAds.Instance.onInterstitialAdShown(type);
    }

    void OfferwallClosedEvent()
    {
    }

    void OfferwallShowFailEvent(SupersonicError error)
    {
    }

    void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
    {
        userCredits = userCredits + Convert.ToInt32(dict["credits"]);

    }

    void GetOfferwallCreditsFailEvent(SupersonicError error)
    {
    }

}
