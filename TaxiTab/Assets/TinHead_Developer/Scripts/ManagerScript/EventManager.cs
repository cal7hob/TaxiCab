using UnityEngine;
using System.Collections;


namespace TinHead_Developer
{
    public class EventManager : MonoBehaviour
    {

        public delegate void Game();
        public static event Game GameStart;

        public delegate void Status(string Status);
        public static event Status GameStatus;

        public static void StartGame()
        {
            if (GameStart != null)
            {
                GameStart();
            }
        }
        public static void StatusEvent(string Status)
        {
            if(GameStatus!=null)
            {
                GameStatus(Status);
            }
        }

    }
}
