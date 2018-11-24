using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
	public List<NavigationNode> Nodes;
	public SpriteRenderer StatusSprite;
	public bool ReverseOnCompletion;

	private NavMeshAgent agent;
	private int dest;
	private int direction = 1;

	void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
		agent.SetDestination(Nodes[0].transform.position);	
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.05f && Nodes.Count != 0){
			GotoNextNode();
		}
	}

	void GotoNextNode(){
		int oldDest = dest;

		if(ReverseOnCompletion){
			if(dest == Nodes.Count - 1){
				direction = -1;
			}else if(dest == 0){
				direction = 1;
			}
			dest += direction;
		}else{
			dest = (dest + 1) % Nodes.Count;
		}

		if(Nodes[oldDest].Neighbours.Contains(Nodes[dest])){
			agent.SetDestination(Nodes[dest].transform.position);
		}else{
			agent.isStopped = true;
			StatusSprite.gameObject.SetActive(true);
		}
	}

	public void Goto(Vector3 point){
		agent.SetDestination(point);	
	}

	public void SetSpeed(float s){
		agent.speed = s;
	}
}
