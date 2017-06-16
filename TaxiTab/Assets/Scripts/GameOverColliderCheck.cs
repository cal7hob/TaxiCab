using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameOver.Instance.EnableGameOverText();
        }
    }
}
