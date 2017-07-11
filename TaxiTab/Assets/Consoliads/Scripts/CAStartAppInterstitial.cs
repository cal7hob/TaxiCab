using System;
using UnityEngine;
using System.Collections.Generic;
    class CAStartAppInterstitial:AdNetwork    {
    public override void initialize(string gameObjectName, string uniqueDeviceID)
    {
        Debug.Log("ADNETWORK: INITIALIZE StartAppInterstitial");
        #if UNITY_ANDROID
        #endif
    }

    public override bool showAd(int sceneID)
    {
        bool result = false;
        return result;
    }
}

