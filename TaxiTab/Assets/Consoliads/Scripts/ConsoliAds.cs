using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using FlurryAnalytics;


public class ConsoliAds : MonoBehaviour, IRevMobListener
{
    private string appID;
    private List<AdNetwork> supportedAdNetworks = new List<AdNetwork>();
    private Hashtable adNetworksList = new Hashtable();
    private AdNetworkFactory adNetworkFactory = new AdNetworkFactory();
    private const string gameObjectName = "ConsoliAds";
    private static ConsoliAds _instance;
    private CAAdmob mAdmob = new CAAdmob();
    private bool initAdmobBanner = false;
    private int currentSceneID = 0;
    private AdNetworkQueueType adNetworkQueueType = AdNetworkQueueType.RoundRobin;
    private bool showBanner = false;
    private int showBannerIndex;
    private CAAdmobNativeAd admobNativeAd;
    private bool loadAdmobNativeAdAfterInit = false;

    public CAFirebase firebase;

    public static event Action onInterstitialAdShownEvent;
    public static event Action onVideoAdShownEvent;
    public static event Action onRewardedVideoAdShownEvent;
    public static event Action onRewardedVideoAdCompletedEvent;
    public static event Action onRewardedVideoAdClickEvent;
    public static event Action onPopupAdShownEvent;

    [Header("ConsoliAds Version 1.2.1")]
    public string userSignature;
    public string productName;
    public Platform platform = Platform.Google;
    public string bundleIdentifier;
    public string bundleVersion;
    public string supportEmail;
    public bool hideAds = false;
    public bool ShowLog = true;
    public bool ChildDirected = false;
    public CAScene[] sceneList;


    public CAAdIDList adIDList;
    [Header("Select Analytics")]
    public bool FirebaseAnalytics = false;
    public bool GoogleAnalytics = false;
    public bool FlurryAnalytics = false;
    [Header("Google Analytics")]
    public string gpAnalyticsTrackingCode;
    public string asAnalyticsTrackingCode;

    [Header("Flurry Analytics")]
    public string gpFlurryAppKey;
    public string asFlurryAppKey;

    public GoogleAnalyticsV4 googleAnalytics;
    public CALeadbolt leadbolt;
    // Use this for initialization

    private List<int> cacheInterstitialRequest = new List<int>();
    private List<int> cacheRewardedRequest = new List<int>();
    private bool initialized = false;

    void Awake()
    {
		//TODO: when delivering to client comment the below line
        //PlayerPrefs.DeleteAll();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            CALogManager.Instance.EnableLog(ShowLog);
            StartCoroutine(ServerConfig.Instance.syncWithServer());
            //SetupAdNetworks(); //is going to be called inside syncWithServer function

        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private ConsoliAds() { }
    public static ConsoliAds Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ConsoliAds>();
                if (_instance && _instance.gameObject)
                {
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }
    public void SetupAdNetworks()
    {
        adNetworkFactory.setupAdNetworkIDs();
        SetupinterstitialAndVideo();
        inititalizeAdNetworks();
        initializeAnalytics();
        initialized = true;
        if (showBanner)
        {
            ShowBanner(showBannerIndex);
        }

        StartCoroutine(ProcessCachedAdRequest());
    }
    private IEnumerator ProcessCachedAdRequest()
    {
        yield return new WaitForSeconds(5);

        for (int i = 0; i < cacheInterstitialRequest.Count; i++)
        {
            ShowInterstitial(cacheInterstitialRequest[i]);
        }
        for (int i = 0; i < cacheRewardedRequest.Count; i++)
        {
            ShowRewardedVideo(cacheRewardedRequest[i]);
        }
        cacheInterstitialRequest.Clear();
        cacheRewardedRequest.Clear();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {

            StartCoroutine(ServerConfig.Instance.sendNetworkStats());

			CAInterstitial network = (CAInterstitial)ConsoliAds.Instance.getFromAdNetworkList(AdNetworkType.CONSOLIADS);

			if (network != null) {
				string uniqueDeviceID = SystemInfo.deviceUniqueIdentifier;
				network.sendStatsOnPause(uniqueDeviceID);
			}

        }
    }

    void SetupinterstitialAndVideo()
    {
        foreach (CAScene scene in sceneList)
        {
            foreach (AdNetworkTypeInterstitialAndVideo type in scene.interstitialAndVideo.networkList)
            {
                if (!adNetworksList.ContainsKey((AdNetworkType)type))
                {
                    AdNetwork adNetwork = adNetworkFactory.getAdNetworkInstance((AdNetworkType)type);

                    if (adNetwork != null)
                        adNetworksList[(AdNetworkType)type] = adNetwork;
                }
            }
            foreach (AdNetworkTypeRewardedVideo type in scene.rewardedVideo.networkList)
            {
                if (!adNetworksList.ContainsKey((AdNetworkType)type))
                {
                    AdNetwork adNetwork = adNetworkFactory.getAdNetworkInstance((AdNetworkType)type);

                    if (adNetwork != null)
                        adNetworksList[(AdNetworkType)type] = adNetwork;
                }
            }
            //adding failover
            AdNetworkTypeInterstitialAndVideo failOver = scene.interstitialAndVideo.failOver;
            if (!adNetworksList.ContainsKey((AdNetworkType)failOver))
            {
                AdNetwork adNetwork = adNetworkFactory.getAdNetworkInstance((AdNetworkType)failOver);
                if (adNetwork != null)
                    adNetworksList[(AdNetworkType)failOver] = adNetwork;
            }
            //adding failover
            AdNetworkTypeRewardedVideo failOverRewardedVideo = scene.rewardedVideo.failOver;
            if (!adNetworksList.ContainsKey((AdNetworkType)failOverRewardedVideo))
            {
                AdNetwork adNetwork = adNetworkFactory.getAdNetworkInstance((AdNetworkType)failOverRewardedVideo);
                if (adNetwork != null)
                    adNetworksList[(AdNetworkType)failOverRewardedVideo] = adNetwork;
            }

        }
    }

    void inititalizeAdNetworks()
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
                string uniqueDeviceID = SystemInfo.deviceUniqueIdentifier;
                foreach (DictionaryEntry entry in adNetworksList)
                {
                    AdNetwork adNetwork = (AdNetwork)entry.Value;
                    if (!hideAds || adNetwork.isRewardedAd)
                    {
                        CALogManager.Instance.Log("Initializing ad " + adNetwork.type);
                        adNetwork.initialize(gameObjectName, uniqueDeviceID);
                    }
                }
#endif
    }

