using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    //SingleTon
    private static HUDManager instance = null;
    public static HUDManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HUDManager>();
            }
            return instance;
        }
    }
    //StarsRelatedVariables
    public int stars;
    public int carHited;
    public int passangerCounter;

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public  void CalculateStars()
    {
        //Star for completing the level
        stars++;
        //ifRemaingTimeiSGraterthan30%
        Debug.Log(LevelManager.Instance.CalculatedRemainingTime() + "remainingTime");
        if (LevelManager.Instance.CalculatedRemainingTime() >= 30)
        {
            Debug.Log("RemaingTimeStarAdded");
           stars++;
        }
        
        if (carHited == 0)
        {
           stars++;

        }
        Debug.Log(HUDManager.Instance.stars + "Stars");
    }
   public void NumberOfPassangerDroped(int passenger)
    {
        passangerCounter = passenger;
       
    }
}
