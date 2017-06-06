using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathToFollow : MonoBehaviour {

    public GameObject[] GetPaths;
	// Use this for initialization
	void Start () {
        int num = Random.Range(0,GetPaths.Length);
        transform.position = GetPaths[num].transform.position;
        PathMovement MyPath = GetComponent<PathMovement>();
        MyPath.pathName = GetPaths[num].name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
