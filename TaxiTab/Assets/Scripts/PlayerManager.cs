using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //cityCoordinates.Add(new PassengerPoints)
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "TrafficCar")
        {
            Debug.Log("Collided with the Car! Time Decreased");
            //Time - 10
            LevelManager.Instance.TimeDecrement();
        }
    }
}
