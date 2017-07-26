/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/


namespace BuildReportTool
{

// per platform identification
// needed to handle special cases
// example: some platforms have a compressed build, some do not.
// also, native plugins are handled differently in each platform.
public enum BuildPlatform
{
	None = 0,

	// -------
	// Mobiles
	// -------

	Android = 1,
	iOS,
	Blackberry,
	WindowsPhone8,
	Tizen,


	// --------
	// Web
	// --------

	Web = 100,
	Flash,
	WebGL, // upcoming

	
	// --------
	// Desktops
	// --------

	// distinctions between 32 or 64 bit need to be made to
	// determine which existing native plugins are used or not

	MacOSX32 = 200,
	MacOSX64,
	MacOSXUniversal,

	Windows32 = 300,
	Windows64,

	Linux32 = 400,
	Linux64,
	LinuxUniversal,


	// ------
	// Consoles (7th gen)
	// ------

	// currently not handled in any special way (probably needs to be):
	XBOX360 = 500,
	PS3,
	Wii, // for posterity


	// ------
	// Consoles (8th gen)
	// ------
	
	XBOXOne = 600,
	
	PS4,
	PSVitaNative,
	PSMobile,
	
	WiiU,
	Nintendo3DS
}

}
