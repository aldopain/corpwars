using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

	public Dropdown Destination;
	public Dropdown NewCargoType;
	public Slider NewCargoAmount;
	
	public void FillDests(){
		Destination.ClearOptions();
		List<string> destNames = new List<string>();
		foreach(LocalEconomy le in GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalEconomy>().TradePosts){
			destNames.Add(le.name);
		}

		Destination.AddOptions(destNames);
	}

	public void AddStop(){
		Stop ns = new Stop();
		ns.node = GameObject.Find(Destination.options[Destination.value].text).GetComponent<NavigationNode>();
		ns.Load = new List<Cargo>();
		ns.Unload = new List<Cargo>();
		if(NewCargoAmount.value > 0){
			ns.Load.Add(new Cargo((Cargo.CargoType)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
		}else{
			ns.Unload.Add(new Cargo((Cargo.CargoType)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
		}

		Stops.Add(ns);

		if(Stops.Count == 1){
			agent.SetDestination(Stops[0].node.transform.position);	
		}
	}

	void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
		FillDests();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.05f && Stops.Count != 0 && !isTrading){
			if(Stops[dest].Load.Count == 0 && Stops[dest].Unload.Count == 0){
				GotoNextNode();
			}else{
				StartCoroutine(Trade());
			}
		}
	}

	IEnumerator Trade(){
		isTrading = true;
		yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("GameController").GetComponent<Time>().DayLength * (Stops[dest].Load.Count + Stops[dest].Unload.Count));
		for(int i = 0; i < Stops[dest].Unload.Count; i++){
			Stops[dest].node.GetComponent<LocalEconomy>().Add(Stops[dest].Unload[i]);
			GetComponent<UnitInventory>().RemoveCargo(Stops[dest].Unload[i]);
		}
		
		for(int i = 0; i < Stops[dest].Load.Count; i++){
			Stops[dest].node.GetComponent<LocalEconomy>().Remove(Stops[dest].Load[i]);
			GetComponent<UnitInventory>().AddCargo(new Cargo(Stops[dest].Load[i]));
		}

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
}
