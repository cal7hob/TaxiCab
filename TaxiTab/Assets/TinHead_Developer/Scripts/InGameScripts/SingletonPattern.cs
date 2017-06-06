using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Singleton pattern.
/// </summary>

    namespace TinHead_Developer
{
public class SingletonPattern<GComponent > : MonoBehaviour
        where GComponent : Component 
{
    protected static GComponent _instance;

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static GComponent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GComponent>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<GComponent>();
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
    /// </summary>
     protected void Awake()
    {

        }
   }
}