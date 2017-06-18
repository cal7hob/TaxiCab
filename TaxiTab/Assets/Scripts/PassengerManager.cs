using System;
using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{

    int totalPassengers = 1;
    public int droppedPassengers;

    public static System.Random rnd;
    public int PassengerCounter = 0;
    public GameObject PickUpPoint;
    public GameObject DroppingPoint;
    public GameObject DropFootPath;
    public GameObject passengerSingle;

    public List<Material> passengersList;


    public List<Transform> startingCoordinates;
    public List<Transform> endingCoordinates;
    public List<Transform> pickUpFootPathPoints;
    public List<Transform> dropFootPathPoints;
    //TODO;
    //public List<GameObject> passengers;

    public int route;
    private static PassengerManager instance = null;
    public static PassengerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PassengerManager>();
            }
            return instance;
        }
    }
    //public void Awake()
    //{
    //    if (instance)
    //    {
    //        DestroyImmediate(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        instance = this;
    //    }
    //}

 
    public void PassengerRouteSpawner()
    {
        if (PassengerCounter < startingCoordinates.Count)
        {
            PickUpPoint.transform.position = startingCoordinates[PassengerCounter].position;
            PickUpPoint.GetComponent<bl_MiniMapItem>().ShowItem();
            PickUpPoint.SetActive(true);
            DroppingPoint.transform.position = endingCoordinates[PassengerCounter].position;
            DroppingPoint.GetComponent<bl_MiniMapItem>().HideItem();
            DroppingPoint.SetActive(false);
            passengerSingle.transform.position = pickUpFootPathPoints[PassengerCounter].position;

            DropFootPath.transform.position = dropFootPathPoints[PassengerCounter].position;
            var temp = rnd.Next(0, 5);
            Debug.Log(temp + "Random");
            passengerSingle.GetComponentInChildren<Renderer>().material = passengersList[temp];
        }
    }
    // Use this for initialization
    void Start()
    {
     //   PickUpPoint = GameObject.FindGameObjectWithTag("Pickup");
     //   DroppingPoint = GameObject.FindGameObjectWithTag("DropPoint");
        rnd = new System.Random();

        PassengerRouteSpawner();
        DroppingPoint.SetActive(false);

    }

    //TODO:
    // Update is called once per frame
    //   void Update() {
    //       if (droppedPassengers == totalPassengers)
    //       {
    //           LevelComplete();
    //       }
    //}  
}
