using UnityEngine;
using System.Collections;
using UnityEditor;


public class CASettings : Editor
{

    [MenuItem("ConsoliAds/Uninstall")]
    public static void Uninstall()
    {
        CAUninstallPlugin.Uninstall();
    }
    [MenuItem("ConsoliAds/Documentation")]
    public static void OpenDocumentation()
    {
        string url = "http://www.consoliads.com/admin/documentation";
        Application.OpenURL(url);
    }

}
