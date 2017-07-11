using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using GoogleMobileAds.Api;

public class ServerConfig
{

    private static ServerConfig _instance;
    //------------------------------------------------------------------------------
    private ServerConfig() { }
    //------------------------------------------------------------------------------
    public static ServerConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServerConfig();
            }
            return _instance;
        }
    }

	//called on configure server button click
    public string configureServer(ConsoliAds CAInstance)
    {
        //creating App JSON
        var strJson = new JSONClass();
        strJson.Add("package", CAInstance.bundleIdentifier.ToString());
        strJson.Add("title", CAInstance.productName.ToString());
        strJson.Add("gssdkVersion", CAConstants.ConsoliAdsVersion);
        strJson.Add("userSignature", CAInstance.userSignature.ToString());
        strJson.Add("totalSequences", CAInstance.sceneList.Length.ToString());

        if (String.IsNullOrEmpty(CAInstance.adIDList.gpRateUsURL))
        {
            strJson.Add("gpRateUsURL", AdNetworkFactory.gpRateUsURL + CAInstance.bundleIdentifier);
        }
        else
        {
            strJson.Add("gpRateUsURL", CAInstance.adIDList.gpRateUsURL);
        }

        strJson.Add("asRateUsURL", CAInstance.adIDList.asRateUsURL);

        if (CAInstance.hideAds)
            strJson["isHideAds"].AsInt = 1;
        else
            strJson["isHideAds"].AsInt = 0;
        if (CAInstance.ShowLog)
        {
            strJson["mediationLog"].AsInt = 1;
        }

        if (CAInstance.ChildDirected)
        {
            strJson["childDirected"].AsInt = 1;
        }
        else
        {
            strJson["childDirected"].AsInt = 0;
        }
         

        strJson["analytics"][0]["an_id"].AsInt = (int)CAAnalytics.FireBaseAnalytics;
        strJson["analytics"][0]["an_value"].AsBool = CAInstance.FirebaseAnalytics;
        strJson["analytics"][1]["an_id"].AsInt = (int)CAAnalytics.GoogleAnalytics;
        strJson["analytics"][1]["an_value"].AsBool = CAInstance.GoogleAnalytics;
        strJson["analytics"][2]["an_id"].AsInt = (int)CAAnalytics.FlurryAnalytics;
        strJson["analytics"][2]["an_value"].AsBool = CAInstance.FlurryAnalytics;

        strJson["store"].AsInt = (int)(Platform)CAInstance.platform;
        for (int sequenceCounter = 0; sequenceCounter < CAInstance.sceneList.Length; sequenceCounter++)
        {
            strJson["sequences"][sequenceCounter]["seqTitleID"].AsInt = (int)(SceneTypes)CAInstance.sceneList[sequenceCounter].sceneType;
            strJson["sequences"][sequenceCounter]["isFirstSkip"] = CAInstance.sceneList[sequenceCounter].interstitialAndVideo.skipFirst.ToString();
            strJson["sequences"][sequenceCounter]["failOverAdID"].AsInt = (int)(AdNetworkType)CAInstance.sceneList[sequenceCounter].interstitialAndVideo.failOver;
            strJson["sequences"][sequenceCounter]["isFirstSkipRewardedVideo"] = CAInstance.sceneList[sequenceCounter].rewardedVideo.skipFirst.ToString();
            strJson["sequences"][sequenceCounter]["failOverAdIDRewardedVideo"].AsInt = (int)(AdNetworkType)CAInstance.sceneList[sequenceCounter].rewardedVideo.failOver;

            for (int adCounter = 0; adCounter < CAInstance.sceneList[sequenceCounter].interstitialAndVideo.networkList.Length; adCounter++)
            {
                AdNetworkTypeInterstitialAndVideo ad = CAInstance.sceneList[sequenceCounter].interstitialAndVideo.networkList[adCounter];
                strJson["sequences"][sequenceCounter]["interstitialAndVideo"][adCounter]["adID"].AsInt = (int)ad;
                strJson["sequences"][sequenceCounter]["interstitialAndVideo"][adCounter]["adOrder"].AsInt = (adCounter + 1);
            }

            for (int adCounter = 0; adCounter < CAInstance.sceneList[sequenceCounter].rewardedVideo.networkList.Length; adCounter++)
            {
                AdNetworkTypeRewardedVideo ad = CAInstance.sceneList[sequenceCounter].rewardedVideo.networkList[adCounter];
                strJson["sequences"][sequenceCounter]["rewardedVideo"][adCounter]["adID"].AsInt = (int)ad;
                strJson["sequences"][sequenceCounter]["rewardedVideo"][adCounter]["adOrder"].AsInt = (adCounter + 1);
            }

            strJson["sequences"][sequenceCounter]["native"]["enabled"].AsBool = CAInstance.sceneList[sequenceCounter].native.enabled;
            strJson["sequences"][sequenceCounter]["native"]["adID"].AsInt = (int)CAInstance.sceneList[sequenceCounter].native.adType;
            strJson["sequences"][sequenceCounter]["native"]["width"].AsInt = CAInstance.sceneList[sequenceCounter].native.width;
            strJson["sequences"][sequenceCounter]["native"]["height"].AsInt = CAInstance.sceneList[sequenceCounter].native.height;
            strJson["sequences"][sequenceCounter]["native"]["position"].AsInt = (int)CAInstance.sceneList[sequenceCounter].native.position;

            strJson["sequences"][sequenceCounter]["banner"]["enabled"].AsBool = CAInstance.sceneList[sequenceCounter].banner.enabled;
            strJson["sequences"][sequenceCounter]["banner"]["adID"].AsInt = (int)CAInstance.sceneList[sequenceCounter].banner.adType;
            strJson["sequences"][sequenceCounter]["banner"]["size"].AsInt = (int)CAInstance.sceneList[sequenceCounter].banner.size;
            strJson["sequences"][sequenceCounter]["banner"]["position"].AsInt = (int)CAInstance.sceneList[sequenceCounter].banner.position;

        }
		// Debug.Log(strJson.ToString());
        //sending JSON and retriving result from server
        String url = CAConstants.ConsoliAdsConfigURL;
        WWW result = CAInstance.postAppJson(url, strJson.ToString());


        if (result.error != null)
        {
        }
        else {
            var responseArray = JSONNode.Parse(result.text);
            if (responseArray != null)
            {
                populateResponse(responseArray, CAInstance);
                if (responseArray["message"] != null)
                {
                    return responseArray["message"];
                }
            }
        }
        return null;
    }
	//starts on gameplay
    public IEnumerator syncWithServer()
    {
        //creating App JSON
        int errorCode;
        var strJson = new JSONClass();
        strJson.Add("package", ConsoliAds.Instance.bundleIdentifier.ToString());
        strJson["store"].AsInt = (int)ConsoliAds.Instance.platform;
        strJson["uniqueDeviceID"] = SystemInfo.deviceUniqueIdentifier;

        strJson["appID"] = PlayerPrefs.GetString("ConsoliAds_AppID", "");
        strJson["deviceID"] = PlayerPrefs.GetString("ConsoliAds_DeviceID", "");
        strJson["region"] = PlayerPrefs.GetString("ConsoliAds_Region", "");

		var queueStats = AdNetwork.getQueueEventStatsAll();

        if (queueStats["eventStats"] != null)
        {
            strJson["adsQueueEventStats"] = queueStats["eventStats"];
        }
        String url = CAConstants.ConsoliAdsSyncURL;

        WWWForm form = new WWWForm();
		//CALogManager.Instance.Log("syncWithServer uploading states" + queueStats.ToString());
        form.AddField("appJson", strJson.ToString());
        //works in the background
        WWW www = new WWW(url, form);
        yield return www;

        if (www.error != null)
        {
            errorCode = -1;
			Debug.Log ("error: syncWithServer");
        }
        else {
            var responseArray = JSONNode.Parse(www.text);
			Debug.Log ("success: recieve response");
            if (responseArray != null)
            {
                if (responseArray["message"] != null && responseArray["message"].ToString().Contains("completed"))
                {
                    AdNetwork.resetQueueEventStats();
                }
                populateResponse(responseArray, ConsoliAds.Instance);
            }
        }
        //calling here because this sync method runs in the background
        ConsoliAds.Instance.SetupAdNetworks();
    }
    public IEnumerator sendNetworkStats()
    {
        //creating App JSON
        int errorCode;
        var strJson = new JSONClass();
        strJson.Add("package", ConsoliAds.Instance.bundleIdentifier.ToString());
        strJson["store"].AsInt = (int)ConsoliAds.Instance.platform;
        strJson["uniqueDeviceID"] = SystemInfo.deviceUniqueIdentifier;

        strJson["appID"] = PlayerPrefs.GetString("ConsoliAds_AppID", "");
        strJson["deviceID"] = PlayerPrefs.GetString("ConsoliAds_DeviceID", "");
        strJson["region"] = PlayerPrefs.GetString("ConsoliAds_Region", "");

        var queueStats = AdNetwork.getQueueEventStats();

        if (queueStats["eventStats"] != null)
        {
            strJson["adsQueueEventStats"] = queueStats["eventStats"];
        }
        //strJson["uniqueDeviceID"] = "dcsdv1313r";
        //sending JSON and retriving result from server
        String url = CAConstants.sendNetworkStatsURL;
        //works in the background
        //WWW www = ConsoliAds.Instance.postAppJson (url, strJson.ToString ());
        WWWForm form = new WWWForm();
        form.AddField("appJson", strJson.ToString());
        //works in the background
        WWW www = new WWW(url, form);
        yield return www;

        if (www.error != null)
        {
            errorCode = -1;
        }
        else {
            var responseArray = JSONNode.Parse(www.text);
            if (responseArray != null && responseArray["message"] != null && responseArray["message"].ToString().Contains("completed"))
            {

                if (queueStats["eventStats"] != null)
                {
                    AdNetwork.resetQueueEventStats(queueStats);
                }
            }
        }
    }

    private void populateResponse(JSONNode responseArray, ConsoliAds CAInstance)
    {
        if (responseArray != null)
        {
            //checking to enable log
            if (responseArray["mediationMode"] != null)
            {
                if (responseArray["mediationMode"].ToString().ToLower().Contains("test"))
                {
                    CAInstance.EnableLog(true);
                }
                else
                {
                    CAInstance.EnableLog(false);
                }
            }
            else
            {
                CAInstance.EnableLog(false);
            }
            if (responseArray["childDirected"] != null)
            {
                if (responseArray["childDirected"].AsInt == 1)
                {
                    CAInstance.ChildDirected = true;
                }
                else
                {
                    CAInstance.ChildDirected = false;
                }
            }

            if (responseArray["gpRateUsURL"] != null)
            {
                CAInstance.adIDList.gpRateUsURL = responseArray["gpRateUsURL"];
            }
            if (responseArray["asRateUsURL"] != null)
            {
                CAInstance.adIDList.asRateUsURL = responseArray["asRateUsURL"];
            }

            //saving deviceID in the sharedPrefs
            if (responseArray["deviceID"] != null)
            {
                PlayerPrefs.SetString("ConsoliAds_DeviceID", responseArray["deviceID"]);
            }
            if (responseArray["appID"] != null)
            {
                PlayerPrefs.SetString("ConsoliAds_AppID", responseArray["appID"]);
            }
            if (responseArray["region"] != null)
            {
                PlayerPrefs.SetString("ConsoliAds_Region", responseArray["region"]);
            }
            //populating inspector
            if (responseArray["package"] != null)
            {
                CAInstance.bundleIdentifier = responseArray["package"];
            }
            if (responseArray["title"] != null)
            {
                CAInstance.productName = responseArray["title"];
            }
            if (responseArray["adsQueueType"] != null)
            {
                if (responseArray["adsQueueType"].ToString().Contains("priority"))
                {
                    CAInstance.setAdNetworkQueueType(AdNetworkQueueType.Priority);
                }
            }
            int hideAllAds = PlayerPrefs.GetInt("consoliads_hide_all_ads", 0);
            if (hideAllAds == 1)
            {
                CAInstance.hideAds = true;
            }
            else if (responseArray["isHideAds"] != null && responseArray["isHideAds"].AsInt == 1)
            {
                CAInstance.hideAds = true;
            }
            else {
                CAInstance.hideAds = false;
            }
            if (responseArray["supportURL"] != null)
            {
                CAInstance.supportEmail = responseArray["supportURL"];
            }
            if (responseArray["gpMoreAppsURL"] != null)
            {
                CAInstance.adIDList.gpMoreAppsURL = responseArray["gpMoreAppsURL"];
            }
            if (responseArray["asMoreAppsURL"] != null)
            {
                CAInstance.adIDList.asMoreAppsURL = responseArray["asMoreAppsURL"];
            }
            if (responseArray["sequences"] != null)
            {
                //initializing ad sequences array to the size of the return JSON Array from server
                CAInstance.sceneList = new CAScene[responseArray["sequences"].Count];
                //populating ad sequences
                for (int sequenceCounter = 0; sequenceCounter < responseArray["sequences"].Count; sequenceCounter++)
                {
                    //initializing each array item  of ad sequence
                    CAInstance.sceneList[sequenceCounter] = new CAScene();
                    CAInstance.sceneList[sequenceCounter].interstitialAndVideo = new CAInterstitialAndVideoSettings();
                    CAInstance.sceneList[sequenceCounter].rewardedVideo = new CARewardedVideoSettings();
                    CAInstance.sceneList[sequenceCounter].native = new CANativeAdSettings();
                    CAInstance.sceneList[sequenceCounter].banner = new CABannerSettings();
                    //populating sequence values
                    if (responseArray["sequences"][sequenceCounter]["isFirstSkip"].AsInt == 1)
                    {
                        CAInstance.sceneList[sequenceCounter].interstitialAndVideo.skipFirst = true;
                    }
                    else
                    {
                        CAInstance.sceneList[sequenceCounter].interstitialAndVideo.skipFirst = false;
                    }
                    //populating sequence values
                    if (responseArray["sequences"][sequenceCounter]["isFirstSkipRewardedVideo"].AsInt == 1)
                    {
                        CAInstance.sceneList[sequenceCounter].rewardedVideo.skipFirst = true;
                    }
                    else
                    {
                        CAInstance.sceneList[sequenceCounter].rewardedVideo.skipFirst = false;
                    }

                    CAInstance.sceneList[sequenceCounter].interstitialAndVideo.failOver = (AdNetworkTypeInterstitialAndVideo)responseArray["sequences"][sequenceCounter]["failOverAdID"].AsInt;
                    CAInstance.sceneList[sequenceCounter].rewardedVideo.failOver = (AdNetworkTypeRewardedVideo)responseArray["sequences"][sequenceCounter]["failOverAdIDRewardedVideo"].AsInt;
                    CAInstance.sceneList[sequenceCounter].sceneType = (SceneTypes)responseArray["sequences"][sequenceCounter]["seqTitleID"].AsInt;

                    //initializing ad sequence's ads Array to the size received in JSON Array 
                    CAInstance.sceneList[sequenceCounter].interstitialAndVideo.networkList = new AdNetworkTypeInterstitialAndVideo[responseArray["sequences"][sequenceCounter]["interstitialAndVideo"].Count];
                    //populating ad sequence's Ads Array
                    for (int adCounter = 0; adCounter < responseArray["sequences"][sequenceCounter]["interstitialAndVideo"].Count; adCounter++)
                    {
                        CAInstance.sceneList[sequenceCounter].interstitialAndVideo.networkList[adCounter] = (AdNetworkTypeInterstitialAndVideo)responseArray["sequences"][sequenceCounter]["interstitialAndVideo"][adCounter]["adID"].AsInt;

                    }
                    CAInstance.sceneList[sequenceCounter].rewardedVideo.networkList = new AdNetworkTypeRewardedVideo[responseArray["sequences"][sequenceCounter]["rewardedVideo"].Count];
                    //populating ad sequence's Ads Array
                    for (int adCounter = 0; adCounter < responseArray["sequences"][sequenceCounter]["rewardedVideo"].Count; adCounter++)
                    {
                        CAInstance.sceneList[sequenceCounter].rewardedVideo.networkList[adCounter] = (AdNetworkTypeRewardedVideo)responseArray["sequences"][sequenceCounter]["rewardedVideo"][adCounter]["adID"].AsInt;

                    }
                    //populating native ad settings
                    if (responseArray["sequences"][sequenceCounter]["native"] != null)
                    {
                        CAInstance.sceneList[sequenceCounter].native = new CANativeAdSettings();
                        CAInstance.sceneList[sequenceCounter].native.enabled = responseArray["sequences"][sequenceCounter]["native"]["enabled"].AsBool;
                        CAInstance.sceneList[sequenceCounter].native.width = responseArray["sequences"][sequenceCounter]["native"]["width"].AsInt;
                        CAInstance.sceneList[sequenceCounter].native.height = responseArray["sequences"][sequenceCounter]["native"]["height"].AsInt;
                        CAInstance.sceneList[sequenceCounter].native.position = (AdPosition)responseArray["sequences"][sequenceCounter]["native"]["position"].AsInt;
                    }
                    else
                    {
                        CAInstance.sceneList[sequenceCounter].native = new CANativeAdSettings();
                        CAInstance.sceneList[sequenceCounter].native.enabled = false;
                    }
                    //populating native ad settings

                    if (responseArray["sequences"][sequenceCounter]["banner"] != null)
                    {
                        CAInstance.sceneList[sequenceCounter].banner = new CABannerSettings();
                        CAInstance.sceneList[sequenceCounter].banner.enabled = responseArray["sequences"][sequenceCounter]["banner"]["enabled"].AsBool;
                        CAInstance.sceneList[sequenceCounter].banner.size = (AdmobBannerSize)responseArray["sequences"][sequenceCounter]["banner"]["size"].AsInt;
                        CAInstance.sceneList[sequenceCounter].banner.position = (AdPosition)responseArray["sequences"][sequenceCounter]["banner"]["position"].AsInt;
                    }
                    else
                    {
                        CAInstance.sceneList[sequenceCounter].banner = new CABannerSettings();
                        CAInstance.sceneList[sequenceCounter].banner.enabled = false;
                    }
                }
            }
            if (responseArray["adIDs"] != null)
            {
                //populating ad IDs
                for (int adIDCounter = 0; adIDCounter < responseArray["adIDs"].Count; adIDCounter++)
                {
                    AdValueType type = (AdValueType)responseArray["adIDs"][adIDCounter]["adValueType"].AsInt;
                    Platform platform = (Platform)responseArray["adIDs"][adIDCounter]["OS"].AsInt;
                    String key = responseArray["adIDs"][adIDCounter]["adValue"];
                    setAdNetworkKey(type, platform, key, CAInstance);

                }
            }
            if (responseArray["analytics"] != null)
            {
                //populating ad IDs
                for (int i = 0; i < responseArray["analytics"].Count; i++)
                {
                    CAAnalytics type = (CAAnalytics)responseArray["analytics"][i]["an_id"].AsInt;
                    bool value = responseArray["analytics"][i]["an_value"].AsBool;
                    setAnalyticsValue(type, value, CAInstance);

                }
            }

             

        }
    }

    private void setAnalyticsValue(CAAnalytics type, bool value, ConsoliAds CAInstance)
    {
        switch (type)
        {
            case CAAnalytics.GoogleAnalytics:
                CAInstance.GoogleAnalytics = value;
                break;
            case CAAnalytics.FlurryAnalytics:
                CAInstance.FlurryAnalytics = value;
                break;
            case CAAnalytics.FireBaseAnalytics:
                CAInstance.FirebaseAnalytics = value;
                break;

        }
    }

    private void setAdNetworkKey(AdValueType type, Platform platform, String key, ConsoliAds CAInstance)
    {
        switch (type)
        {
            case AdValueType.ConsoliadsAppKey:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpConsoliadsAppKey = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asConsoliadsAppKey = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amConsoliadsAppKey = key;
                break;
            case AdValueType.ChartboostAppID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpChartboostAppID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asChartboostAppID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amChartboostAppID = key;
                break;
            case AdValueType.ChartboostAppSignature:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpChartboostAppSignature = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asChartboostAppSignature = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amChartboostAppSignature = key;
                break;
            case AdValueType.AdmobAppID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdmobAppID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdmobAppID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdmobAppID = key;
                break;
            case AdValueType.AdmobBannerAdUnitID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdmobBannerAdUnitID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdmobBannerAdUnitID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdmobBannerAdUnitID = key;
                break;
            case AdValueType.AdmobInterstitialAdUnitID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdmobInterstitialAdUnitID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdmobInterstitialAdUnitID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdmobInterstitialAdUnitID = key;
                break;
            case AdValueType.AdmobRewardedVideoAdUnitID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdmobRewardedVideoAdUnitID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdmobRewardedVideoAdUnitID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdmobRewardedVideoAdUnitID = key;
                break;
            case AdValueType.AdmobNativeAdID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdmobNativeAdUnitID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdmobNativeAdUnitID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdmobNativeAdUnitID = key;
                break;
            case AdValueType.HeyzapID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpHeyzapID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asHeyzapID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amHeyzapID = key;
                break;
            case AdValueType.RevmobMediaID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpRevmobMediaID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asRevmobMediaID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amRevmobMediaID = key;
                break;
            case AdValueType.UnityAdsID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpUnityadsAppID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asUnityadsAppID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amUnityadsAppID = key;
                break;
            case AdValueType.AdColonyAppID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdcolonyAppID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdcolonyAppID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdcolonyAppID = key;
                break;
            case AdValueType.AdColonyZoneID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpAdcolonyZoneID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asAdcolonyZoneID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amAdcolonyZoneID = key;
                break;
            case AdValueType.SupersonicAppKey:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpSupersonicAppKey = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asSupersonicAppKey = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amSupersonicAppKey = key;
                break;
            case AdValueType.AppLovinID:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpApplovinID = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asApplovinID = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amApplovinID = key;
                break;
            case AdValueType.LeadboltAppKey:
                if (platform == Platform.Google)
                    CAInstance.adIDList.gpLeadboltAppKey = key;
                else if (platform == Platform.Apple)
                    CAInstance.adIDList.asLeadboltAppKey = key;
                else if (platform == Platform.Amazon)
                    CAInstance.adIDList.amLeadboltAppKey = key;
                break;
            case AdValueType.GoogleAnalyticsTrackingCode:
                if (platform == Platform.Google)
                    CAInstance.gpAnalyticsTrackingCode = key;
                else if (platform == Platform.Apple)
                    CAInstance.asAnalyticsTrackingCode = key;
                break;
            case AdValueType.FlurryAnalyticsAppKey:
                if (platform == Platform.Google)
                    CAInstance.gpFlurryAppKey = key;
                else if (platform == Platform.Apple)
                    CAInstance.asFlurryAppKey = key;
                break;
        }
    }
}
