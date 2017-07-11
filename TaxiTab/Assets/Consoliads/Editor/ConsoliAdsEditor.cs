using UnityEngine;
using System.Collections;
using UnityEditor;
using SimpleJSON;
using System;

[CustomEditor(typeof(ConsoliAds))]
public class ConsoliAdsEditor : Editor
{
    public override void OnInspectorGUI()
    {
		
        DrawDefaultInspector();


        ConsoliAds sdkScript = (ConsoliAds)target;


        /*
        for (int i = 0; i < sdkScript.sceneList.Length; i++)
        {
            SceneTypes type = sdkScript.sceneList[i].sceneType;
            if (type != SceneTypes.RewardedVideo)
            {
                //is a rewarded video

                for (int j = 0; j < sdkScript.sceneList[i].adNetworkList.Length; j++)
                {
                    if (sdkScript.sceneList[i].adNetworkList[j] == AdNetworkType.ADMOBREWARDEDVIDEO)
                    {
                        sdkScript.sceneList[i].adNetworkList[j] = AdNetworkType.EMPTY;
                        EditorUtility.DisplayDialog("Error", "Can not select rewarded video ads in this scene", "Ok");
                    }
                }
            }
            else
            {

            }
        }
        */

        if (GUILayout.Button("Configure Server"))
        {
            string result = null;
            //sdkScript.ConfigureServer();
            String errorMsg = "", warnings = "";
            if (sdkScript.userSignature == "")
            {
                errorMsg += "User Signature cannot be empty!\n";
            }

            if (sdkScript.productName == "")
            {
                errorMsg += "Product Name cannot be empty!\n";
            }
            if (sdkScript.bundleIdentifier == "")
            {
                errorMsg += "Bundle Identifier cannot be empty!";
            }
            if (Platform.IsDefined(typeof(Platform), sdkScript.platform) == false)
            {
                errorMsg += "Store cannot be empty!";
            }
			if (sdkScript.platform != Platform.Apple && EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android ) 
			{
				errorMsg += "Plateform does not mactch with your Target Plateform!\n";
			}
			else if (sdkScript.platform == Platform.Apple && EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
			{
				errorMsg += "Plateform does not mactch with your Target Plateform!\n";
			}
            if (PlayerSettings.applicationIdentifier != sdkScript.bundleIdentifier)
            {
                warnings += "Bundle Indentifier does not match with your application's bundle indentifier!\n";
            }

            if (errorMsg != "")
            {
                EditorUtility.DisplayDialog("Error", errorMsg, "Ok");
            }
            else {
                if (warnings != "")
                {
                    bool dialogResult = EditorUtility.DisplayDialog("Warning", warnings, "Continue", "Cancel");
                    if (dialogResult)
                    {
                        result = ServerConfig.Instance.configureServer(sdkScript);
                    }
                    else {
                        EditorUtility.DisplayDialog("Success", "ConsoliAds synchronization was canceled", "Ok");
                    }
                }
                else {

                    result = ServerConfig.Instance.configureServer(sdkScript);
                }
            }
            if (result != null)
            {
                //popup that show the response message from server
                EditorUtility.DisplayDialog("Configure server", result, "Ok");
            }
        }


        if (GUILayout.Button("Goto Consoli Ads"))
        {
            Help.BrowseURL(CAConstants.ConsoliAdsBaseURL);
        }
    }

}

