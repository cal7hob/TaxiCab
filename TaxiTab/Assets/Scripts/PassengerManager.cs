using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour {

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
    public void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

      
    }

    int totalPassengers = 1;
  public  int droppedPassengers;

    public GameObject PickUpPoint;
    public GameObject DroppingPoint;

	// Use this for initialization
	void Start () {
        PickUpPoint = GameObject.FindGameObjectWithTag("Pickup");
        DroppingPoint = GameObject.FindGameObjectWithTag("DropPoint");
        DroppingPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (droppedPassengers == totalPassengers)
        {
            LevelComplete();
        }
	}

    private void LevelComplete()
    {
        Debug.Log("Level Completed");
    }
}
