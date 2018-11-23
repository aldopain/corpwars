using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
	public List<NavigationNode> Nodes;
	
	private NavMeshAgent agent;
	private int patrolDest;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(Nodes[0].transform.position);	
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.05f){
			agent.SetDestination(Nodes[patrolDest].transform.position);
			patrolDest = (patrolDest + 1) % Nodes.Count;
		}
	}

	public void Goto(Vector3 point){
		agent.SetDestination(point);	
	}
}
