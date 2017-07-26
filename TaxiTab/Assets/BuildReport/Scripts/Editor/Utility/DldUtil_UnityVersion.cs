/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using System.Collections;

namespace DldUtil
{
	public static class UnityVersion
	{
		

		public static void GetUnityVersionNumbers(string unityVersionString, out int major, out int minor, out int patch)
		{
			var splits = unityVersionString.Split(new[] {".", "a", "b", "rc", "f"}, StringSplitOptions.RemoveEmptyEntries);
			
			major = -1;
			minor = -1;
			patch = -1;

			if (splits.Length >= 1)
			{
				int.TryParse(splits[0], out major);
			}

			if (splits.Length >= 2)
			{
				int.TryParse(splits[1], out minor);
			}

			if (splits.Length >= 3)
			{
				int.TryParse(splits[2], out patch);
			}
		}
		
		public static void GetUnityVersionNumbers(out int major, out int minor, out int patch)
		{
			GetUnityVersionNumbers(Application.unityVersion, out major, out minor, out patch);

			//Debug.LogFormat("major: {0}, minor: {1}, patch: {2}", major, minor, patch);
		}

		public static bool IsUnityVersionAtLeast(int majorAtLeast, int minorAtLeast, int patchAtLeast)
		{
			int unityMajor;
			int unityMinor;
			int unityPatch;

			GetUnityVersionNumbers(out unityMajor, out unityMinor, out unityPatch);

			return (unityMajor >= majorAtLeast) && (unityMinor >= minorAtLeast) && (unityPatch >= patchAtLeast);
		}

		public static bool IsUnityVersionAtMost(int majorAtMost, int minorAtMost, int patchAtMost)
		{
			int unityMajor;
			int unityMinor;
			int unityPatch;

			GetUnityVersionNumbers(out unityMajor, out unityMinor, out unityPatch);

			return (unityMajor <= majorAtMost) && (unityMinor <= minorAtMost) && (unityPatch <= patchAtMost);
		}
	}
}
