using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBloker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(0, 12,true);
    }
	
	// Update is called once per frame
	void Update () {
      
	}
    
}
