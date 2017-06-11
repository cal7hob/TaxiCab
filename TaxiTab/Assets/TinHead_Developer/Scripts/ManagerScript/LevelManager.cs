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

        public GameState GameStatus;

        public GameObject[] Players;
        public GameObject Player;
        public int CurrentPlayer = 0;
        public GameObject Destination;
        public Text TimeUI;

        private int CurrentTime;
        private int minutes;
        private int seconds;
		public RCC_Camera Camera;
        public InGameUi InGameUi;


        public void TimeDecrement()
        {
            Debug.Log("Time Decrement Called");
            //seconds -= 10;
            CurrentTime -= 10;
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
                if (GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Objective <= 0)
                    GameComplete();
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
            EventManager.StartGame();

        }

        void Update()
        {

        }
        public void ActivateLevel()
        {

            Instantiate(GameManager.Instance.Gameplaylevel[GameManager.Instance.level].Level);
            EventManager.StatusEvent("Instruction");
            CurrentTime = GameManager.Instance.Gameplaylevel[GameManager.Instance.level].time;
            //   EventManager.GameStatus += CheckGameStatus;

        }
        public void PlaceStartingData()
        {         
            //Player StartingData
			CurrentPlayer=GameManager.Instance.SelectedCar;
            GameObject Player = Instantiate(Players[CurrentPlayer]);
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

           TimeUI.text=string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void ShowAd(int ID)
        {
            
        }

        public void GameComplete()
        {
            InGameUi.LevelComplete.SetActive(true);
            if(GameManager.Instance.IsCoinBased)
            {

            }
            //    ShowAd();
        }
        public void GameFailed()
        {
            InGameUi.LevelFailed.SetActive(true);

            //    ShowAd();
        }
        public void GamePaused()
        {
            InGameUi.LevelPaused.SetActive(true);

            //    ShowAd();
        }
        public void Cinematic()
        {

        }
		public void Restart(){

		}
		public void Resume(){

		}
		public void Pause(){
		}
		public void MainMenu(){


		}

    }
}
