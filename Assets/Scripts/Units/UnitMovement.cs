using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
	[System.Serializable]
	public struct Stop{
		public NavigationNode node;
		public List<Cargo> Load;
		public List<Cargo> Unload;
	}

	public List<Stop> Stops;
	public SpriteRenderer StatusSprite;
	public bool ReverseOnCompletion;

	private NavMeshAgent agent;
	private int dest;
	private int direction = 1;

	bool isTrading;
	
	void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(agent.enabled){
			if(agent.remainingDistance < 0.05f && !isTrading){
				if(Stops.Count != 0){
					if(Stops[dest].Load.Count == 0 && Stops[dest].Unload.Count == 0){
						GotoNextNode();
					}else{
						StartCoroutine(Trade());
					}
				}else{
					WaitInTown();
				}
			}
		}
	}

	IEnumerator Trade(){
		isTrading = true;
		agent.enabled = false;
		yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("GameController").GetComponent<Time>().DayLength * (Stops[dest].Load.Count + Stops[dest].Unload.Count));
		for(int i = 0; i < Stops[dest].Unload.Count; i++){
			Stops[dest].node.GetComponent<LocalEconomy>().Add(Stops[dest].Unload[i]);
			GetComponent<UnitInventory>().RemoveCargo(Stops[dest].Unload[i]);
		}
		
		for(int i = 0; i < Stops[dest].Load.Count; i++){
			Stops[dest].node.GetComponent<LocalEconomy>().Remove(Stops[dest].Load[i]);
			GetComponent<UnitInventory>().AddCargo(new Cargo(Stops[dest].Load[i]));
		}

		agent.enabled = true;
		GotoNextNode();
		isTrading = false;		
	}


	void GotoNextNode(){
		int oldDest = dest;

		if(ReverseOnCompletion){
			if(dest == Stops.Count - 1){
				direction = -1;
			}else if(dest == 0){
				direction = 1;
			}
			dest += direction;
		}else{
			dest = (dest + 1) % Stops.Count;
		}

		if(Stops[oldDest].node.Neighbours.Contains(Stops[dest].node)){
			agent.SetDestination(Stops[dest].node.transform.position);
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

	public void WaitInTown(){
		Destroy(gameObject);
	}
}