    private void initializeAnalytics()
    {
        //setup variables
        if (GoogleAnalytics)
        {
            CALogManager.Instance.Log("Initializing Analytics GOOGLE");

            googleAnalytics.otherTrackingCode = gpAnalyticsTrackingCode;
            googleAnalytics.androidTrackingCode = gpAnalyticsTrackingCode;
            googleAnalytics.IOSTrackingCode = asAnalyticsTrackingCode;
            googleAnalytics.productName = productName;
            googleAnalytics.bundleVersion = bundleVersion;
            googleAnalytics.bundleIdentifier = bundleIdentifier;
            googleAnalytics.StartSession();
        }
        if (FlurryAnalytics)
        {
            CALogManager.Instance.Log("Initializing Analytics FLURRY");

#if UNITY_ANDROID
            FlurryAndroid.SetLogEnabled(true);
#elif UNITY_IPOHNE
		                                        FlurryIOS.SetDebugLogEnabled(true);
#endif
            //initialize
            Flurry.Instance.StartSession(asFlurryAppKey, gpFlurryAppKey);
        }
        if (FirebaseAnalytics)
        {
            firebase = CAFirebase.Instance;
            firebase.initialize(ShowLog);
        }

    }
    public void setAdNetworkQueueType(AdNetworkQueueType type)
    {
        adNetworkQueueType = type;
    }

