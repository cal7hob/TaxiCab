using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TinHead_Developer
{
    public class GameManager : MonoBehaviour
    {
        private SceneLoader M_SceneManager;

        [HideInInspector]
        public level[] Gameplaylevel;

        [HideInInspector]
        public bool IsLockedBasedGame = false;
		[HideInInspector]
		public bool LevelSelectionLockCoinBased = false;

		[HideInInspector]
		public int SelectedCar=0;

		[HideInInspector]
		public bool GameContainsRewardedVideo = false;

        [HideInInspector]
        public bool IsCoinBased = false;

        [HideInInspector]
        public int TotalScene = 0;
        public int level = 0;
        public int CurrentScene = 0;
        public GameObject[] Scenes;
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }
                return instance;
            }
        }
        public void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
        }

        public void Start()
        {
            //manager for loading our scenes
            M_SceneManager = new SceneLoader();

            //when the game run for the first time
           if (Preferences.Instance.firsttime)
                SetLevelPref();
            
            //instantitates a scene when the game game loads first time in everyplay
            Instantiate(Scenes[CurrentScene]);

        }
        // Use this for initialization
        void OnLevelWasLoaded()
        {
            //when ever a scene is loaded we instantiate a new scene within a single gameplay
            Instantiate(Scenes[CurrentScene]);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Play(int scene)
        {
            StartCoroutine(M_SceneManager.LoadScene(scene));

        }
      void SetLevelPref()
        {
            for (int i = 0; i <TotalScene; i++)
            {
                if (Gameplaylevel[i].LevelLocked)
                {
                    PlayerPrefsX.SetBool("Level" + (i + 1).ToString(), false);
                    Debug.Log(PlayerPrefsX.GetBool("Level" + (i + 1).ToString()));
                }
                else
                {
                    PlayerPrefsX.SetBool("Level" + (i + 1).ToString(), true);
                }
            }
            Preferences.Instance.firsttime = false;
        }


    }
}


