using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Events_Logic : MonoBehaviour {
	Player_List pl;

	void Start () {
		pl = GetComponent<Player_List>();
	}

	public void PrintString(string[] s){
		print(s[0]);
	}

	public void Sqr(string[] a){
		int i = System.Convert.ToInt32(a[0]);
		print(i * i);
	}

	public void Add(string[] args){
		int a = Convert.ToInt32(args[0]);
		int b = Convert.ToInt32(args[1]); 
		print(a + b);
	}

	//Changes the global price of a resource by a certain amount
	public void GlobalPrice(string[] args){
		int resourceIndex = Convert.ToInt32(args[0]);
		int difference = Convert.ToInt32(args[1]);

		GetComponent<Economy_Global>().pricing[resourceIndex] += difference;
	}

	//Changes the demands of a certain class in a certain town by a certain amount
	public void PopDemands(string[] args){
		string town = args[0];
		string popClass = args[1];
		int resourceIndex = Convert.ToInt32(args[2]);
		int amount = Convert.ToInt32(args[3]);

		switch(popClass){
			case "Poor":
				GameObject.Find(town).GetComponent<Town_Population>().Poor.Demands[resourceIndex] += amount;
				break;
			case "Middle":
				GameObject.Find(town).GetComponent<Town_Population>().Middle.Demands[resourceIndex] += amount;
				break;
			case "Rich":
				GameObject.Find(town).GetComponent<Town_Population>().Rich.Demands[resourceIndex] += amount;
				break;
			default:
				Debug.LogError("ChangePopDemands can't find PopClass " + popClass);
				break;
		}		
	}

	public void PopAmount(string[] args){
		string town = args[0];
		string popClass = args[1];
		int amount = Convert.ToInt32(args[2]);

		switch(popClass){
			case "Poor":
				GameObject.Find(town).GetComponent<Town_Population>().Poor.Amount += amount;
				break;
			case "Middle":
				GameObject.Find(town).GetComponent<Town_Population>().Middle.Amount += amount;
				break;
			case "Rich":
				GameObject.Find(town).GetComponent<Town_Population>().Rich.Amount += amount;
				break;
			default:
				Debug.LogError("ChangePopDemands can't find PopClass " + popClass);
				break;
		}		
	}

	public void PopProductivity(string[] args){
		string town = args[0];
		string popClass = args[1];
		int amount = Convert.ToInt32(args[2]);

		switch(popClass){
			case "Poor":
				GameObject.Find(town).GetComponent<Town_Population>().Poor.Productivity += amount;
				break;
			case "Middle":
				GameObject.Find(town).GetComponent<Town_Population>().Middle.Productivity += amount;
				break;
			case "Rich":
				GameObject.Find(town).GetComponent<Town_Population>().Rich.Productivity += amount;
				break;
			default:
				Debug.LogError("ChangePopDemands can't find PopClass " + popClass);
				break;
		}		
	}

	public void PopHappiness(string[] args){
		string town = args[0];
		string popClass = args[1];
		int amount = Convert.ToInt32(args[2]);

		switch(popClass){
			case "Poor":
				GameObject.Find(town).GetComponent<Town_Population>().Poor.Happiness += amount;
				break;
			case "Middle":
				GameObject.Find(town).GetComponent<Town_Population>().Middle.Happiness += amount;
				break;
			case "Rich":
				GameObject.Find(town).GetComponent<Town_Population>().Rich.Happiness += amount;
				break;
			default:
				Debug.LogError("ChangePopDemands can't find PopClass " + popClass);
				break;
		}		
	}

	public void SetOwner(string[] args){
		string entity = args[0];
		string newOwner = args[1];

		GameObject.Find(entity).GetComponent<Actor_Resources>().Owner = pl.GetByName(newOwner);
	}

	public void ProductionInput(string[] args){
		string town = args[0];
		int resourceIndex = Convert.ToInt32(args[1]);
		double amount = Convert.ToDouble(args[2]);

		GameObject.Find(town).GetComponent<Town_Production>().inputModifier[resourceIndex] += (float)amount;
	}

	public void ProductionOutput(string[] args){
		string town = args[0];
		int resourceIndex = Convert.ToInt32(args[1]);
		double amount = Convert.ToDouble(args[2]);

		GameObject.Find(town).GetComponent<Town_Production>().outputModifier[resourceIndex] += (float)amount;
	}

	public void PriceModifier(string[] args){
		string town = args[0];
		int resourceIndex = Convert.ToInt32(args[1]);
		double amount = Convert.ToDouble(args[2]);

		GameObject.Find(town).GetComponent<Economy_Local>().priceModifiers[resourceIndex] += amount;
	}

	public void CaravanSpeed(string[] args){
		string player = args[0];
		double modifier = Convert.ToDouble(args[1]);

		GameObject[] caravans = GameObject.FindGameObjectsWithTag("Caravan");
		foreach(GameObject go in caravans){
			if(go.GetComponent<Actor_Resources>().Owner.name.Equals(player)){
				go.GetComponent<Caravan_UnitManager>().SpeedModifier += (float)modifier;
				go.GetComponent<Caravan_UnitManager>().SetCaravanSpeed();
			}
		}
	}

	public void AddMoney(string[] args){
		string player = args[0];
		int amount = Convert.ToInt32(args[1]);

		GameObject.Find(player).GetComponent<Player_Money>().Add(amount);
	}

	public void AddNeighbour(string[] args){
		string town1 = args[0];
		string town2 = args[1];

		GameObject.Find(town1).GetComponent<NavigationNode>().Neighbours.Add(GameObject.Find(town2).GetComponent<NavigationNode>());
	}
}
