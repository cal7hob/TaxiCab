using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class CAFirebase
{
#if UNITY_ANDROID
    private AndroidJavaObject activityContext = null;
    private AndroidJavaObject _plugin = null;
#endif
    public bool initialized = false;
    private static CAFirebase _instance;

    private CAFirebase() { }

	#if UNITY_IPHONE
	[DllImport("__Internal")]
	public static extern void _initializeWithLog(bool enableLog);
    [DllImport("__Internal")]
	public static extern void _logEventName(string eventName, string action, string sceneType);
    [DllImport("__Internal")]
	public static extern void _logSelectContentWithItemID(string itemID, string contentType);
    [DllImport("__Internal")]
	public static extern void _logJoinGroupID(string groupID);
    [DllImport("__Internal")]
	public static extern void _logLevelUPWithCharacter(string character, long level);
    [DllImport("__Internal")]
    public static extern void _logPostScore(long score);
    [DllImport("__Internal")]
    public static extern void _logPostScoreWithLevel(long Score, long level);
    [DllImport("__Internal")]
    public static extern void _logPostScoreWithCharacter(long Score, long level, string character);
    [DllImport("__Internal")]
    public static extern void _logSpendVirtualCurrencyName(string virtualCurrency, string itemName, long value);
    [DllImport("__Internal")]
	public static extern void _logTutorialBegin();
    [DllImport("__Internal")]
	public static extern void _logTutorialComplete();
    [DllImport("__Internal")]
	public static extern void _logUnlockAchievementWithID(string achievementID);

	#endif


    public static CAFirebase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CAFirebase();
            }
            return _instance;
        }
    }

    public void initialize(bool enableLog)
    {
#if UNITY_ANDROID

        //getting current activity context to be passed into the android sdk
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        // find the plugin instance
        using (var pluginClass = new AndroidJavaClass("com.consoliads.firebase.FirebaseUnityPlugin"))
            _plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
        AndroidJavaObject jLogEnable = new AndroidJavaObject("java.lang.Boolean", enableLog);

        initialized = _plugin.Call<bool>("initialize", new object[] { activityContext, jLogEnable });

#elif UNITY_IPHONE 
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			_initializeWithLog(enableLog);
			initialized = true;
		}
#endif
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogEvent(string eventName, string action, string sceneType)
    {
#if UNITY_ANDROID

        _plugin.Call("LogEvent", new object[] { eventName, action, sceneType });

#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logEventName(eventName, action, sceneType);
#endif

    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectContent(string itemID, string contentType)
    {
#if UNITY_ANDROID

        _plugin.Call("LogSelectContent", new object[] { itemID, contentType });

#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logSelectContentWithItemID(itemID, contentType);
#endif
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void JoinGroup(string groupID)
    {
#if UNITY_ANDROID

        _plugin.Call("LogJoinGroup", new object[] { groupID });

#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logJoinGroupID(groupID);
#endif
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LevelUp(string character, long level)
    {
#if UNITY_ANDROID

        AndroidJavaObject jLevel = new AndroidJavaObject("java.lang.Long", level);
        _plugin.Call("LogLevelUp", new object[] { character, jLevel });
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logLevelUPWithCharacter(character, level);
#endif

    }

    // ------------------------------------------------------------------------------------------------------------------
	public void PostScore(long score)
    {
#if UNITY_ANDROID

        AndroidJavaObject jScore = new AndroidJavaObject("java.lang.Long", score);

        _plugin.Call("LogPostScore", new object[] { jScore });
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logPostScore(score);
#endif

    }
    // ------------------------------------------------------------------------------------------------------------------
	public void PostScore(long score, long level)
    {
#if UNITY_ANDROID

        AndroidJavaObject jScore = new AndroidJavaObject("java.lang.Long", score);
        AndroidJavaObject jLevel = new AndroidJavaObject("java.lang.Long", level);

        _plugin.Call("LogPostScore", new object[] { jScore, jLevel });
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logPostScoreWithLevel(score, level);
#endif

    }
    // ------------------------------------------------------------------------------------------------------------------
    public void PostScore(long score, long level, string character)
    {
#if UNITY_ANDROID

        AndroidJavaObject jScore = new AndroidJavaObject("java.lang.Long", score);
        AndroidJavaObject jLevel = new AndroidJavaObject("java.lang.Long", level);

        _plugin.Call("LogPostScore", new object[] { jScore, jLevel, character });
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logPostScoreWithCharacter(score, level, character);
#endif

    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SpendVirtualCurrency(string itemName, string virtualCurrencyName, long value)
    {
#if UNITY_ANDROID

        AndroidJavaObject jValue = new AndroidJavaObject("java.lang.Long", value);

        _plugin.Call("LogSpendVirtualCurrency", new object[] { itemName, virtualCurrencyName, jValue });
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logSpendVirtualCurrencyName(virtualCurrencyName, itemName, value );
#endif

    }

    // ------------------------------------------------------------------------------------------------------------------
    public void TutorialBegin()
    {
#if UNITY_ANDROID

        _plugin.Call("LogTutorialBegin");
#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logTutorialBegin();
#endif

    }

    // ------------------------------------------------------------------------------------------------------------------
    public void TutorialComplete()
    {
#if UNITY_ANDROID

        _plugin.Call("LogTutorialComplete");

#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logTutorialComplete();
#endif

    }
    // ------------------------------------------------------------------------------------------------------------------

    public void UnlockAchievement(string achievementID)
    {
#if UNITY_ANDROID

        _plugin.Call("LogUnlockAchievement", new object[] { achievementID });

#elif UNITY_IPHONE  
        if (Application.platform == RuntimePlatform.IPhonePlayer)
			_logUnlockAchievementWithID(achievementID);
#endif

    }



}
