/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.IO;



public static class BRT_LibCacheUtil
{
	// assetPath is expected to start with "Assets/"
	//
	// since this function calls AssetDatabase.AssetPathToGUID(),
	// it can only be called from the Unity main thread
	//
	public static long GetImportedFileSize(string assetPath)
	{
		long result = -1;

		assetPath = BuildReportTool.Util.MyHtmlDecode(assetPath);

		// files in "StreamingAssets" folder do not get imported
		// in the 1st place, so skip them
		if (BuildReportTool.Util.IsFileStreamingAsset(assetPath))
		{
			return -1;
		}

		// files like Thumbs.db or .DS_Store files do not get imported
		if (BuildReportTool.Util.IsUselessFile(assetPath))
		{
			return -1;
		}

		// Unix-style hidden files do not get imported
		if (BuildReportTool.Util.IsFileAUnixHiddenFile(assetPath))
		{
			return -1;
		}

		if (!string.IsNullOrEmpty(assetPath))
		{
			string guid = AssetDatabase.AssetPathToGUID(assetPath);
			if (guid.Length < 2)
			{
				//Debug.Log(assetPath + " has no guid? value is \"" + guid + "\"");
				return -1;
			}

			string assetImportedPath = Path.GetFullPath(Application.dataPath + "../../Library/cache/" + guid.Substring(0, 2) + "/" + guid);

			if (File.Exists(assetImportedPath))
			{
				result = BuildReportTool.Util.GetFileSizeInBytes(assetImportedPath);
			}
			else
			{
				//Debug.Log(assetPath + " not found: " + assetImportedPath);
				assetImportedPath = Path.GetFullPath(Application.dataPath + "../../Library/metadata/" + guid.Substring(0, 2) + "/" + guid);
				if (File.Exists(assetImportedPath))
				{
					result = BuildReportTool.Util.GetFileSizeInBytes(assetImportedPath);
				}
			}
		}

		return result;
	}
}
