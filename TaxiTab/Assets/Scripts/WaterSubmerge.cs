using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSubmerge : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("PlayerDrown",10f); 
        }

    }
   

    void PlayerDrown()
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        Invoke("PlayerDrown", 1f);

    }
    void notPlayerDrown()
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
