using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinHead_Developer {
    public class LevelSelection : MonoBehaviour {

        // Use this for initialization
        int CurrentSelectedLevel = 0;
        public Image[] Lock;
        public GameObject UnlockLevelTab;
        public Text CurrentSelectedLevelCoin;
        public Text UnlockMessage;
		public GameObject [] LevelStars;
		public  Sprite Star;
		public GameObject NExt;
        void Start() {
            SoundManager.Instance.PlaySound("LevelSelection");
			PlayerPrefsX.SetBool("Level1", true);

            LoadLockers();
			LoadStars ();
			ConsoliAds.Instance.ShowInterstitial (1);
    }

      public void LoadLockers()
        {
            if (GameManager.Instance.IsLockedBasedGame)
            {
                for (int i = 0; i < GameManager.Instance.TotalScene; i++)
                {

                    if (PlayerPrefsX.GetBool("Level" + (i + 1).ToString()))
                    {
						Lock[i].gameObject.SetActive(false);
						Debug.Log(PlayerPrefsX.GetBool("Level" + (i + 1).ToString()));
                    }
                    else
                    {
						Lock[i].gameObject.SetActive(true);
						Debug.Log(PlayerPrefsX.GetBool("Level" + (i + 1).ToString()));

                    }
                }
            }
        }
		public void LoadStars(){
			for (int i = 0; i < LevelStars.Length; i++) {
				for (int j = 0; j < PlayerPrefs.GetInt("Level" + (i + 1)); j++) {
					LevelStars [i].transform.GetChild(j).GetComponent<Image> ().sprite = Star;
				}
			}
		}

        // Update is called once per frame
     public void LevelSelected(int LevelSelected)
        {
			Debug.Log (PlayerPrefsX.GetBool ("Level" + LevelSelected));
			if (PlayerPrefsX.GetBool ("Level" + LevelSelected)) {
				
				CurrentSelectedLevel = LevelSelected - 1;
				GameManager.Instance.level = CurrentSelectedLevel;
				NExt.SetActive (true);
			}

            else if (PlayerPrefsX.GetBool("Level" + LevelSelected)  == false)
            {
                NExt.SetActive(false);
            }

			else if (GameManager.Instance.IsCoinBased && GameManager.Instance.LevelSelectionLockCoinBased)
            {
                CurrentSelectedLevel = LevelSelected-1;
                GameManager.Instance.level = CurrentSelectedLevel;
                UnlockLevelTab.SetActive(true);
                Debug.Log("Debugunlock");

                CurrentSelectedLevelCoin.text = GameManager.Instance.Gameplaylevel[LevelSelected - 1].Coins.ToString();
            }
        }
        public void UnlockLevel()
        {
			if(Preferences.Instance.Coins>= GameManager.Instance.Gameplaylevel[CurrentSelectedLevel].Coins)
            {
				Preferences.Instance.Coins -= GameManager.Instance.Gameplaylevel[CurrentSelectedLevel].Coins;
                PlayerPrefsX.SetBool("Level"+(CurrentSelectedLevel+1).ToString(),true);
 
                UnlockMessage.text = "Unlocked Successfully";
                StartCoroutine(waitforUnlockMessage(UnlockLevelTab.gameObject,3));
            }
            else
            {
                UnlockMessage.text = "Not Enough Coins";
            }
        }
       IEnumerator waitforUnlockMessage(GameObject Object,int time)
        {
            yield return new WaitForSeconds(time);
            Object.SetActive(false);
            UnlockMessage.text = "";
        }

        public void LoadScene(int Scene)
        {
			if (PlayerPrefsX.GetBool ("Level" + (CurrentSelectedLevel + 1).ToString ())) {
				GameManager.Instance.Play (Scene);
			}
        }
            

    }
}
