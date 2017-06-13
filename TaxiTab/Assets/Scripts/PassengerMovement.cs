using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerMovement : MonoBehaviour {

    public GameObject playerVehcile;
    public Transform dropPoint;
    public Transform DropFootPath;

    public Transform target;
    private Animator pedestrianAnimator;

    // Use this for initialization
    void Start()
    {
        playerVehcile = GameObject.FindGameObjectWithTag("Player");
        pedestrianAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);



    }

    //// Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
    public void PickupPassenger()
    {
        pedestrianAnimator.SetBool("IsRunning", true);
        target = playerVehcile.transform;
        Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), false);
    }
    public void DropPassenger()
    {
        Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = dropPoint.transform.position;
        target = DropFootPath;
        pedestrianAnimator.SetBool("IsRunning", true);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
           col.GetComponent<PlayerManager>().StartCar();

            //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "FootPath")
        {
            Debug.Log("Passenger reached destination");
            pedestrianAnimator.SetBool("IsRunning", false);
           // Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), false);
            ChangeRoute();
        }


    }

    void ChangeRoute()
    {
        PassengerManager.Instance.route++;
        PassengerManager.Instance.singleInstance = true;
        PassengerManager.Instance.droppedPassengers++;
        Debug.Log(PassengerManager.Instance.droppedPassengers);
    }
}
