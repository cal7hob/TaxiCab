using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CARevmobAdManager
{
    private RevMob revmob;
    private RevMobFullscreen fullscreen, video, rewardedVideo;
    private RevMobPopup popup;
    private RevMobLink link;
    private RevMobLink button;
    private RevMobBanner banner;
    private RevMobBanner loadedBanner;
    private static CARevmobAdManager _instance;
    //------------------------------------------------------------------------------
    private CARevmobAdManager() {}
    //------------------------------------------------------------------------------
    public static CARevmobAdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CARevmobAdManager();
            }
            return _instance;
        }
    }
    //------------------------------------------------------------------------------
    public void initialize(Dictionary<String, String> appIds, string gameObjectName)
    {
            revmob = RevMob.Start(appIds, gameObjectName);
    }
    //------------------------------------------------------------------------------
    public void createFullScreen()
    {
        fullscreen = revmob.CreateFullscreen();
    }
    //------------------------------------------------------------------------------
    public bool showFullScreen()
    {
        if (fullscreen != null)
        {
            fullscreen.Show();
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public bool IsFullScreenAvailable()
    {
        if (fullscreen != null)
        {
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public void hideFullScreen()
    {
        fullscreen.Hide();
    }
    //------------------------------------------------------------------------------
    public void createVideo()
    {
        video = revmob.CreateVideo();
    }
    //------------------------------------------------------------------------------
    public bool showVideo()
    {
        if (video != null)
        {
            video.ShowVideo();
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public bool IsVideoAvailable()
    {
        if (video != null)
        {
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public void createRewardedVideo()
    {
        rewardedVideo = revmob.CreateRewardedVideo();
    }
    //------------------------------------------------------------------------------
    public bool showRewardedVideo()
    {
        if (rewardedVideo != null)
        {
            rewardedVideo.ShowRewardedVideo();
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public bool IsRewardedVideoAvailable()
    {
        if (rewardedVideo != null)
        {
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public void createPopup()
    {
        //popup = revmob.CreatePopup(); no longer supported in v9.1.0
    }
    //------------------------------------------------------------------------------
    public bool showPopup()
    {
        if (popup != null)
        {
            popup.Show();
            popup = null;
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------
    public void createBanner()
    {
        #if UNITY_ANDROID || UNITY_IOS
                banner = revmob.CreateBanner();
        #endif
    }
    //------------------------------------------------------------------------------
    public void showBanner()
    {
        if(banner != null)
        {
            banner.Show();
        }
    }
}
