using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("destory", 5f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void destory()
    {
        GameObject.Destroy(this.gameObject);
    }
}