    public bool IsInterstitialAvailable(int index)
    {
        if (initialized && sceneList.Length - 1 >= index)
        {
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[currentScene.interstitialAndVideo.Count];
                AdNetworkTypeInterstitialAndVideo failOver = currentScene.interstitialAndVideo.failOver;
                if (currentAdNetwork == AdNetworkTypeInterstitialAndVideo.EMPTY)
                {
                    currentScene.interstitialAndVideo.Count = (currentScene.interstitialAndVideo.Count + 1) % currentScene.interstitialAndVideo.networkList.Length;
                    return false;
                }
                AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                if (adNetwork.IsAdAvailable(currentSceneID))
                {
                    return true;
                }
                else
                {
                    AdNetwork failOverAd = (AdNetwork)adNetworksList[failOver];
                    return failOverAd.IsAdAvailable(currentSceneID);
                }
            }
            else
            {
                for (int i = 0; i < currentScene.interstitialAndVideo.networkList.Length; i++)
                {
                    AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[i];
                    if (currentAdNetwork == AdNetworkTypeInterstitialAndVideo.EMPTY)
                    {
                        return false;
                    }
                    AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                    if (adNetwork.IsAdAvailable(currentSceneID))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool IsRewardedVideoAvailable(int index)
    {
        if (initialized && sceneList.Length - 1 >= index)
        {
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[currentScene.rewardedVideo.Count];
                AdNetworkTypeRewardedVideo failOver = currentScene.rewardedVideo.failOver;
                if (currentAdNetwork == AdNetworkTypeRewardedVideo.EMPTY)
                {
                    currentScene.rewardedVideo.Count = (currentScene.rewardedVideo.Count + 1) % currentScene.rewardedVideo.networkList.Length;
                    return false;
                }
				AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];
                if (adNetwork.IsAdAvailable(currentSceneID))
                {
                    return true;
                }
                else
                {
					AdNetwork failOverAd = (AdNetwork)adNetworksList[(AdNetworkType)failOver];
					if (failOver == AdNetworkTypeRewardedVideo.EMPTY)
						return false;
                    return failOverAd.IsAdAvailable(currentSceneID);
                }
            }
            else
            {
                for (int i = 0; i < currentScene.rewardedVideo.networkList.Length; i++)
                {
                    AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[i];
                    if (currentAdNetwork == AdNetworkTypeRewardedVideo.EMPTY)
                    {
                        return false;
                    }
					AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];
                    if (adNetwork.IsAdAvailable(currentSceneID))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void LoadInterstitialForScene(int index)
    {
        if (initialized && sceneList.Length - 1 >= index)
        {
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[currentScene.interstitialAndVideo.Count];
                if (currentAdNetwork == AdNetworkTypeInterstitialAndVideo.EMPTY)
                {
                    return;
                }
                AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                adNetwork.LoadAdForScene(currentSceneID);
            }
            else
            {
                for (int i = 0; i < currentScene.interstitialAndVideo.networkList.Length; i++)
                {
                    AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[i];
                    if (currentAdNetwork == AdNetworkTypeInterstitialAndVideo.EMPTY)
                    {
                        continue;
                    }
                    AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                    adNetwork.LoadAdForScene(currentSceneID);

                }
            }
        }
    }
    public void LoadRewardedVideoForScene(int index)
    {
        if (initialized && sceneList.Length - 1 >= index)
        {
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[currentScene.rewardedVideo.Count];
                if (currentAdNetwork == AdNetworkTypeRewardedVideo.EMPTY)
                {
                    return;
                }
                AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                adNetwork.LoadAdForScene(currentSceneID);
            }
            else
            {
                for (int i = 0; i < currentScene.interstitialAndVideo.networkList.Length; i++)
                {
                    AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[i];
                    if (currentAdNetwork == AdNetworkTypeRewardedVideo.EMPTY)
                    {
                        continue;
                    }
                    AdNetwork adNetwork = (AdNetwork)adNetworksList[currentAdNetwork];
                    adNetwork.LoadAdForScene(currentSceneID);

                }
            }
        }
    }
    public void ShowInterstitial(int index)
    {

#if (UNITY_ANDROID || UNITY_IPHONE)// && !UNITY_EDITOR
        if (!initialized)
        {
            cacheInterstitialRequest.Add(index);
            return;
        }
        if (sceneList.Length - 1 >= index)
        {
            //currentSeqNum = sequenceId;
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            CALogManager.Instance.Log("Showing Scene " + currentScene.sceneType);
            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                showInterstitialWithRoundRobin(currentScene);
            }
            else
            {
                showInterstitialWithPriority(currentScene);
            }
        }
        else
        {

        }
#endif
    }
    public void ShowRewardedVideo(int index)
    {

#if (UNITY_ANDROID || UNITY_IPHONE)// && !UNITY_EDITOR
        if (!initialized)
        {
            cacheRewardedRequest.Add(index);
            return;
        }
        if (sceneList.Length - 1 >= index)
        {
            //currentSeqNum = sequenceId;
            CAScene currentScene = sceneList[index];
            currentSceneID = (int)currentScene.sceneType;
            CALogManager.Instance.Log("Showing Scene " + currentScene.sceneType);

            if (adNetworkQueueType.Equals(AdNetworkQueueType.RoundRobin))
            {
                showRewardedVideoWithRoundRobin(currentScene);
            }
            else
            {
                showRewardedVideoWithPriority(currentScene);
            }
        }
        else
        {

        }
#endif
    }
    private void showInterstitialWithRoundRobin(CAScene currentScene)
    {
        AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[currentScene.interstitialAndVideo.Count];
        if (currentAdNetwork.Equals(AdNetworkTypeInterstitialAndVideo.EMPTY))
        {
            currentScene.interstitialAndVideo.Count = (currentScene.interstitialAndVideo.Count + 1) % currentScene.interstitialAndVideo.networkList.Length;
            return;
        }
        AdNetworkTypeInterstitialAndVideo failOver = currentScene.interstitialAndVideo.failOver;
        if (!currentScene.interstitialAndVideo.skipFirst || !currentScene.interstitialAndVideo.IsFirst)
        {
            currentScene.interstitialAndVideo.Count = (currentScene.interstitialAndVideo.Count + 1) % currentScene.interstitialAndVideo.networkList.Length;

            AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];

            if (!showAd(adNetwork, currentSceneID))
            {
                adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)failOver];
				if (failOver == AdNetworkTypeInterstitialAndVideo.EMPTY)
					return;
                showAd(adNetwork, currentSceneID);
            }
        }
    }
    private void showInterstitialWithPriority(CAScene currentScene)
    {
        for (int i = 0; i < currentScene.interstitialAndVideo.networkList.Length; i++)
        {
            if (!currentScene.interstitialAndVideo.skipFirst || !currentScene.interstitialAndVideo.IsFirst)
            {
                AdNetworkTypeInterstitialAndVideo currentAdNetwork = currentScene.interstitialAndVideo.networkList[i];
                if (currentAdNetwork.Equals(AdNetworkTypeInterstitialAndVideo.EMPTY))
                {
                    continue;
                }
                AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];
                if (showAd(adNetwork, currentSceneID))
                {
                    break;
                }
            }
        }
    }
    private void showRewardedVideoWithRoundRobin(CAScene currentScene)
    {
        AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[currentScene.rewardedVideo.Count];
        if (currentAdNetwork.Equals(AdNetworkTypeRewardedVideo.EMPTY))
        {
            currentScene.rewardedVideo.Count = (currentScene.rewardedVideo.Count + 1) % currentScene.rewardedVideo.networkList.Length;
            return;
        }
        AdNetworkTypeRewardedVideo failOver = currentScene.rewardedVideo.failOver;
        if (!currentScene.rewardedVideo.skipFirst || !currentScene.rewardedVideo.IsFirst)
        {
            currentScene.rewardedVideo.Count = (currentScene.rewardedVideo.Count + 1) % currentScene.rewardedVideo.networkList.Length;


            AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];

            if (!showAd(adNetwork, currentSceneID))
            {
                adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)failOver];
				if (failOver == AdNetworkTypeRewardedVideo.EMPTY)
					return;
                showAd(adNetwork, currentSceneID);
            }
        }
    }
    private void showRewardedVideoWithPriority(CAScene currentScene)
    {
        for (int i = 0; i < currentScene.rewardedVideo.networkList.Length; i++)
        {
            if (!currentScene.rewardedVideo.skipFirst || !currentScene.rewardedVideo.IsFirst)
            {
                AdNetworkTypeRewardedVideo currentAdNetwork = currentScene.rewardedVideo.networkList[i];
                if (currentAdNetwork.Equals(AdNetworkTypeRewardedVideo.EMPTY))
                {
                    continue;
                }
                AdNetwork adNetwork = (AdNetwork)adNetworksList[(AdNetworkType)currentAdNetwork];
                if (showAd(adNetwork, currentSceneID))
                {
                    break;
                }
            }
        }
    }
    private bool showAd(AdNetwork adNetwork, int sceneID)
    {
        CALogManager.Instance.Log("Showing ad " + adNetwork.type);

        try
        {
            if (adNetwork.hasRequestCallback)
            {
                if (adNetwork.requestCacheQueue.Count > 0)
                {
                    //queue is not empty
                    String status = (String)adNetwork.requestCacheQueue.Dequeue();
                    AdNetwork.SaveQueueRequest(adNetwork.type, sceneID);

                    if (status.Contains(CAConstants.REQUEEST_LOADED))
                    {

                        adNetwork.AddSceneToQueue(sceneID);

                        if (adNetwork.showAd(currentSceneID))
                        {
							CALogManager.Instance.Log("showAd: Ad shown " + adNetwork.type);

                            return true;
                        }
                        else
                        {
                            CALogManager.Instance.Log("Ad unable to show " + adNetwork.type);

                            //ad not shown
                            adNetwork.sceneQueue.Dequeue();

                        }
                    }
                }
                else
                {
                    adNetwork.requestAd();
                }

            }
            else
            {
                adNetwork.AddSceneToQueue(sceneID);
                //check net availablility
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    AdNetwork.SaveQueueRequest(adNetwork.type, sceneID);
                }

                if (adNetwork.showAd(currentSceneID))
                {
					CALogManager.Instance.Log("showAd: Ad shown " + adNetwork.type);

                    return true;
                }
                else
                {
                    //ad not shown
                    if (adNetwork.sceneQueue.Count > 0)
                    {
                        CALogManager.Instance.Log("Ad unable to show " + adNetwork.type);

                        int scene = (int)adNetwork.sceneQueue.Dequeue();
                    }

                }
            }
        }
        catch (System.Exception ex)
        {
            CALogManager.Instance.LogError("Show ad Exception: " + ex.Message + " " + ex.StackTrace);
        }


        return false;
    }
    public AdNetwork getFromAdNetworkList(AdNetworkType type)
    {
        if (adNetworksList.ContainsKey(type))
        {
            return (AdNetwork)adNetworksList[type];
        }
        return null;
    }
    public void hideAllAds()
    {
        CALogManager.Instance.Log("Hiding all ads will never show again");

        //this prefs value need to be 1
        PlayerPrefs.SetInt("consoliads_hide_all_ads", 1);
    }
    public void LogScreen(string title)
    {
        CALogManager.Instance.Log("Logging Screen");

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        if (GoogleAnalytics)
        {
            if (googleAnalytics)
            {
                googleAnalytics.LogScreen(GetStore() + ": " + title);
            }
        }
        if (FlurryAnalytics)
        {
            Flurry.Instance.LogEvent(GetStore() + ": " + title);
            //            Flurry.Instance.LogEvent("screen", new Dictionary<string, string> {{ "platform", store }, { "screen", title }});

        }
#endif
    }

    public void LogEvent(string category, string label, string action, long value)
    {
        CALogManager.Instance.Log("Logging event");
#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
        if (GoogleAnalytics)
        {
            if (googleAnalytics)
            {
                googleAnalytics.LogEvent(category, label, action, value);
            }

        }
        if (FlurryAnalytics)
        {
            Flurry.Instance.LogEvent("event", new Dictionary<string, string> { { "platform", GetStore() }, { "category", category }, { "label", label }, { "value", value.ToString() } });
        }
#endif
    }
    private string GetStore()
    {
        switch (platform)
        {
            case Platform.Amazon:
                return "amazon";
            case Platform.Google:
                return "google";
            case Platform.Apple:
                return "apple";
            default:
                return null;
        }

    }

    public string MoreFunURL()
    {
        return adNetworkFactory.moreFunURL;
    }
    public string SupportEmail()
    {
        return supportEmail;
    }
    public string RateUsURL()
    {
        return adNetworkFactory.rateUsURL;
    }
    public WWW postAppJson(string url, string json)
    {
        // Create the WWW object and provide the url of this web request.
        WWWForm form = new WWWForm();
        form.AddField("appJson", json);


        WWW www = new WWW(url, form);

        // Run the web call in the background.
        StartCoroutine(WaitForRequest(www));

        // Do nothing until the response is complete.
        while (!www.isDone) { }

        // Deliver the result to the method that called this one.
        return www;
    }
    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
    }
    private void saveQueueImpression(AdNetworkType type)
    {
        if (adNetworksList.ContainsKey(type))
        {
            Debug.Log("saveQueueImpression: AD LIST CONTAINS THIS NETWORK");
        }
        AdNetwork network = (AdNetwork)adNetworksList[type];
        int sceneID;
        if (network.hasClickCallback)
        {
            sceneID = (int)network.sceneQueue.Peek();

        }
        else
        {
            sceneID = (int)network.sceneQueue.Dequeue();

        }
        AdNetwork.SaveQueueImpression(type, sceneID);
    }
    private void saveQueueClick(AdNetworkType type)
    {
        AdNetwork network = (AdNetwork)adNetworksList[type];
        int sceneID = (int)network.sceneQueue.Dequeue();
        AdNetwork.SaveQueueClick(type, sceneID);
    }

    public void EnableLog(bool value)
    {
        ShowLog = value;
        CALogManager.Instance.EnableLog(value);
    }

    #region Consoliads Event Methods
    //triggers when an Ad is requested successfully
    public void onAdRequested(AdNetworkType type)
    {
        if (adNetworksList.ContainsKey(type))
        {
            Debug.Log("onAdRequested: AD LIST CONTAINS THIS NETWORK");
        }
        CALogManager.Instance.Log("Ad Loaded " + type);

        AdNetwork network = (AdNetwork)adNetworksList[type];
        network.requestCacheQueue.Enqueue(CAConstants.REQUEEST_LOADED);

    }
    //triggers when an Ad requested fails
    public void onAdRequestFailed(AdNetworkType type)
    {
        if (adNetworksList.ContainsKey(type))
        {
            Debug.Log("onAdRequestFailed: AD LIST CONTAINS THIS NETWORK");
        }
        CALogManager.Instance.LogWarrning("Ad failed to Load " + type);

        AdNetwork network = (AdNetwork)adNetworksList[type];
        network.requestCacheQueue.Enqueue(CAConstants.REQUEEST_FAILED);

    }
    public void onInterstitialAdShown(AdNetworkType type)
    {

		CALogManager.Instance.Log("InterstitialAd shown " + type);

        saveQueueImpression(type);

        if (onInterstitialAdShownEvent != null)
            onInterstitialAdShownEvent();

    }
    public void onVideoAdShown(AdNetworkType type)
    {
		CALogManager.Instance.Log("VideoAd shown " + type);

        saveQueueImpression(type);

        if (onVideoAdShownEvent != null)
            onVideoAdShownEvent();
    }
    public void onRewardedVideoAdShown(AdNetworkType type)
    {
        CALogManager.Instance.Log("Rewarded video Ad shown " + type);


        saveQueueImpression(type);

        if (onRewardedVideoAdShownEvent != null)
            onRewardedVideoAdShownEvent();
    }
    public void onRewardedVideoAdCompleted(AdNetworkType type)
    {
        CALogManager.Instance.Log("Rewarded video Ad completed " + type);

        if (onRewardedVideoAdCompletedEvent != null)
            onRewardedVideoAdCompletedEvent();
    }
    public void onPopupAdShown(AdNetworkType type)
    {
		CALogManager.Instance.Log("PopupAd shown " + type);

        saveQueueImpression(type);


        if (onPopupAdShownEvent != null)
            onPopupAdShownEvent();
    }
    public void onAdClick(AdNetworkType type)
    {
        CALogManager.Instance.Log("Ad clicked " + type);

        saveQueueClick(type);

    }
    public void onRewardedVideoAdClicked(AdNetworkType type)
    {
        CALogManager.Instance.Log("Rewarded video ad clicked " + type);

        saveQueueClick(type);

        if (onRewardedVideoAdClickEvent != null)
            onRewardedVideoAdClickEvent();
    }
    public void onAdClosed(AdNetworkType type)
    {
        CALogManager.Instance.Log("Ad closed " + type);

        AdNetwork adNetwork = (AdNetwork)adNetworksList[type];
        if (adNetwork.sceneQueue.Count > 0)
        {
            adNetwork.sceneQueue.Dequeue();
        }
    }

    public void onAdShowFailed(AdNetworkType type)
    {
        CALogManager.Instance.LogWarrning("Ad failed to show " + type);


        AdNetwork adNetwork = (AdNetwork)adNetworksList[type];
        if (adNetwork.sceneQueue.Count > 0)
        {
            adNetwork.sceneQueue.Dequeue();
        }


    }
    #endregion

    #region admmob related methods

    public void LoadNativeAd(int sceneIndex)
    {
        if (!initialized)
        {
            loadAdmobNativeAdAfterInit = true;
            return;
        }
        if (!hideAds)
        {
            if (admobNativeAd == null)
            {
                admobNativeAd = new CAAdmobNativeAd();
            }
            if (sceneList.Length - 1 >= sceneIndex)
            {
                CANativeAdSettings adSettings = sceneList[sceneIndex].native;
                if (adSettings.enabled)
                {
                    admobNativeAd.LoadAd(adNetworkFactory.admobNativeAdID, adSettings.width, adSettings.height, adSettings.position);
                }
            }
        }
    }
    public void HideNativeAd()
    {
        if (admobNativeAd != null)
        {
            admobNativeAd.Hide();
        }
    }
    public void ShowNativeAd()
    {
        if (admobNativeAd != null)
        {
            admobNativeAd.Show();
        }
    }
    public void ShowBanner(int sceneIndex)
    {

        if (!initialized)
        {
            showBanner = true;
            showBannerIndex = sceneIndex;
            return;
        }
        if (!hideAds && sceneList.Length - 1 >= sceneIndex && sceneList[sceneIndex].banner.enabled)
        {
            if (mAdmob.bannerView != null)
            {
                mAdmob.bannerView.Destroy();
            }


            initializeAdmobBanner(sceneList[sceneIndex]);
            if (mAdmob != null && mAdmob.bannerView != null)
            {
                mAdmob.bannerView.Show();
            }
        }

    }

    public void HideBanner()
    {
#if (UNITY_ANDROID || UNITY_IPHONE) // && !UNITY_EDITOR
        showBanner = false;
        if (mAdmob != null && mAdmob.bannerView != null)
        {
            CALogManager.Instance.Log("Hiding Admob Banner");
            mAdmob.bannerView.Hide();
        }
#endif
    }
    public void initializeAdmobBanner(CAScene scene)
    {
#if (UNITY_ANDROID || UNITY_IPHONE)
        //if (!initAdmobBanner)
        //{

        switch (scene.banner.size)
        {
            case AdmobBannerSize.Banner:
                mAdmob.RequestBanner(adNetworkFactory.admobBannerID, AdSize.Banner, scene.banner.position);
                break;
            case AdmobBannerSize.IABBanner:
                mAdmob.RequestBanner(adNetworkFactory.admobBannerID, AdSize.IABBanner, scene.banner.position);
                break;
            case AdmobBannerSize.Leaderboard:
                mAdmob.RequestBanner(adNetworkFactory.admobBannerID, AdSize.Leaderboard, scene.banner.position);
                break;
            case AdmobBannerSize.MediumRectangle:
                mAdmob.RequestBanner(adNetworkFactory.admobBannerID, AdSize.MediumRectangle, scene.banner.position);
                break;
            case AdmobBannerSize.SmartBanner:
                mAdmob.RequestBanner(adNetworkFactory.admobBannerID, AdSize.SmartBanner, scene.banner.position);
                break;
        }
        initAdmobBanner = true;
        //}
#endif
    }
    #endregion

    #region AppLovin Ad event listener
    void onAppLovinEventReceived(string ev)
    {

        if (ev.Contains("DISPLAYEDINTER"))
        {
            // An ad was shown.  Pause the game.
            onInterstitialAdShown(AdNetworkType.APPLOVININTERSTITIAL);
        }
        else if (ev.Contains("HIDDENINTER"))
        {
            // Ad ad was closed.  Resume the game.
            // If you're using PreloadInterstitial/HasPreloadedInterstitial, make a preload call here.
            AppLovin.PreloadInterstitial();
        }
        else if (ev.Contains("LOADEDINTER"))
        {
            // An interstitial ad was successfully loaded.
            onAdRequested(AdNetworkType.APPLOVININTERSTITIAL);

        }
        else if (string.Equals(ev, "LOADINTERFAILED"))
        {
            // An interstitial ad failed to load.
            onAdRequestFailed(AdNetworkType.APPLOVININTERSTITIAL);

        }
        if (ev.Contains("REWARDAPPROVEDINFO"))
        {

            // The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
            char delimeter = '|';

            // Split the string based on the delimeter
            string[] split = ev.Split(delimeter);

            // Pull out the currency amount
            double amount = double.Parse(split[1]);

            // Pull out the currency name
            string currencyName = split[2];

            // Do something with the values from above.  For example, grant the coins to the user.
            //updateBalance(amount, currencyName);
        }
        else if (ev.Contains("LOADEDREWARDED"))
        {
            // A rewarded video was successfully loaded.
            onAdRequested(AdNetworkType.APPLOVINREWARDEDVIDEO);

        }
        else if (ev.Contains("DISPLAYEDREWARDED"))
        {
            // A rewarded video was successfully displayed.
            onRewardedVideoAdShown(AdNetworkType.APPLOVINREWARDEDVIDEO);
        }
        else if (ev.Contains("LOADREWARDEDFAILED"))
        {
            // A rewarded video failed to load.
            onAdRequestFailed(AdNetworkType.APPLOVINREWARDEDVIDEO);

        }
        else if (ev.Contains("HIDDENREWARDED"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            onRewardedVideoAdCompleted(AdNetworkType.APPLOVINREWARDEDVIDEO);
            AppLovin.LoadRewardedInterstitial();
        }
    }
    #endregion

    #region IRevMobListener implementation
    public void SessionIsStarted()
    {
        if (adNetworksList.ContainsKey(AdNetworkType.REVMOBFULLSCREEN))
        {
            CARevmobAdManager.Instance.createFullScreen();
        }
        if (adNetworksList.ContainsKey(AdNetworkType.REVMOBVIDEO))
        {
            CARevmobAdManager.Instance.createVideo();
        }
        if (adNetworksList.ContainsKey(AdNetworkType.REVMOBREWARDEDVIDEO))
        {
            CARevmobAdManager.Instance.createRewardedVideo();
        }
    }

    public void SessionNotStarted(string message)
    {
    }

    public void AdDidReceive(string revMobAdType)
    {
        switch (revMobAdType)
        {
            case "Fullscreen":
                onAdRequested(AdNetworkType.REVMOBFULLSCREEN);
                break;
            case "Video":
                break;
            case "RewardedVideo":
                break;
        }
    }

    public void AdDidFail(string revMobAdType)
    {
        switch (revMobAdType)
        {
            case "Fullscreen":
                onAdRequestFailed(AdNetworkType.REVMOBFULLSCREEN);
                break;
            case "Video":
                break;
            case "RewardedVideo":
                break;
        }
    }

    public void AdDisplayed(string revMobAdType)
    {
        switch (revMobAdType)
        {
            case "Fullscreen":
                onInterstitialAdShown(AdNetworkType.REVMOBFULLSCREEN);
                break;
            case "Video":
                onVideoAdShown(AdNetworkType.REVMOBVIDEO);
                break;
            case "RewardedVideo":
                onRewardedVideoAdShown(AdNetworkType.REVMOBREWARDEDVIDEO);
                break;
        }
    }

    public void UserClickedInTheAd(string revMobAdType)
    {
        switch (revMobAdType)
        {
            case "Fullscreen":
                onAdClick(AdNetworkType.REVMOBFULLSCREEN);
                break;
            case "Video":
                onAdClick(AdNetworkType.REVMOBVIDEO);
                break;
            case "RewardedVideo":
                onAdClick(AdNetworkType.REVMOBREWARDEDVIDEO);
                break;
        }
    }

    public void UserClosedTheAd(string revMobAdType)
    {
        switch (revMobAdType)
        {
            case "Fullscreen":
                onAdClosed(AdNetworkType.REVMOBFULLSCREEN);
                break;
            case "Video":
                onAdClosed(AdNetworkType.REVMOBVIDEO);
                break;
            case "RewardedVideo":
                onAdClosed(AdNetworkType.REVMOBREWARDEDVIDEO);
                break;
        }
    }

    public void VideoLoaded()
    {
        onAdRequested(AdNetworkType.REVMOBVIDEO);
    }
    public void VideoNotCompletelyLoaded()
    {
        onAdRequestFailed(AdNetworkType.REVMOBVIDEO);
    }
    public void VideoStarted()
    {
    }
    public void VideoFinished()
    {
    }

    public void RewardedVideoLoaded()
    {
        onAdRequested(AdNetworkType.REVMOBREWARDEDVIDEO);
    }
    public void RewardedVideoNotCompletelyLoaded()
    {
        onAdRequestFailed(AdNetworkType.REVMOBREWARDEDVIDEO);

    }
    public void RewardedVideoStarted()
    {
    }
    public void RewardedVideoFinished()
    {
    }
    public void RewardedVideoCompleted()
    {
        onRewardedVideoAdCompleted(AdNetworkType.REVMOBREWARDEDVIDEO);

    }
    public void RewardedPreRollDisplayed()
    {
    }


    public void InstallDidReceive(string message) { }

    public void InstallDidFail(string message) { }

    public void EulaIsShown()
    {
    }

    public void EulaAccepted()
    {
    }

    public void EulaRejected()
    {
    }

    #endregion

    #region Consoliads Ad Listeners
    public void didCacheInterstitial(String location)
    {

    }

    // Called after an interstitial has attempted to load from the consoliads API
    // servers but failed.
    public void didFailToLoadInterstitialEvent(String location)
    {

    }

    // Called after an interstitial has been displayed on the screen.
    public void didDisplayInterstitialEvent(String location)
    {
        onInterstitialAdShown(AdNetworkType.CONSOLIADS);
    }
    // Called after an interstitial has been closed.
    public void didCloseInterstitialEvent(String location)
    {
        onAdClosed(AdNetworkType.CONSOLIADS);

    }
    // Called after an interstitial has been clicked.
    public void didClickInterstitialEvent(String location)
    {
        onAdClick(AdNetworkType.CONSOLIADS);
    }
    // Called after an Error in consoliads sdk.
    public void onAppItUpErrorEvent(String error)
    {
    }
    #endregion
}
