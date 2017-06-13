using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    public GameObject pedestrian;
    private PlayerManager Car;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pedestrian.GetComponent<PassengerMovement>().DropPassenger();
            Car = other.GetComponent<PlayerManager>();
            Car.StopCar();
            Invoke("ChangeRoute", 10f);
            Invoke("RestartCar", 2f);
            
        }
    }

    void RestartCar()
    {
        Car.StartCar();

    }

    void ChangeRoute()
    {
        PassengerManager.Instance.route++;
        PassengerManager.Instance.singleInstance = true;
        PassengerManager.Instance.droppedPassengers++;
        Debug.Log(PassengerManager.Instance.droppedPassengers);
    }
}
