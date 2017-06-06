using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TinHead_Developer
{


    public class MainMenuController : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void play(int Scene)
        {
            GameManager.Instance.Play(1);
        }

        public void MoreFun()
        {
      //      Application.OpenURL(ConsoliAds.Instance.MoreFunURL());
        }

        public void RateUsYes()
        {
            Application.OpenURL("http://play.google.com/store/apps/details?id=com.molev.car.wash.mechanic.workshop");
            }

        public void RateusLater()
        {
        }
        public void RateUsNo()
        {
       //     string email = ConsoliAds.Instance.supportEmail;
            string subject = MyEscapeURL("Feedback: | Car wash & mechanic workshop | V1 | Play Store");
            string body = MyEscapeURL("");
        //    Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
        }
        public void OpenRateUs()
        {
        }

        string MyEscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }
    }
    }

