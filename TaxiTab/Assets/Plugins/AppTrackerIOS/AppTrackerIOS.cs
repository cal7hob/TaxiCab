using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace AppTrackerUnitySDK {
	public class AppTrackerIOS : MonoBehaviour {
		#if UNITY_IPHONE
	
		//declare the methods in the native code
		[DllImport ("__Internal")]
		private static extern void _startSession (string apikey, bool enableAutoRecache);
		[DllImport ("__Internal")]
		private static extern void _closeSession (); 
		[DllImport ("__Internal")]
		private static extern void _loadModule (string location);
		[DllImport ("__Internal")]
		private static extern void _loadModuleWithUserData (string location, string userdata);
		[DllImport ("__Internal")]
		private static extern void _loadModuleToCache (string location);
		[DllImport ("__Internal")]
		private static extern void _loadModuleToCacheWithUserData (string location, string userdata);
		[DllImport ("__Internal")]
		private static extern void _destroyModule ();
		[DllImport ("__Internal")]
		private static extern void _fixAdOrientation (int orientation);
		[DllImport ("__Internal")]
		private static extern void _setCrashHandlerStatus (bool enable);
		[DllImport ("__Internal")]
		private static extern void _setAgeRange (string range);
		[DllImport ("__Internal")]
		private static extern void _setGender (string gender);
		[DllImport ("__Internal")]
		private static extern bool _isAdReady (string location);


		// Public methods accessible to developers
		public static void startSession (string apikey, bool enableAutoRecache) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_startSession(apikey, enableAutoRecache);
			}
		}
		public static void closeSession () {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_closeSession();
			}
		} 

		public static void loadModule (string location) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_loadModule(location);
			}
		}

		public static void loadModule (string location, string userdata) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_loadModuleWithUserData(location, userdata);
			}
		}

		public static void loadModuleToCache (string location) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_loadModuleToCache(location);
			}
		}

		public static void loadModuleToCache (string location, string userdata) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_loadModuleToCacheWithUserData(location, userdata);
			}
		}

		public static void destroyModule () {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_destroyModule();
			}
		}

		public enum AdOrientation {
			AdOrientation_AutoDetect=0,
			AdOrientation_Landscape=1,
			AdOrientation_Portrait=2
		} ;

		public static void fixAdOrientation(AdOrientation orientation)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_fixAdOrientation((int)orientation);		
			}
		}

		public static void setCrashHandlerStatus(Boolean enable)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setCrashHandlerStatus(enable);
			}
		}

		public static void setAgeRange(string range) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setAgeRange(range);
			}
		}

		public static void setGender(string gender) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_setGender(gender);
			}
		}

		public static bool isAdReady(string location) {
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				return _isAdReady(location);
			}	
			return false;
		}

		public static event Action<string> onModuleClosedEvent;
		public static event Action<string> onModuleLoadedEvent;
		public static event Action<string,string,bool> onModuleFailedEvent;
		public static event Action<string> onModuleCachedEvent;
		public static event Action<string> onModuleClickedEvent;
		public static event Action<bool> onMediaFinishedEvent;

		void Awake() {
			//gameObject.name = "AppTracker";
			DontDestroyOnLoad (gameObject);
		}

		public void onModuleClosed(string message) {
			if (onModuleClosedEvent != null)
				onModuleClosedEvent(message);
		}
		public void onModuleFailed(string message) {
			if (onModuleFailedEvent != null) 
			{
				string[] names = message.Split(':');
				onModuleFailedEvent (names[0],names[1],"yes".Equals(names[2])?true:false);
			}
		}
		public void onModuleLoaded(string message) {
			if (onModuleLoadedEvent != null)
				onModuleLoadedEvent (message);
		}
		public void onModuleCached(string message) {
			if (onModuleCachedEvent != null)
				onModuleCachedEvent (message);
		}
		public void onModuleClicked(string message) {
			if (onModuleClickedEvent != null)
				onModuleClickedEvent (message);
		}
		public void onMediaFinished(string message) {
			if(onMediaFinishedEvent != null)
				onMediaFinishedEvent("1".Equals(message));
		}
	#endif
	}
}
