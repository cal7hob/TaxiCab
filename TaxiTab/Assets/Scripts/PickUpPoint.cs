using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoint : MonoBehaviour {

     public GameObject pedestrian; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerManager>().StopCar();
            //pedestrianAnimator.SetBool("IsRunning", true);
            pedestrian.GetComponent<PassengerMovement>().PickupPassenger();

            Debug.Log("hi");
            PassengerManager.Instance.DroppingPoint.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
