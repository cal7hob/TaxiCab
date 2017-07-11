using UnityEngine;
using System.Collections;

#if UNITY_EDITOR 
using UnityEditor;
using System.IO;

public class CAUninstallPlugin
{

    #region Constants

    private const string kUninstallAlertTitle = "Uninstall - ConsoliAds";
    private const string kUninstallAlertMessage = "Backup before doing this step to preserve changes done in this plugin. This deletes files only related to ConsoliAds plugin. Do you want to proceed?";

    private const string kAssets = "Assets";
    public const string kPluginsPath = "Assets/Plugins";
    public const string kAndroidPluginsPath = kPluginsPath + "/Android";
    public const string kIOSPluginsPath = kPluginsPath + "/iOS";

    private static string[] kPluginFolders = new string[]
        {
            kAndroidPluginsPath                     +   "/ChartboostSDK",
            kAndroidPluginsPath                     +   "/GoogleMobileAdsPlugin",
            kAndroidPluginsPath                     +   "/Heyzap",
            kAndroidPluginsPath                     +   "/Supersonic",
            kAndroidPluginsPath                     +   "/unityads",
            kAndroidPluginsPath                     +   "/google-play-services_lib",

            kPluginsPath                            +   "/AppLovin",
            kPluginsPath                            +   "/AppTrackerAndroid",
            kPluginsPath                            +   "/AppTrackerIOS",
            kPluginsPath                            +   "/FlurryAnalytics",
            kPluginsPath                            +   "/GoogleAnalyticsV4",
            kPluginsPath                            +   "/Heyzap",
            kPluginsPath                            +   "/AppLovin",

            kIOSPluginsPath                         +   "/Flurry",
            kIOSPluginsPath                         +   "/Supersonic",
            kIOSPluginsPath                         +   "/Heyzap",

            kPluginsPath                            +   "/GoogleMobileAds.framework",

            kIOSPluginsPath                         +   "/AppTracker.framework",
            kIOSPluginsPath                         +   "/AdColony.framework",
            kIOSPluginsPath                         +   "/UnityAds.bundle",
            kIOSPluginsPath                         +   "/UnityAds.framework",
            kIOSPluginsPath                         +   "/GoogleSymbolUtilities.framework"     ,
            kIOSPluginsPath                         +   "/GoogleUtilities.framework"           ,
            kIOSPluginsPath                         +   "/FirebaseAnalytics.framework"         ,
            kIOSPluginsPath                         +   "/FirebaseCore.framework"              ,
            kIOSPluginsPath                         +   "/FirebaseInstanceID.framework"        ,
            kIOSPluginsPath                         +   "/GoogleInterchangeUtilities.framework",
            kIOSPluginsPath                         +   "/GoogleMobileAds.framework"           ,

            kAssets                                 +   "/AppTracker",
            kAssets                                 +   "/Chartboost",
            kAssets                                 +   "/GoogleMobileAds",
            kAssets                                 +   "/Sample",
            kAssets                                 +   "/Supersonic",
            kAssets                                 +   "/Consoliads/Scripts",
            kAssets                                 +   "/Editor/Heyzap",
            kAssets                                 +   "/Standard Assets/UnityAds",
            kAssets                                 +   "/Standard Assets/Editor/UnityAds",
            kAssets                                 +   "/Editor/Heyzap",

            kAssets                                 +   "/Consoliads",
        };
    private static string[] kPluginFiles = new string[]
        {

            kAssets                                 +   "/Consoliads/ConsoliAds.prefab",
            kAssets                                 +   "/Consoliads/Editor/ConsoliAdsEditor.cs",

            kAssets                                 +   "/StreamingAssets/drawable-xhdpi.zip",
            kAssets                                 +   "/StreamingAssets/drawable-xxhdpi.zip",
            kAssets                                 +   "/StreamingAssets/resources.zip",
            kAssets                                 +   "/StreamingAssets/loading.html",
            kAssets                                 +   "/StreamingAssets/drawable.zip",
            kAssets                                 +   "/StreamingAssets/drawable-hdpi.zip",
            kAssets                                 +   "/StreamingAssets/drawable-ldpi.zip",
            kAssets                                 +   "/StreamingAssets/drawable-mdpi.zip",

            kPluginsPath                            +   "/RevMobPopup.cs",
            kPluginsPath                            +   "/RevMobLink.cs",
            kPluginsPath                            +   "/RevMobFullscreen.cs",
            kPluginsPath                            +   "/RevMobBanner.cs",
            kPluginsPath                            +   "/RevMob.cs",
            kPluginsPath                            +   "/IRevMobListener.cs",
            kPluginsPath                            +   "/AdColony.cs",

            kPluginsPath                            +   "/Editor/AdvertiserOptIn.js",
            kPluginsPath                            +   "/Editor/TooltipDrawer.cs",
            kPluginsPath                            +   "/Editor/GoogleAnalyticsMenu.cs",
            kPluginsPath                            +   "/Editor/RangedTooltipDrawer.js",

            kAndroidPluginsPath                     +   "/RevMobAndroidFullscreen.cs",
            kAndroidPluginsPath                     +   "/RevMobAndroidBanner.cs",
            kAndroidPluginsPath                     +   "/RevMobAndroid.cs",
            kAndroidPluginsPath                     +   "/unityadc.jar",
            kAndroidPluginsPath                     +   "/revmob-android-wrapper.jar",
            kAndroidPluginsPath                     +   "/FlurryAnalytics-5.4.0.jar",
            kAndroidPluginsPath                     +   "/consoliads_sdk.jar",
            kAndroidPluginsPath                     +   "/AppTrackerUnity.jar",
            kAndroidPluginsPath                     +   "/AppTracker.jar",
            kAndroidPluginsPath                     +   "/applovin-unity-plugin.jar",
            kAndroidPluginsPath                     +   "/applovin-sdk-6.3.0.jar",
            kAndroidPluginsPath                     +   "/android-support-v4.jar",
            kAndroidPluginsPath                     +   "/adcolony.jar",
            kAndroidPluginsPath                     +   "/AndroidManifest.xml",
            kAndroidPluginsPath                     +   "/RevMobAndroidPopup.cs",
            kAndroidPluginsPath                     +   "/RevMobAndroidLink.cs",
            kAndroidPluginsPath                     +   "/UnityAds.aar",
            kAndroidPluginsPath                     +   "/fyber-sdk-bridge.jar"                  ,
            kAndroidPluginsPath                     +   "/play-services-ads-9.6.1.jar"           ,
            kAndroidPluginsPath                     +   "/play-services-ads-lite-9.6.1.jar"      ,
            kAndroidPluginsPath                     +   "/play-services-analytics-impl-9.6.1.jar",
            kAndroidPluginsPath                     +   "/play-services-base-9.6.1.jar"          ,
            kAndroidPluginsPath                     +   "/play-services-basement-9.6.1.jar"      ,
            kAndroidPluginsPath                     +   "/play-services-clearcut-9.6.1.jar"      ,
            kAndroidPluginsPath                     +   "/play-services-gass-9.6.1.jar"          ,
            kAndroidPluginsPath                     +   "/play-services-tasks-9.6.1.jar"         ,
            kAndroidPluginsPath                     +   "/consoliads-sdk.jar"                    ,
            kAndroidPluginsPath                     +   "/firebase-analytics-9.6.1.jar"          ,
            kAndroidPluginsPath                     +   "/firebase-analytics-impl-9.6.1.jar"     ,
            kAndroidPluginsPath                     +   "/firebase-common-9.6.1.jar"             ,
            kAndroidPluginsPath                     +   "/firebase-iid-9.6.1.jar"                ,
            kAndroidPluginsPath                     +   "/firebase-lib.jar"                      ,

            kIOSPluginsPath                         +   "/ALAd.h",
            kIOSPluginsPath                         +   "/ALAdDelegateWrapper.h",
            kIOSPluginsPath                         +   "/ALAdDisplayDelegate.h",
            kIOSPluginsPath                         +   "/ALAdLoadDelegate.h",
            kIOSPluginsPath                         +   "/ALAdRewardDelegate.h",
            kIOSPluginsPath                         +   "/ALAdService.h",
            kIOSPluginsPath                         +   "/ALAdSize.h",
            kIOSPluginsPath                         +   "/ALAdType.h",
            kIOSPluginsPath                         +   "/ALAdUpdateDelegate.h",
            kIOSPluginsPath                         +   "/ALAdVideoPlaybackDelegate.h",
            kIOSPluginsPath                         +   "/ALAdView.h",
            kIOSPluginsPath                         +   "/ALAnnotations.h",
            kIOSPluginsPath                         +   "/ALErrorCodes.h",
            kIOSPluginsPath                         +   "/ALEventService.h",
            kIOSPluginsPath                         +   "/ALEventTypes.h",
            kIOSPluginsPath                         +   "/ALIncentivizedInterstitialAd.h",
            kIOSPluginsPath                         +   "/ALInterstitialAd.h",
            kIOSPluginsPath                         +   "/ALInterstitialCache.h",
            kIOSPluginsPath                         +   "/ALManagedLoadDelegate.h",
            kIOSPluginsPath                         +   "/ALNativeAd.h",
            kIOSPluginsPath                         +   "/ALNativeAdLoadDelegate.h",
            kIOSPluginsPath                         +   "/ALNativeAdPrecacheDelegate.h",
            kIOSPluginsPath                         +   "/ALNativeAdService.h",
            kIOSPluginsPath                         +   "/ALPostbackDelegate.h",
            kIOSPluginsPath                         +   "/ALPostbackService.h",
            kIOSPluginsPath                         +   "/ALSdk.h",
            kIOSPluginsPath                         +   "/ALSdkSettings.h",
            kIOSPluginsPath                         +   "/ALSwiftHeaders.h",
            kIOSPluginsPath                         +   "/ALTargetingData.h",
            kIOSPluginsPath                         +   "/AppTracker.h",
            kIOSPluginsPath                         +   "/CBAnalytics.h",
            kIOSPluginsPath                         +   "/CBInPlay.h",
            kIOSPluginsPath                         +   "/Chartboost.h",
            kIOSPluginsPath                         +   "/ChartboostManager.h",
            kIOSPluginsPath                         +   "/GADUAdLoader.h",
            kIOSPluginsPath                         +   "/GADUBanner.h",
            kIOSPluginsPath                         +   "/GADUInterstitial.h",
            kIOSPluginsPath                         +   "/GADUNativeCustomTemplateAd.h",
            kIOSPluginsPath                         +   "/GADUObjectCache.h",
            kIOSPluginsPath                         +   "/GADURequest.h",
            kIOSPluginsPath                         +   "/GADURewardBasedVideoAd.h",
            kIOSPluginsPath                         +   "/GADUTypes.h",
            kIOSPluginsPath                         +   "/GAI.h",
            kIOSPluginsPath                         +   "/GAIDictionaryBuilder.h",
            kIOSPluginsPath                         +   "/GAIEcommerceFields.h",
            kIOSPluginsPath                         +   "/GAIEcommerceProduct.h",
            kIOSPluginsPath                         +   "/GAIEcommerceProductAction.h",
            kIOSPluginsPath                         +   "/GAIEcommercePromotion.h",
            kIOSPluginsPath                         +   "/GAIFields.h",
            kIOSPluginsPath                         +   "/GAIHandler.h",
            kIOSPluginsPath                         +   "/GAILogger.h",
            kIOSPluginsPath                         +   "/GAITrackedViewController.h",
            kIOSPluginsPath                         +   "/GAITracker.h",
            kIOSPluginsPath                         +   "/RevMobAdLink.h",
            kIOSPluginsPath                         +   "/RevMobAds.h",
            kIOSPluginsPath                         +   "/RevMobAdsDelegate.h",
            kIOSPluginsPath                         +   "/RevMobBanner.h",
            kIOSPluginsPath                         +   "/RevMobBannerView.h",
            kIOSPluginsPath                         +   "/RevMobButton.h",
            kIOSPluginsPath                         +   "/RevMobFullscreen.h",
            kIOSPluginsPath                         +   "/RevMobPopup.h",
            kIOSPluginsPath                         +   "/RevMobUnityiOSDelegate.h",
            kIOSPluginsPath                         +   "/UnityAdsUnityWrapper.h",
            kIOSPluginsPath                         +   "/ALAdDelegateWrapper.m",
            kIOSPluginsPath                         +   "/ALInterstitialCache.m",
            kIOSPluginsPath                         +   "/ALManagedLoadDelegate.m",
            kIOSPluginsPath                         +   "/ChartBoostBinding.m",
            kIOSPluginsPath                         +   "/GADUAdLoader.m",
            kIOSPluginsPath                         +   "/GADUBanner.m",
            kIOSPluginsPath                         +   "/GADUInterface.m",
            kIOSPluginsPath                         +   "/GADUInterstitial.m",
            kIOSPluginsPath                         +   "/GADUNativeCustomTemplateAd.m",
            kIOSPluginsPath                         +   "/GADUObjectCache.m",
            kIOSPluginsPath                         +   "/GADURequest.m",
            kIOSPluginsPath                         +   "/GADURewardBasedVideoAd.m",
            kIOSPluginsPath                         +   "/GAIHandler.m",
            kIOSPluginsPath                         +   "/RevMobUnityiOSBinding.m",
            kIOSPluginsPath                         +   "/AppLovinUnity.mm",
            kIOSPluginsPath                         +   "/AppTracker.mm",
            kIOSPluginsPath                         +   "/ChartBoostManager.mm",
            kIOSPluginsPath                         +   "/UnityADC.mm",
            kIOSPluginsPath                         +   "/UnityAdsUnityWrapper.mm",
            kIOSPluginsPath                         +   "/arrow@2x.png",
            kIOSPluginsPath                         +   "/arrow_dark@2x.png",
            kIOSPluginsPath                         +   "/browser_icon@2x.png",
            kIOSPluginsPath                         +   "/close@2x.png",
            kIOSPluginsPath                         +   "/cross@2x.png",
            kIOSPluginsPath                         +   "/default_icon_download.png",
            kIOSPluginsPath                         +   "/empty_star@2x.png",
            kIOSPluginsPath                         +   "/full_star@2x.png",
            kIOSPluginsPath                         +   "/half_star@2x.png",
            kIOSPluginsPath                         +   "/top_Apps@2x.png",
            kIOSPluginsPath                         +   "/RevMobIOS.cs",
            kIOSPluginsPath                         +   "/RevMobIOSBanner.cs",
            kIOSPluginsPath                         +   "/RevMobIOSFullscreen.cs",
            kIOSPluginsPath                         +   "/RevMobIosLink.cs",
            kIOSPluginsPath                         +   "/libAppLovinSdk.a",
            kIOSPluginsPath                         +   "/libChartboost.a",
            kIOSPluginsPath                         +   "/libGoogleAnalyticsServices.a",
            kIOSPluginsPath                         +   "/RevMobAds.a",
            kIOSPluginsPath                         +   "/FireAnalytics.h"      ,
            kIOSPluginsPath                         +   "/GADUNativeExpressAd.h",
            kIOSPluginsPath                         +   "/GADUPluginUtil.h"     ,
            kIOSPluginsPath                         +   "/GADUNativeExpressAd.m",
            kIOSPluginsPath                         +   "/GADUPluginUtil.m"     ,
            kIOSPluginsPath                         +   "/libFireAnalytics.a"   ,

        };

