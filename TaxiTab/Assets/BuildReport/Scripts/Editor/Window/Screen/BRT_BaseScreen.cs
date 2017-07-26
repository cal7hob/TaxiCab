/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;



namespace BuildReportTool.Window.Screen
{

public abstract class BaseScreen
{
	public abstract string Name { get; }

	public abstract void RefreshData(BuildInfo buildReport);

	public abstract void DrawGUI(Rect position, BuildInfo buildReportToDisplay);
	
	public virtual void Update(double timeNow, double deltaTime, BuildInfo buildReportToDisplay)
	{
	}

	
	protected void DrawCentralMessage(Rect position, string msg)
	{
		float w = 300;
		float h = 100;
		float x = (position.width - w) * 0.5f;
		float y = (position.height - h) * 0.25f;

		GUI.Label(new Rect(x, y, w, h), msg);
	}
}

}
