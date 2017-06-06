using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

namespace TinHead_Developer
{
    [System.Serializable]
    class Buttons
    {
        public Button Button;
        public int BindScene = 0;
    }
    public class ButtonBinder : MonoBehaviour
    {
        private static ButtonBinder instance = null;
        public static ButtonBinder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ButtonBinder>();
                }
                return instance;
            }
        }
        public void Awake()
        {
            BindButtons();
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


        [SerializeField]
        [Header("Name is the name of button \n Button is your button reference \n BindScene is the scene you want to bind with the Button")]

        Buttons[] Button;

        public void Start()
        {
    //        BindButtons();
        }


        public void BindButtons()
        {
            Debug.Log("Working");

            //Button[0].Button.onClick.AddListener(delegate ()
            //    {
            //        Debug.Log(1); 
            //       GameManager.Instance.LoadLevel(Button[0].BindScene);
            //    } );

            //    Button[1].Button.onClick.AddListener(delegate ()
            //    {
            //        Debug.Log(0);

            //        GameManager.Instance.LoadLevel(Button[1].BindScene);
            //    });
            

            //    Button[2].Button.onClick.AddListener(delegate ()
            //    {
            //        Debug.Log(2);
            //        GameManager.Instance.LoadLevel(Button[2].BindScene);
            //    });        
        }
    }
}

