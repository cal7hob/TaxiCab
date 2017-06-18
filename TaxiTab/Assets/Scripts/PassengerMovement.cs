using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinHead_Developer;
public class PassengerMovement : MonoBehaviour {

    public Transform dropPoint;
    public Transform DropFootPath;

    public Transform target;
    private Animator pedestrianAnimator;
    public GameObject ParticalFx;

    // Use this for initialization
    void Start()
    {
       // LevelManager.Instance.Player = LevelManager.Instance.Player;
        pedestrianAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(LevelManager.Instance.Player.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
    }

    //// Update is called once per frame
    void Update()
    {
      //  transform.LookAt(target);
    }
    public void PickupPassenger()
    {   
        transform.LookAt(LevelManager.Instance.Player.transform);
        pedestrianAnimator.SetBool("IsRunning", true);
        target = LevelManager.Instance.Player.transform;
        Physics.IgnoreCollision(LevelManager.Instance.Player.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), false);
    }
    public void DropPassenger()
    {
        this.gameObject.transform.rotation = Quaternion.identity;
        Physics.IgnoreCollision(LevelManager.Instance.Player.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), true);
        this.gameObject.SetActive(true);
        this.gameObject.transform.position =dropPoint.transform.position;
        target = DropFootPath;
        transform.LookAt(target);
        pedestrianAnimator.SetBool("IsRunning", true);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
           col.GetComponent<PlayerManager>().StartCar();

            //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Instantiate(ParticalFx, new Vector3(this.transform.position.x, 5, this.transform.position.z), Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "FootPath")
        {
            Debug.Log("Passenger reached destination");
            pedestrianAnimator.SetBool("IsRunning", false);
            // Physics.IgnoreCollision(playerVehcile.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>(), false);
            Instantiate(ParticalFx, new Vector3(this.transform.position.x,5, this.transform.position.z), Quaternion.identity);
            ChangeRoute();
        }

    }

    void ChangeRoute()
    {   
        PassengerManager.Instance.droppedPassengers++;
        LevelManager.Instance.objective--;
        HUDManager.Instance.NumberOfPassangerDroped(PassengerManager.Instance.droppedPassengers);
        Debug.Log(PassengerManager.Instance.droppedPassengers);
    }
}
