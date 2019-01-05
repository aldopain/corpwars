using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Local : MonoBehaviour {
	
	public Actor_Resources resources;
	int[] produced;				//produced since last report
	int[] bought;				//bought since last report
	int[] sold;					//sold since last report

	void Awake(){
		resources = GetComponent<Actor_Resources>();

		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		produced = new int[gl.ResourcesList.Length];
		bought = new int[gl.ResourcesList.Length];
		sold = new int[gl.ResourcesList.Length];
	}

	//Positive amount means that town is buying from a trader; negative means town is selling to a trader
	//Returns true if trade was successful
	public bool Trade(int index, int amount, Actor_Resources other){
		if(amount < 0){			//selling to a trader
			amount *= -1;
			if(resources.CheckResource(index, amount)){
				resources.AddResource(index, -amount);
				other.AddResource(index, amount);
				DeclareTrade(index, -amount);
				print("Trade between " + name + " and " + other.name + " is successful");
				return true;
			}
		}else{					//buying from a trader
			if(other.CheckResource(index, amount)){
				resources.AddResource(index, amount);
				other.AddResource(index, -amount);
				DeclareTrade(index, amount);
				print("Trade between " + name + " and " + other.name + " is successful");
				return true;
			}
		}
		return false;
	}

	public bool Trade(Resource_Input input, Actor_Resources other){
		return Trade(input.inputID, input.amount, other);
	}

	public void DeclareProduction(int index, int amount){
		produced[index] += amount;
	}

	//if amount > 0, declare resources as bought; else declare resources as sold
	public void DeclareTrade(int index, int amount){
		if(amount > 0) bought[index] += amount;
		if(amount < 0) sold[index] += amount;
	}
}
