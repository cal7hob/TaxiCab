/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace DldUtil
{

public static class BigFileReader
{
	public static bool FileHasText(string path, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";

		long currentLine = 0;
		while (true)
		{
			++currentLine;
			line = sr.ReadLine();

			if (line == null)
			{
				break;
			}

			for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
			{
				if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
				{
					sr.Close();
					bs.Close();
					fs.Close();

					return true;
				}
			}
		}

		sr.Close();
		bs.Close();
		fs.Close();

		return false;
	}

	public static string SeekText(string path, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = string.Empty;
		
		//long currentLine = 0;
		while (true)
		{
			//++currentLine;
			line = sr.ReadLine();
			//Debug.LogFormat("seeking... line number {0}: {1}", currentLine, line);

			// reached end of file?
			if (line == null)
			{
				break;
			}
			
			for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
			{
				if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
				{
					return line;
				}
			}
		}

		return string.Empty;
	}

	public struct FoundText
	{
		public long LineNumber;
		public string Text;
	}

	public static List<FoundText> SeekAllText(string path, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = string.Empty;
		
		List<FoundText> returnValue = new List<FoundText>();

		long currentLine = 0;
		while (true)
		{
			++currentLine;
			line = sr.ReadLine();
			//Debug.LogFormat("seeking... line number {0}: {1}", currentLine, line);

			// reached end of file?
			if (line == null)
			{
				break;
			}
			
			for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
			{
				if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
				{
					FoundText newFoundText;
					newFoundText.LineNumber = currentLine;
					newFoundText.Text = line;
					returnValue.Add(newFoundText);
				}
			}
		}

		return returnValue;
	}
	
	public static IEnumerable<string> ReadFile(string path, params string[] seekText)
	{
		return ReadFile(path, true, seekText);
	}

	public static IEnumerable<string> ReadFile(string path, bool startAfterSeekedText, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";
		
		bool seekTextRequested = (seekText != null) && (seekText.Length > 0) && !string.IsNullOrEmpty(seekText[0]);

		
		long seekTextFoundAtLine = -1;
		
		
		if (seekTextRequested)
		{
			long currentLine = 0;
			while (true)
			{
				++currentLine;
				line = sr.ReadLine();
				//Debug.LogFormat("seeking... line number {0}: {1}", currentLine, line);
				
				// reached end of file?
				if (line == null)
				{
					break;
				}

				var atLeastOneSeekTextFound = false;
				for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
				{
					if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
					{
						atLeastOneSeekTextFound = true;
						break;
					}
				}

				// if seekText not found yet, continue search
				if (!atLeastOneSeekTextFound)
				{
					continue;
				}

				seekTextFoundAtLine = currentLine;
					
				//Debug.Log("seeking: " + line);
				//Debug.LogFormat("seekText found at line number {0}: {1}", currentLine, line);
			}
			//Debug.Log("done seeking");
		
			if (seekTextFoundAtLine != -1)
			{	
				fs.Seek(0, SeekOrigin.Begin);
				
				currentLine = 0;
				while (true)
				{
					++currentLine;
					line = sr.ReadLine();
					
					if (line == null)
					{
						break;
					}
					if (startAfterSeekedText && currentLine <= seekTextFoundAtLine)
					{
						continue;
					}
					if (!startAfterSeekedText && currentLine < seekTextFoundAtLine)
					{
						continue;
					}
					
					//Debug.Log("seeked: " + line);
					
					yield return line;
				}
			}
		}
		else
		{
			while (true)
			{
				line = sr.ReadLine();
				
				if (line == null)
				{
					break;
				}
				
				yield return line;
			}
		}
		
		line = "";
		
		sr.Close();
		bs.Close();
		fs.Close();
	}


	public static IEnumerable<string> ReadFile(string path)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";

		while (true)
		{
			line = sr.ReadLine();

			if (line == null)
			{
				break;
			}

			yield return line;
		}

		line = "";

		sr.Close();
		bs.Close();
		fs.Close();
	}
}

}
