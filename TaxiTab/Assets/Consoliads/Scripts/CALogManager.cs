using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;


public class CALogManager
{
    private static CALogManager _instance;
    private bool logEnabled = false;

    //------------------------------------------------------------------------------
    private CALogManager() { }
    //------------------------------------------------------------------------------
    public static CALogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CALogManager();
            }
            return _instance;
        }
    }

    public void EnableLog(bool value)
    {
        logEnabled = value;
    }


    public bool IsLogEnabled()
    {
		return logEnabled;
    }

    public void LogError(string message)
    {
        if (IsLogEnabled())
        {
            Debug.LogError(message);
        }
    }

    public void LogWarrning(string message)
    {
        if (IsLogEnabled())
        {
            Debug.LogWarning(message);
        }
    }

    public void Log(string message)
    {
        if (IsLogEnabled())
        {
            Debug.Log(message);
        }
    }



}

