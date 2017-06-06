using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour {

    public MolevPathEditor PathToFollow;

    public int CurrentWayPointID = 0;
    public float speed = 1.0f;
    public float rotationspeed = 5.0f;
    public string pathName;
    float ReachDistance = 1.0f;
    Vector3 Last_position;
    Vector3 Current_position;
    float distance = 0.0f;
	// Use this for initialization
	void Start () {
        PathToFollow = GameObject.Find(pathName).GetComponent<MolevPathEditor>();
        Last_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position,transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*rotationspeed);
        if (distance<=ReachDistance)
        {
            CurrentWayPointID++;
        }
        else if(CurrentWayPointID>=PathToFollow.path_objs.Count)
        {
            CurrentWayPointID = 0;
        }

    }
}
