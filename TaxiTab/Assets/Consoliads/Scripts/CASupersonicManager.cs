using System;
using System.Collections.Generic;
using UnityEngine;
class CASupersonicManager
{
    private Boolean IsStarted = false;
    private static CASupersonicManager _instance = null;
    private CASupersonicManager() { }
    public static CASupersonicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CASupersonicManager();
            }
            return _instance;
        }
    }
    public void startSupersonic()
    {
        if (!IsStarted)
        {
            //Start Supersonic Plugin
            Debug.Log("Supersonic.Agent.start");
            Supersonic.Agent.start();

            //Supersonic tracking sdk
            Supersonic.Agent.reportAppStarted();

            //Dynamic config example
            SupersonicConfig.Instance.setClientSideCallbacks(true);
            IsStarted = true;
        }
    }
}
