using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinHead_Developer
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager instance = null;
        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LevelManager>();
                }
                return instance;
            }
        }
        int TotalTime;
        public GameState GameStatus;

        public GameObject[] Players;
        public GameObject Player;
        public int CurrentPlayer = 0;
        public GameObject Destination;
        public Text TimeUI;

        [Range(0,int.MaxValue)]
        private int CurrentTime;
        private int minutes;
        private int seconds;
		public RCC_Camera Camera;
        public InGameUi InGameUi;
        public GameObject MiniMap;

        private int pendestrianKilled;
		public GameObject[] LevelCompleteStars;
        public int PendestrianKilled
        {
            get
            {
                return pendestrianKilled;
            }
            set
            {
                pendestrianKilled = value;

                if (pendestrianKilled >= 2)
                {

                    Invoke("GameFailed", 2);
                }
         
            }
        }
        public void TimeDecrement()
        {
            Debug.Log("Time Decrement Called");
            //seconds -= 10;
            Debug.Log(CurrentTime);
            CurrentTime -= 10;
            TimeUI.GetComponent<Animator>().SetTrigger("isTimeDecrement");
           
        }
        public int CalculatedRemainingTime()
        {

            
            // return (CurrentTime /) * 100;
            int temp = (int)(((float)CurrentTime / TotalTime) * 100); 
            Debug.Log(temp);

            return temp;

        }

        public int objective
        {
            get
            {
                return GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Objective;
            }
            set
            {
                GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Objective = value;
                PassengerManager.Instance.PassengerCounter++;
                PassengerManager.Instance.route++;
                PassengerManager.Instance.PassengerRouteSpawner();
                if (GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Objective <= 0)
                  StartCoroutine(  GameComplete());
               }
        }
        //From here on the functions are scripted and no more variable declaration is done




            
            public void Awake()
        {
            
            ActivateLevel();
            PlaceStartingData();
        }

        void Start()
        {
			ConsoliAds.Instance.HideBanner ();
			SoundManager.Instance.PlaySound ("GamePlay");
       //     EventManager.StartGame();

        }
        public void ActivateLevel()
        {

            Instantiate(GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Level);
            ChangeDayNighy(GameManager.Instance.level);
            EventManager.StatusEvent("Instruction");
            TotalTime  = CurrentTime = GameManager.Instance.Gameplaylevel[GameManager.Instance.level].time;
			CurrentPlayer = GameManager.Instance.SelectedCar;
            //   EventManager.GameStatus += CheckGameStatus;

        }
        public void PlaceStartingData()
        {         
            //Player StartingData
            Player = Instantiate(Players[CurrentPlayer]);
            GameObject.FindObjectOfType<bl_MiniMap>().m_Target = Player;
            Camera.playerCar = Player.transform;
            if (GameManager.Instance.Gameplaylevel[GameManager.Instance.level].IsPlayerSpawn == true)
            {
                Player.transform.position = GameManager.Instance.Gameplaylevel[GameManager.Instance.level].SpawnPoint.transform.position;
                Player.transform.rotation = GameManager.Instance.Gameplaylevel[GameManager.Instance.level].SpawnPoint.transform.rotation;

            }
			if (GameManager.Instance.Gameplaylevel [GameManager.Instance.level].IsDestination == true) {
				GameObject G = Instantiate(Destination);
				G.transform.position= GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Destination.transform.position;
			}

            //TimeStartingData

            if (GameManager.Instance.Gameplaylevel[GameManager.Instance.level].TimeBased)
            {
                InvokeRepeating("TimeStart", 0.0f, 1.0f);
            }
        }
        public void ChangeDayNighy(int level)
        {
            if (level == 1)
            {



            }
        }
        public void CheckGameStatus(string Status)
        {
            switch (Status)
            {
                case "Instruction":
                    if (GameManager.Instance.Gameplaylevel[GameManager.Instance.level].ContainInstructions)
                    {
                        Time.timeScale = 1;
					Instantiate(GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Instruction);
                        Debug.Log(Status);
                        return;
                    }
                    break;

                case "Paused":
                    Time.timeScale = 0;
                    GamePaused();
                    break;
                case "LevelComplete":
                    Time.timeScale = 0;
                    GameComplete();
                    break;
                case "LevelFailed":
                    Time.timeScale = 0;
                    GameFailed();
                    break;
                case "Cinematic":                
                    Cinematic();
                    break;
                default:
                    Debug.LogWarning("Something not right with the instruction");
                    break;
            }
        }

        public void TimeStart()
        {
            CurrentTime -= 1;
            minutes = CurrentTime / 60;
            seconds = CurrentTime % 60;

            if(CurrentTime <= 0)
            {
                CancelInvoke("TimeStart");
                Invoke("GameFailed", 2);
                return;
            }

            TimeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            //if (minutes != 0 && seconds != 0)
            //{
            //    TimeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            //}
            //else if (minutes == 0 && seconds == 0)
            //{
            //    CancelInvoke();
            //    Invoke("GameFailed", 2);
            //}
            ////else
            ////    CancelInvoke();

            ////else
            ////{
            ////    CancelInvoke();
            ////    Invoke("GameFailed", 2);
            ////}
        }

        public void ShowAd(int ID)
        {
			ConsoliAds.Instance.ShowInterstitial (ID);
        }

        IEnumerator GameComplete()
        {
            yield return new WaitForSeconds(2.0f);
            InGameUi.LevelComplete.SetActive(true);
            HUDManager.Instance.CalculateStars();

			PlaySound ("LevelComplete");
			PlayerPrefsX.SetBool("Level" + (GameManager.Instance.level + 2).ToString(), true);
			if (PlayerPrefs.GetInt ("Level" + (GameManager.Instance.level + 1))< HUDManager.Instance.stars  ) {
				PlayerPrefs.SetInt("Level" + (GameManager.Instance.level + 1).ToString(), HUDManager.Instance.stars);
				Preferences.Instance.TotalStars = Preferences.Instance.TotalStars+(HUDManager.Instance.stars -PlayerPrefs.GetInt ("Level" + (GameManager.Instance.level + 1)));
			}
			for (int i = 0; i < HUDManager.Instance.stars; i++) {
				LevelCompleteStars [i].SetActive (true);
				yield return new WaitForSeconds (1.0f);
			}
            if (GameManager.Instance.IsCoinBased)
            {
				//
            }
                ShowAd(3);
        }
        public void GameFailed()
        {
			PlaySound ("LevelFailed");
			Time.timeScale = 0.001f;
            InGameUi.LevelFailed.SetActive(true);

                ShowAd(4);
        }
        public void GamePaused()
        {
			PlaySound ("Click");
			Time.timeScale = 1f; //Level Paused Changing here (Anjum)
            InGameUi.LevelPaused.SetActive(true);

               ShowAd(2);
        }
        public void Cinematic()
        {

        }
		public void Restart(){
			PlaySound ("Click");

			Time.timeScale = 1;
			GameManager.Instance.Play (3);


		}
		public void Resume(){
			PlaySound ("Click");
			Time.timeScale = 1.0f;
			InGameUi.LevelPaused.SetActive(false);
		}

		public void MainMenu(){
			PlaySound ("Click");

			GameManager.Instance.Play (0);
		}
		public void NextLevel(){
			PlaySound ("Click");
			Time.timeScale = 1;
			GameManager.Instance.level ++;  //Next Level by clicking nextBtn
            Debug.Log("Next Level Number" +GameManager.Instance.level);
			GameManager.Instance.Play (3);

		}
		public void PlaySound(string Sound){
			SoundManager.Instance.PlaySound (Sound);
		}
    }
}
