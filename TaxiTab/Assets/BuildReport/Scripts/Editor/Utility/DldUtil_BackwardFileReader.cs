/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.IO;
using System.Text;

namespace DldUtil
{
	// from http://arstechnica.com/civis/viewtopic.php?p=22839293&sid=e2bcb348ac8b58c9016e6a0a6442ba1b#p22839293
	public class BackwardReader
	{
		readonly FileStream fs;

		public BackwardReader(string path)
		{
			fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			fs.Seek(0, SeekOrigin.End);
		}

		public string ReadLine()
		{
			var text = new byte[1];
			long position = 0;

			fs.Seek(0, SeekOrigin.Current);
			position = fs.Position;

			// do we have a trailing \r\n?
			if (fs.Length > 1)
			{
				var vagnretur = new byte[2];

				fs.Seek(-2, SeekOrigin.Current);
				fs.Read(vagnretur, 0, 2);

				if (Encoding.ASCII.GetString(vagnretur).Equals("\r\n"))
				{
					// move it back
					fs.Seek(-2, SeekOrigin.Current);
					position = fs.Position;
				}
			}

			while (fs.Position > 0)
			{
				text.Initialize();

				// read one char
				fs.Read(text, 0, 1);
				var asciiText = Encoding.ASCII.GetString(text);

				// moveback to the character before
				fs.Seek(-2, SeekOrigin.Current);

				if (asciiText.Equals("\n"))
				{
					fs.Read(text, 0, 1);

					asciiText = Encoding.ASCII.GetString(text);
					if (asciiText.Equals("\r"))
					{
						fs.Seek(1, SeekOrigin.Current);
						break;
					}
				}
			}
			
			var count = int.Parse((position - fs.Position).ToString());
			var line = new byte[count];
			fs.Read(line, 0, count);
			fs.Seek(-count, SeekOrigin.Current);

			return Encoding.ASCII.GetString(line);
		}

		public bool SOF
		{
			get { return fs.Position == 0; }
		}

		public void Close()
		{
			fs.Close();
		}
	}

}
