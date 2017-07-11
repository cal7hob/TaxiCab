using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class CAWrapper : MonoBehaviour
{
#if UNITY_ANDROID
    private AndroidJavaObject toastExample = null;
    private static AndroidJavaObject activityContext = null;
    private static AndroidJavaObject _plugin = null;
#endif

	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern bool _initAppWithKey (string appKey, string deviceID, string gameObjectName);

	[DllImport("__Internal")]
	private static extern void _showInterstitial (int scene);

	[DllImport("__Internal")]
	private static extern void _cacheInterstitial (int scene);

	[DllImport("__Internal")]
	private static extern void _loadInterstitialForScene (int scene);

	[DllImport("__Internal")]
	private static extern bool _hasInterstitial (int scene);

	[DllImport("__Internal")]
	private static extern bool _hasInterstitialForScene (int scene);

	[DllImport("__Internal")]
	private static extern void _sendStatsOnPauseWithDeviceID (string deviceID);

	#endif

    public static bool initialized = false;

    public void callBackButtonHandler(string message)
    {
    }
    public static void initialize(string appKey)
    {

    }
    public static void init(string appKey, string gameObjectName, string deviceID)
    {
#if UNITY_ANDROID
        //getting current activity context to be passed into the android sdk
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        // find the plugin instance
        using (var pluginClass = new AndroidJavaClass("com.appitup.sdk.AppItUpUnityPlugin"))
            _plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
        initialized = _plugin.Call<bool>("init", appKey, deviceID, activityContext, gameObjectName);
        //initialized = true;
#elif UNITY_IPHONE 
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			initialized = _initAppWithKey(appKey,deviceID,gameObjectName);
		}
#endif
    }
    /// Loads an interstitial. Location is optional.
    public static bool showInterstitial(int sceneID)
    {
#if UNITY_ANDROID
        if (checkInitialized() && hasInterstitialForScene(sceneID))
        {
            _plugin.Call("showInterstitial", sceneID);
            return true;
        }
        return false;
#elif UNITY_IPHONE
		if (checkInitialized() && hasInterstitialForScene(sceneID))
		{
			_showInterstitial(sceneID);
			return true;
		}
		return false;
#else
		return false;
#endif
    }
    public static bool hasInterstitialForScene(int sceneID)
    {
#if UNITY_ANDROID
        return _plugin.Call<bool>("hasInterstitialForScene", sceneID);

#elif UNITY_IPHONE
		return _hasInterstitialForScene(sceneID);
#else
		return false;
#endif
    }

    public static void loadInterstitialForScene(int sceneID)
    {

#if UNITY_ANDROID
        _plugin.Call("loadInterstitialForScene", sceneID);

#elif UNITY_IPHONE
		_loadInterstitialForScene(sceneID);
#endif
    }

	public static void sendStatsOnPause(string deviceID)
	{
#if UNITY_ANDROID
		_plugin.Call("sendStatsOnPause",deviceID);

#elif UNITY_IPHONE
		_sendStatsOnPauseWithDeviceID(deviceID);
#endif
	}
    private static bool checkInitialized()
    {
        if (initialized)
        {
            return true;
        }
        else {
            return false;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

