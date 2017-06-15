using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    RCC_CarControllerV3 myPlayerCar;
    Rigidbody thisRigidBody; 

	// Use this for initialization
	void Start () {
        //cityCoordinates.Add(new PassengerPoints)
        myPlayerCar = GetComponent<RCC_CarControllerV3>();
        thisRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StopCar()
    {

        thisRigidBody.isKinematic = true;
        
    }
    public void StartCar()
    {

        thisRigidBody.isKinematic = false;

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "TrafficCar")
        {
            Debug.Log("Collided with the Car! Time Decreased");
            //Time - 10
            HUDManager.Instance.carHited++;
            LevelManager.Instance.TimeDecrement();
           
        }
    }
}
