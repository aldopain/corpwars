using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaravanSpawner : MonoBehaviour {
	public List<UnitMovement.Stop> Stops;

	public Text RouteDescription;
	public Dropdown Destination;
	public Dropdown NewCargoType;
	public Slider NewCargoAmount;

	public GameObject UnitPrefab;
	void Start () {
		Destination.ClearOptions();
		FillDests();
	}
	
	void Update () {
		
	}

	public void FillDests(){
		List<string> destNames = new List<string>();
		foreach(LocalEconomy le in GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalEconomy>().TradePosts){
			destNames.Add(le.name);
		}

		Destination.AddOptions(destNames);
	}

	public void AddStop(){
		UnitMovement.Stop ns = new UnitMovement.Stop();
		ns.node = GameObject.Find(Destination.options[Destination.value].text).GetComponent<NavigationNode>();
		ns.Load = new List<Cargo>();
		ns.Unload = new List<Cargo>();
		string descrtiption = Destination.options[Destination.value].text + "; ";
		if(NewCargoAmount.value > 0){
			ns.Load.Add(new Cargo((TradeResource.Types)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
			descrtiption += "Load " + NewCargoAmount.value + " of " + ((TradeResource.Types)NewCargoType.value).ToString();
		}else if(NewCargoAmount.value < 0){
			ns.Unload.Add(new Cargo((TradeResource.Types)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
			descrtiption += "Unload " + Mathf.Abs(NewCargoAmount.value) + " of " + ((TradeResource.Types)NewCargoType.value).ToString();
		}

		Stops.Add(ns);
		descrtiption += "\n";
		RouteDescription.text += descrtiption;
	}

	public void Spawn(){
		GameObject tmp = Instantiate(UnitPrefab, transform.position, new Quaternion(0,0,0,0));
		if(Stops[0].node == Stops[Stops.Count - 1].node){
			tmp.GetComponent<UnitMovement>().ReverseOnCompletion = false;
			Stops.RemoveAt(Stops.Count - 1);
		}else{
			tmp.GetComponent<UnitMovement>().ReverseOnCompletion = true;
		}
		tmp.GetComponent<UnitMovement>().Stops.AddRange(Stops);

		Stops.Clear();
		RouteDescription.text = "";
	}
}
