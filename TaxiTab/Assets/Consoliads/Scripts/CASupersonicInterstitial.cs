using UnityEngine;
using System.Collections;

class CASupersonicInterstitial : AdNetwork
{

    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        CASupersonicManager.Instance.startSupersonic();
        Supersonic.Agent.initInterstitial(appKey, uniqueDeviceID);


        // init
        SupersonicEvents.onInterstitialInitSuccessEvent += InterstitialInitSuccessEvent;
        SupersonicEvents.onInterstitialInitFailedEvent += InterstitialInitFailEvent;

        // load
        SupersonicEvents.onInterstitialReadyEvent += InterstitialReadyEvent;
        SupersonicEvents.onInterstitialLoadFailedEvent += InterstitialLoadFailedEvent;

        // show
        SupersonicEvents.onInterstitialShowSuccessEvent += InterstitialShowSuccessEvent;
        SupersonicEvents.onInterstitialShowFailedEvent += InterstitialShowFailEvent;

        // ad interaction
        SupersonicEvents.onInterstitialClickEvent += InterstitialAdClickedEvent;
        //SupersonicEvents.onInterstitialOpenEvent += InterstitialAdOpenedEvent;
        SupersonicEvents.onInterstitialCloseEvent += InterstitialAdClosedEvent;

        
    }

    public override bool showAd(int sceneID)
    {
        if (Supersonic.Agent.isInterstitialReady())
        {
            Supersonic.Agent.showInterstitial();

            Supersonic.Agent.loadInterstitial();

            return true;
        }
        return false;
    }

    public override bool IsAdAvailable(int sceneID)
    {
        if (Supersonic.Agent.isInterstitialReady())
        {
            return true;
        }
        return false;
    }

    void InterstitialInitSuccessEvent()
    {
        Supersonic.Agent.loadInterstitial();
    }
    
    void InterstitialReadyEvent()
    {
        ConsoliAds.Instance.onAdRequested(type);
    }
    void InterstitialLoadFailedEvent(SupersonicError error)
    {
        if(error.getCode() != 501)
        {
            ConsoliAds.Instance.onAdRequestFailed(type);
        }
    }

    void InterstitialInitFailEvent(SupersonicError error)
    {
    }

    void InterstitialShowSuccessEvent()
    {
        ConsoliAds.Instance.onInterstitialAdShown(type);
    }

    void InterstitialShowFailEvent(SupersonicError error)
    {
    }

    void InterstitialAdClickedEvent()
    {
        ConsoliAds.Instance.onAdClick(type);
    }

    void InterstitialAdClosedEvent()
    {
        Supersonic.Agent.loadInterstitial();

        ConsoliAds.Instance.onAdClosed(type);

    }
}
