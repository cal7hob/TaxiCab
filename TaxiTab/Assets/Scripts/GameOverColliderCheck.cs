using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinHead_Developer;
public class GameOverColliderCheck : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit game over");
        if(other.gameObject.tag == "Player")
        {
           // if (other.gameObject.GetComponent<RCC_CarControllerV3>().speed>= 30)
            {
                LevelManager.Instance.GameFailed();
            }
            
        }
    }
}
