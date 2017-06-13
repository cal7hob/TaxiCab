using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoint : MonoBehaviour {

    public Animator pedestrianAnimator;
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
            pedestrianAnimator.SetFloat("Speed_f", 0.2f);
            Debug.Log("hi");
            PassengerManager.Instance.DroppingPoint.SetActive(true);
            //this.gameObject.SetActive(false);
        }
    }
}
