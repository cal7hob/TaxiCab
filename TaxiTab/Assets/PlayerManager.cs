using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    LevelManager lalala;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "TrafficCar")
        {
            //Time - 10
            LevelManager.Instance.TimeDecrement();
        }
    }
}
