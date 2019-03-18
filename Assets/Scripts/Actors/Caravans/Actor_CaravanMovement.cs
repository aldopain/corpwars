using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Actor_CaravanMovement : MonoBehaviour {
	public NavigationRoute Route;
    /// <summary>
    /// Invokes when this actor arrives at the next Route Point
    /// </summary>
    public UnityEvent OnArrival;
    public bool Repeat;


	public NavigationStop currentRoutePoint;
    int StopCounter = 0;
	[HideInInspector]
	public NavMeshAgent agent;

	void Awake(){
		agent = GetComponent<NavMeshAgent>();
		GotoNextPoint();
	}

	void Update(){
		if(agent.enabled){
			if(agent.remainingDistance < 0.5f && !agent.isStopped){
				_OnArrival();
			}	
		}
	}

	///<summary>
	///Move attached NavMeshAgent to the next town
	///</summary>
	void GotoNextPoint(){
        currentRoutePoint = Route.Next();
        if (currentRoutePoint != null){
            agent.SetDestination(currentRoutePoint.Point.transform.position);
        } else if (Repeat) {
            GotoNextPoint();
        } else {
            // here some checks, events, etc.
        }
    }

    /// <summary>
    /// Gets called when actor arrives at the next Route Point
    /// </summary>
	void _OnArrival(){
        if (currentRoutePoint.Trade){
            StopCounter++;
            GetComponent<Actor_TradeController>().Trade(currentRoutePoint.Point, StopCounter);
            StartCoroutine(Wait(GetComponent<Actor_TradeController>().StopLength(StopCounter)));
        }
        GotoNextPoint();
		OnArrival.Invoke();
	}

    IEnumerator Wait(float seconds)
    {
        agent.isStopped = true;
        //GetComponent<MeshRenderer>().enabled = false; 
        transform.Find("Model").gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        //GetComponent<MeshRenderer>().enabled = true;
        transform.Find("Model").gameObject.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }
}
