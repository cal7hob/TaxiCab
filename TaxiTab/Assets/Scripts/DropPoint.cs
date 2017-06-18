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
            GetComponent<bl_MiniMapItem>().HideItem();
            this.gameObject.SetActive(false);
            //Invoke("ChangeRoute", 2f);
           
           Invoke("RestartCar", 2f);

        }
    }

    void RestartCar()
    {
        Car.StartCar();

    }

   
}