    #endregion

    #region Methods

    public static void Uninstall()
    {
        bool _startUninstall = EditorUtility.DisplayDialog(kUninstallAlertTitle, kUninstallAlertMessage, "Uninstall", "Cancel");

        if (_startUninstall)
        {

            foreach (string _eachFILE in kPluginFiles)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFILE);

                if (File.Exists(_absolutePath))
                {
                    Delete(_absolutePath);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }

            foreach (string _eachFolder in kPluginFolders)
            {
                string _absolutePath = AssetPathToAbsolutePath(_eachFolder);

                if (Directory.Exists(_absolutePath))
                {
                    Directory.Delete(_absolutePath, true);

                    // Delete meta files.
                    if (File.Exists(_absolutePath + ".meta"))
                    {
                        Delete(_absolutePath + ".meta");
                    }
                }
            }
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("ConsoliAds",
                                        "Uninstall successful!",
                                        "Ok");
        }
    }

    #endregion
    public static string AssetPathToAbsolutePath(string _relativePath)
    {
        string _unrootedRelativePath = _relativePath.TrimStart('/');

        if (!_unrootedRelativePath.StartsWith(kAssets))
            return null;

        string _absolutePath = Path.Combine(GetProjectPath(), _unrootedRelativePath);

        // Return absolute path to asset
        return _absolutePath;
    }
    public static string GetProjectPath()
    {
        return Path.GetFullPath(Application.dataPath + @"/../");
    }
    public static void Delete(string _filePath)
    {
#if (UNITY_WEBPLAYER || UNITY_WEBGL)
			Debug.LogError("[CPFileOperations] File operations are not supported.");
#else
        File.Delete(_filePath);
#endif
    }

}

#endif