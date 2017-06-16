using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
    public GameObject gameOverImage;
    private static GameOver instance = null;
    public static GameOver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameOver>();
            }
            return instance;
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableGameOverText()
    {
        gameOverImage.SetActive(true);
    }
}
