using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Actor_CaravanMovement : MonoBehaviour {
	public Transform[] Route;
	public UnityEvent OnArrival;

	int currentRoutePoint;
	[HideInInspector]
	public NavMeshAgent agent;

	void Awake(){
		agent = GetComponent<NavMeshAgent>();
		GotoNextPoint();
	}

	void Update(){
		if(agent.enabled){
			if(agent.remainingDistance < 0.5f){
				_OnArrival();
			}	
		}
	}

	///<summary>
	///Move attached NavMeshAgent to the next town
	///</summary>
	void GotoNextPoint(){
		if(agent.enabled){
			currentRoutePoint = (currentRoutePoint + 1) % Route.Length;
			agent.SetDestination(Route[currentRoutePoint].position);
		}
	}

	void _OnArrival(){
		GetComponent<Actor_TradeController>().Trade(currentRoutePoint);
		GotoNextPoint();
		OnArrival.Invoke();
	}
}
