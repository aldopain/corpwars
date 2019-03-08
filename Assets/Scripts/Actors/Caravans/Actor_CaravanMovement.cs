using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Actor_CaravanMovement : MonoBehaviour {
	public Transform[] Route;
    /// <summary>
    /// Invokes when this actor arrives at the next Route Point
    /// </summary>
    public UnityEvent OnArrival;
    public bool ReverseOnCompletion;


	int currentRoutePoint;
    int routeDirection = 1;
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
        int oldDest = currentRoutePoint;

        if (ReverseOnCompletion)
        {
            if (currentRoutePoint == Route.Length - 1)
            {
                routeDirection = -1;
            }
            else if (currentRoutePoint == 0)
            {
                routeDirection = 1;
            }
            currentRoutePoint += routeDirection;
        }
        else
        {
            currentRoutePoint = (currentRoutePoint + 1) % Route.Length;
        }

        if (isNextPointValid(oldDest))
        {
            agent.SetDestination(Route[currentRoutePoint].position);
        }
        else
        {
            agent.isStopped = true;
            Debug.LogErrorFormat("{0} cannot move to the next point, as it isn't connected to the current one", name);
        }
    }

    /// <summary>
    /// Gets called when actor arrives at the next Route Point
    /// </summary>
	void _OnArrival(){
		GetComponent<Actor_TradeController>().Trade(currentRoutePoint);
        StartCoroutine(Wait(GetComponent<Actor_TradeController>().StopLength(currentRoutePoint)));
        GotoNextPoint();
		OnArrival.Invoke();
	}

    IEnumerator Wait(float seconds)
    {
        agent.isStopped = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    bool isNextPointValid(int prevPoint)
    {
        return Route[prevPoint].GetComponent<NavigationNode>().Neighbours.Contains(Route[currentRoutePoint].GetComponent<NavigationNode>());
    }
}
