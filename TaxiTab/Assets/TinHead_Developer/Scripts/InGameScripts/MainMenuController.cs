using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TinHead_Developer
{


    public class MainMenuController : MonoBehaviour
    {
		void Start(){
			ConsoliAds.Instance.ShowInterstitial (0);
			ConsoliAds.Instance.ShowBanner (0);
			SoundManager.Instance.PlaySound ("MainMenu");
		}
        public void play(int Scene)
        {
			ClickSound ();
            GameManager.Instance.Play(1);
        }

        public void MoreFun()
        {
			ClickSound ();
      //      Application.OpenURL(ConsoliAds.Instance.MoreFunURL());
        }

        public void RateUsYes()
        {
			ClickSound ();
            Application.OpenURL("http://play.google.com/store/apps/details?id=com.molev.car.wash.mechanic.workshop");
            }

        public void RateusLater()
        {
			ClickSound ();
        }
        public void RateUsNo()
        {
			ClickSound ();
       //     string email = ConsoliAds.Instance.supportEmail;
            string subject = MyEscapeURL("Feedback: | Car wash & mechanic workshop | V1 | Play Store");
            string body = MyEscapeURL("");
        //    Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
        }
		public void OpenRateUs(GameObject RateUs)
        {
			ClickSound ();
			RateUs.SetActive (true);
        }

        string MyEscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }
		 void ClickSound(){
			SoundManager.Instance.PlaySound ("ClickButton");
    }
  }
}
