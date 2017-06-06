using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MolevPathEditor : MonoBehaviour {

    // Use this for initialization
    public Color RayColor = Color.white;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] TheArray;

    void OnDrawGizmos()
    {
        Gizmos.color = RayColor;
        TheArray = GetComponentsInChildren<Transform>();
        path_objs.Clear();
        foreach(Transform path in TheArray)
        {
            if(path!=this.transform)
            {
                path_objs.Add(path);
            }
        }
        for( int i=0; i<path_objs.Count;i++)
        {
            Vector3 position = path_objs[i].position;
            if (i > 0)
            {
                Vector3 prev = path_objs[i - 1].position;
                Gizmos.DrawLine(prev, position);
                Gizmos.DrawWireSphere(position,0.5f);
            }

        }
    }

}
