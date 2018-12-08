using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaravanSpawner : MonoBehaviour {
	public List<UnitMovement.Stop> Stops;
	public List<Ship> Ships;

	//Unit
	public Dropdown UnitType;
	public Dropdown ShipType;
	public Text ShipList;

	//Route
	public Text RouteDescription;
	public Dropdown Destination;
	public Dropdown NewCargoType;
	public Slider NewCargoAmount;

	//Economy
	public int TradeShipCost;
	public int MilitaryShipCost;
	public int PrivateerShipCost;
	public int CaravanCost;

	public GameObject UnitPrefab;
	void Start () {
		Stops = new List<UnitMovement.Stop>();
		Ships = new List<Ship>();

		FillDests();
		FillUnitTypes();
		FillShipTypes();
	}
	
	public void FillDests(){
		Destination.ClearOptions();
		List<string> destNames = new List<string>();
		foreach(LocalEconomy le in GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalEconomy>().TradePosts){
			destNames.Add(le.name);
		}

		Destination.AddOptions(destNames);
	}

	public void FillUnitTypes(){
		UnitType.ClearOptions();
		List<string> tmp = new List<string>();
		tmp.AddRange(System.Enum.GetNames(typeof(Unit.UnitType)));
		UnitType.AddOptions(tmp);
	}

	public void FillShipTypes(){
		ShipType.ClearOptions();
		List<string> tmp = new List<string>();
		tmp.AddRange(System.Enum.GetNames(typeof(Ship.ShipType)));
		ShipType.AddOptions(tmp);
	}

	public void AddStop(){
		//Initializing empty object
		UnitMovement.Stop ns = new UnitMovement.Stop();
		ns.node = GameObject.Find(Destination.options[Destination.value].text).GetComponent<NavigationNode>();
		ns.Load = new List<Cargo>();
		ns.Unload = new List<Cargo>();

		//Getting values
		string descrtiption = Destination.options[Destination.value].text + "; ";
		if(NewCargoAmount.value > 0){
			ns.Load.Add(new Cargo((TradeResource.Types)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
			descrtiption += "Load " + NewCargoAmount.value + " of " + ((TradeResource.Types)NewCargoType.value).ToString();
		}else if(NewCargoAmount.value < 0){
			ns.Unload.Add(new Cargo((TradeResource.Types)NewCargoType.value, Mathf.Infinity, NewCargoAmount.value));
			descrtiption += "Unload " + Mathf.Abs(NewCargoAmount.value) + " of " + ((TradeResource.Types)NewCargoType.value).ToString();
		}

		//Setting values
		Stops.Add(ns);
		descrtiption += "\n";
		RouteDescription.text += descrtiption;
	}

	public void AddShip(){
		switch((Unit.UnitType)UnitType.value){
			case Unit.UnitType.Trade:
				if(ShipType.value != 2){
					Ships.Add(new Ship((Ship.ShipType)ShipType.value));
					ShipList.text += ((Ship.ShipType)ShipType.value).ToString() + "\n";
					CaravanCost += TradeShipCost;
				}
				break;
			case Unit.UnitType.Military:
				if(ShipType.value != 0){
					Ships.Add(new Ship((Ship.ShipType)ShipType.value));
					ShipList.text += ((Ship.ShipType)ShipType.value).ToString() + "\n";
					CaravanCost += MilitaryShipCost;
				}
				break;
			case Unit.UnitType.Privateer:
				if(ShipType.value == 2){
					Ships.Add(new Ship((Ship.ShipType)ShipType.value));
					ShipList.text += ((Ship.ShipType)ShipType.value).ToString() + "\n";
					CaravanCost += PrivateerShipCost;
				}
				break;
		}
	}

	public void Spawn(){
		GameObject tmp = Instantiate(UnitPrefab, Stops[0].node.transform.position, new Quaternion(0,0,0,0));

		//Setting up stops
		if(Stops[0].node == Stops[Stops.Count - 1].node){
			tmp.GetComponent<UnitMovement>().ReverseOnCompletion = false;
			Stops.RemoveAt(Stops.Count - 1);
		}else{
			tmp.GetComponent<UnitMovement>().ReverseOnCompletion = true;
		}
		tmp.GetComponent<UnitMovement>().Stops.AddRange(Stops);

		//Setting up ships
		tmp.GetComponent<Unit>().Ships = new List<Ship>();
		tmp.GetComponent<Unit>().Ships.AddRange(Ships);

		//Clearing the UI and lists
		Stops.Clear();
		RouteDescription.text = "";

		Ships.Clear();
		ShipList.text = "";

		GameObject.FindGameObjectWithTag("GameController").GetComponent<FactionController>().FindFaction("Player Faction").Money -= CaravanCost;
	}
}
