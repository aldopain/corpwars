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

	public NavigationNode currentRoutePoint;
    int StopCounter = 0;
	[HideInInspector]
	public NavMeshAgent agent;

	void Start(){
		agent = GetComponent<NavMeshAgent>();
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Town");
        Route = new NavigationRoute(currentRoutePoint, tmp[Random.Range(0, tmp.Length)].GetComponent<NavigationNode>());
        Route.LoopRoute();
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
            agent.SetDestination(currentRoutePoint.transform.position);
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
        // possible refactoring: get i from Route, because it is equal to IndexOf(currentRoutePoint)
        var index = Route.IndexOf(currentRoutePoint);
        GetComponent<Actor_TradeController>().Trade(currentRoutePoint, index);
        StartCoroutine(Wait(GetComponent<Actor_TradeController>().StopLength(index)));
        GotoNextPoint();
		OnArrival.Invoke();
	}

    public void ChangeIsStopped(bool b){
        agent.isStopped = b;
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
