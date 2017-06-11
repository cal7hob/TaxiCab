using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{

    int totalPassengers = 1;
    public int droppedPassengers;

    public GameObject PickUpPoint;
    public GameObject DroppingPoint;

    public bool singleInstance = true;
    public List<Transform> startingCoordinates;
    public List<Transform> endingCoordinates;

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

    void Update()
    {
        if (singleInstance)
        {
            if (route == 0)
            {
                PickUpPoint.transform.position = startingCoordinates[0].position;
                PickUpPoint.SetActive(true);
                DroppingPoint.transform.position = endingCoordinates[0].position;
                DroppingPoint.SetActive(false);

            }

            else if (route == 1)
            {
                PickUpPoint.transform.position = startingCoordinates[1].position;
                PickUpPoint.SetActive(true);
                DroppingPoint.transform.position = endingCoordinates[1].position;
                DroppingPoint.SetActive(false);

            }
            else if (route == 2)
            {
                PickUpPoint.transform.position = startingCoordinates[2].position;
                PickUpPoint.SetActive(true);
                DroppingPoint.transform.position = endingCoordinates[2].position;
                DroppingPoint.SetActive(false);

            }

            else if (route == 3)
            {
                DroppingPoint.SetActive(false);
                LevelComplete();
            }

            singleInstance = false;
        }

    }

    // Use this for initialization
    void Start()
    {
        PickUpPoint = GameObject.FindGameObjectWithTag("Pickup");
        DroppingPoint = GameObject.FindGameObjectWithTag("DropPoint");
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

    private void LevelComplete()
    {
        Debug.Log("Level Completed");
    }
}
