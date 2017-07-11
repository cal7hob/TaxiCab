using System.Collections;
using System.Collections.Generic;
using TinHead_Developer;
using UnityEngine;
using UnityEngine.AI;
using TinHead_Developer;
public class PadestrianController : MonoBehaviour {
    private Animator thisAnimator;
    private Patrol thisPatrol;
    private NavMeshAgent thisNavMeshAgent;
    private BoxCollider thisCollider;

    // Use this for initialization
    void Start () {
        thisAnimator = GetComponent<Animator>();
        thisPatrol = GetComponent<Patrol>();
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
        thisCollider = GetComponent<BoxCollider>();

	}
	
	// Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Death();
        }
    }

    void Death()
    {
        LevelManager.Instance.PendestrianKilled++;
        thisPatrol.enabled = false;
        thisNavMeshAgent.enabled = false;
        thisAnimator.SetBool("Death_b", true);
        thisCollider.enabled = false;
		TinHead_Developer.SoundManager.Instance.PlaySound ("PersonDead");
    }
    void revive()
    {
        thisPatrol.enabled = true;
        thisNavMeshAgent.enabled = true;
        thisCollider.enabled = true;

    }
}
