using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TinHead_Developer
{

    [System.Serializable]
    public class level
    {
        public GameObject Level=null;
        public bool IsPlayerSpawn = false;
        public bool IsDestination = false;

        public bool ContainInstructions = false;
        public bool TimeBased = false;
        public Transform SpawnPoint;
        public Transform Destination;
        public int time = 0;
        public int Objective = 1;
        public GameObject Instruction;
        public int Coins;
        public bool LevelLocked;

    }
		
    [System.Serializable]
    public class InGameUi {
        public GameObject LevelComplete;
        public GameObject LevelFailed;
        public GameObject LevelPaused;

		public GameObject LoadingScreen=null;
		public GameObject Cinematic=null;
		public GameObject HelpScreen=null;
    }

    [System.Serializable]
    public class SceneLoader
    {

      public  IEnumerator LoadScene(int SceneToLoad)
        {
            GameManager.Instance.CurrentScene = SceneToLoad;
            AsyncOperation async;
            async = SceneManager.LoadSceneAsync(0);
            yield return async;
        }
    }
}
