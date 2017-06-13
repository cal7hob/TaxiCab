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
        target = playerVehcile.transform;
    }

    //// Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }

    public void DropPassenger()
    {
        Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = dropPoint.transform.position;
        target = DropFootPath;
        pedestrianAnimator.SetFloat("Speed_f", 0.2f);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerManager>().StartCar();

            //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "DropFootPath")
        {
            Debug.Log("Passenger reached destination");
            pedestrianAnimator.SetFloat("Speed_f", 0f);
            Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), false);

        }
    }
}
