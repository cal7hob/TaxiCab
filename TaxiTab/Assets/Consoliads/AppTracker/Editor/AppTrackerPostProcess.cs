using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace AppTrackerUnitySDK {
	public class AppTrackerPostProcess : MonoBehaviour {

		[PostProcessBuild(5000)]
		public static void onPostProcessBuild(BuildTarget t, string p) {
	#if UNITY_5
			if(t == BuildTarget.iOS) {
				postProcess (p);
			}
	#else
			if(t == BuildTarget.iPhone && !EditorUserBuildSettings.appendProject) {
				postProcess (p);
			}
	#endif
		}

		private static void postProcess(string p) {
			UnityEditor.XCodeEditorAppTracker.XCProject proj = new UnityEditor.XCodeEditorAppTracker.XCProject (p);
			string projModPath = System.IO.Path.Combine (Application.dataPath, "Consoliads/AppTracker/Editor");
			var files = System.IO.Directory.GetFiles (projModPath, "*.projmods", System.IO.SearchOption.AllDirectories);

			foreach (var file in files) {
				proj.ApplyMod (file);
			}
			proj.Save ();
		}
	}
}