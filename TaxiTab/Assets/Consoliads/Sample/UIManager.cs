using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    CAAdmob admob;
    // Use this for initialization 
    void Start()
    {
        admob = new CAAdmob();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // ------------------------------------------------------------------------------------------------------------------
    public void LogEvent()
    {
        ConsoliAds.Instance.firebase.LogEvent("CustomEvent", "Click", "Main Menu");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogSelectContent()
    {
        ConsoliAds.Instance.firebase.SelectContent("UnityButton", "button");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogJoinGroup()
    {
        ConsoliAds.Instance.firebase.JoinGroup("ConsoliGroup");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogLevelUp()
    {
        ConsoliAds.Instance.firebase.LevelUp("MC_Consoliads", 100);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogPostScore()
    {
        ConsoliAds.Instance.firebase.PostScore(23451, 25, "MC");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogSpendVirtualCurrency()
    {
        ConsoliAds.Instance.firebase.SpendVirtualCurrency("ItemName", "Coins", 302);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogTutorialBegin()
    {
        ConsoliAds.Instance.firebase.TutorialBegin();
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void LogTutorialComplete()
    {
        ConsoliAds.Instance.firebase.TutorialComplete();
    }
    // ------------------------------------------------------------------------------------------------------------------

    public void LogUnlockAchievement()
    {
        ConsoliAds.Instance.firebase.UnlockAchievement("Untouchable");
    }
    
    public void openSampleScene()
    {
        Application.LoadLevel("sample");
    }
}
