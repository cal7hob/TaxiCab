using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerMovement : MonoBehaviour {

    public GameObject playerVehcile;
    public Transform dropPoint;
    public Transform DropFootPath;
    public GameObject passengerMatChanging;
    public int materialNumber = 0;

    public List<Material> passengerMaterials;

    public Transform target;
    private Animator pedestrianAnimator;

    // Use this for initialization
    void Start()
    {
        playerVehcile = GameObject.FindGameObjectWithTag("Player");
        pedestrianAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);

        //if(materialNumber == 0)
        //    passengerMatChanging.GetComponent<Renderer>().material = passengerMaterials[0];
        //else if(materialNumber == 1)
        //    passengerMatChanging.GetComponent<Renderer>().material = passengerMaterials[1];
        //else if (materialNumber == 2)
        //    passengerMatChanging.GetComponent<Renderer>().material = passengerMaterials[2];

        //Debug.Log( passengerMatChanging.GetComponent<Renderer>().material);
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
