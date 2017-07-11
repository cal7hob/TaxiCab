using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using System.Collections;
using ChartboostSDK;

public class AdNetwork
{


    public AdNetworkType type { get; set; }
    public Boolean isRewardedAd = false;
    public bool hasClickCallback = false;
    public bool hasRequestCallback = false;
    public string appKey { get; set; }
    public Queue requestCacheQueue = new Queue();
    public Queue sceneQueue = new Queue();
    public virtual int getServerID()
    {
        return 0;
    }

    public virtual void initialize(string gameObjectName, string uniqueDeviceID) { }
    public virtual bool showAd(int sceneID) { return true; }
    public virtual bool IsAdAvailable(int sceneID) { return false; }
    public virtual void LoadAdForScene(int sceneID) {}
    public virtual void requestAd() {}
    protected CBLocation getCBLocation(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.MainMenu:
                return CBLocation.MainMenu;
            case SceneTypes.OnPause:
                return CBLocation.Pause;
            case SceneTypes.Gameplay:
                return CBLocation.GameScreen;
            case SceneTypes.AppExit:
                return CBLocation.Quit;
            default:
                return CBLocation.Default;
        }
    }
    public void AddSceneToQueue(int scene)
    {
        sceneQueue.Enqueue(scene);

    }

    public static void SaveQueueRequest(AdNetworkType adNetworkType, int sceneType)
    {
        int request = PlayerPrefs.GetInt("queue_Req_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, 0);
        PlayerPrefs.SetInt("queue_Req_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, (request + 1));

    }
    
    public static void SaveQueueImpression(AdNetworkType adNetworkType, int sceneType)
    {

        int impressions = PlayerPrefs.GetInt("queue_Imp_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, 0);
        PlayerPrefs.SetInt("queue_Imp_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, (impressions + 1));

    }
    
    public static void SaveQueueClick(AdNetworkType adNetworkType, int sceneType)
    {
        int click = PlayerPrefs.GetInt("queue_Click_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, 0);
        PlayerPrefs.SetInt("queue_Click_AdNetwokType_" + (int)adNetworkType + "_SceneType_" + sceneType, (click + 1));
    }

	public static JSONClass getQueueEventStats()
	{
		var strJson = new JSONClass();
		int eventCounter = 0, i = 0;
		foreach (var value in Enum.GetValues(typeof(AdNetworkType)))
		{
			AdNetwork network = ConsoliAds.Instance.getFromAdNetworkList((AdNetworkType)value);
			if (network != null && network.sceneQueue.Count.Equals(0))
			{
				strJson["networks"][i].AsInt = (int)value;
				i++;
				foreach (var sceneType in Enum.GetValues(typeof(SceneTypes)))
				{
					//getting events from prefs and storing in array
					int impressions = PlayerPrefs.GetInt("queue_Imp_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					int request = PlayerPrefs.GetInt("queue_Req_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					int click = PlayerPrefs.GetInt("queue_Click_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					if (impressions > 0 || request > 0 || click > 0)
					{
						strJson["eventStats"][eventCounter]["adID"].AsInt = (int)value;
						strJson["eventStats"][eventCounter]["sceneID"].AsInt = (int)sceneType;
						strJson["eventStats"][eventCounter]["request"].AsInt = request;
						strJson["eventStats"][eventCounter]["impression"].AsInt = impressions;
						strJson["eventStats"][eventCounter]["click"].AsInt = click;
						eventCounter++;
					}
				}
			}
		}
		return strJson;
	}
	public static JSONClass getQueueEventStatsAll()
	{
		var strJson = new JSONClass();
		int eventCounter = 0, i = 0;
		foreach (var value in Enum.GetValues(typeof(AdNetworkType)))
		{ 
				foreach (var sceneType in Enum.GetValues(typeof(SceneTypes)))
				{
					//getting events from prefs and storing in array
					int impressions = PlayerPrefs.GetInt("queue_Imp_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					int request = PlayerPrefs.GetInt("queue_Req_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					int click = PlayerPrefs.GetInt("queue_Click_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
					if (impressions > 0 || request > 0 || click > 0)
					{
						strJson["eventStats"][eventCounter]["adID"].AsInt = (int)value;
						strJson["eventStats"][eventCounter]["sceneID"].AsInt = (int)sceneType;
						strJson["eventStats"][eventCounter]["request"].AsInt = request;
						strJson["eventStats"][eventCounter]["impression"].AsInt = impressions;
						strJson["eventStats"][eventCounter]["click"].AsInt = click;
						eventCounter++;
					}
				}

		}
		return strJson;
	}
    public static void resetQueueEventStats()
    {

        foreach (var value in Enum.GetValues(typeof(AdNetworkType)))
        {
            foreach (var sceneType in Enum.GetValues(typeof(SceneTypes)))
            {
                PlayerPrefs.SetInt("queue_Imp_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
                PlayerPrefs.SetInt("queue_Req_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
                PlayerPrefs.SetInt("queue_Click_AdNetwokType_" + (int)value + "_SceneType_" + (int)sceneType, 0);
            }
        }
    }
    public static void resetQueueEventStats(JSONClass strJson)
    {

        for (int i = 0; i < strJson["eventStats"].Count; i++)
        {

            int impression = PlayerPrefs.GetInt("queue_Imp_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, 0);
            int request = PlayerPrefs.GetInt("queue_Req_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, 0);
            int click = PlayerPrefs.GetInt("queue_Click_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, 0);
            if (impression > 0)
            {

                PlayerPrefs.SetInt("queue_Imp_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, (impression - strJson["eventStats"][i]["impression"].AsInt));
            }
            if (request > 0)
            {

                PlayerPrefs.SetInt("queue_Req_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, (request - strJson["eventStats"][i]["request"].AsInt));
            }
            if (click > 0)
            {

                PlayerPrefs.SetInt("queue_Click_AdNetwokType_" + strJson["eventStats"][i]["adID"].AsInt + "_SceneType_" + strJson["eventStats"][i]["sceneID"].AsInt, (click - strJson["eventStats"][i]["click"].AsInt));
            }

        }
    }
  
}
