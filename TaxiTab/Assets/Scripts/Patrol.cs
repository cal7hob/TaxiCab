using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

	public Color RayColor = Color.white;
	public Transform[] points;
	public int destPoint = 0;
	private NavMeshAgent agent;


	void Start () {
		agent = GetComponent<NavMeshAgent>();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;

		GotoNextPoint();
	}


	void GotoNextPoint() {
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}


	void Update () {
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!agent.pathPending && agent.remainingDistance < 0.5f)
			GotoNextPoint();
	}


	void OnDrawGizmos()
	{
		Gizmos.color = RayColor;
		for( int i=0; i<points.Length;i++)
		{
			Vector3 position = points[i].position;
			if (i > 0)
			{
				Vector3 prev = points[i - 1].position;
				Gizmos.DrawLine(prev, position);
				Gizmos.DrawWireSphere(position,0.5f);
			}
		}
	}
}