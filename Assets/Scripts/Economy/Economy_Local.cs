using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Local : MonoBehaviour {
	Town_Resources resources;
	int[] produced;				//produced since last report
	int[] bought;				//bought since last report
	int[] sold;					//sold since last report

	void Awake(){
		resources = GetComponent<Town_Resources>();

		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		produced = new int[gl.ResourcesList.Length];
		bought = new int[gl.ResourcesList.Length];
		sold = new int[gl.ResourcesList.Length];
	}

	//Positive amount means that town is buying from a trader; negative means town is selling to a trader
	//Returns true if trade was successful
	public bool Trade(int index, int amount){
		if(resources.CheckResource(index, amount)){				//if town has enough resources to trade
			resources.AddResource(index, amount);				//add resources
			if(amount > 0) bought[index] += amount;				//declare trade
			if(amount < 0) sold[index] += amount;
			return true; 
		}
		return false;
	}

	public void Produce(int index){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();			//get list of all resources
		
		foreach(Resource_ConvertionRecipe.Input input in gl.ResourcesList[index].Recipe.input){					//check if town has all components
			if(!resources.CheckResource(input.inputID, input.amount)) return;
		}

		for(int i = 0; i < gl.ResourcesList[index].Recipe.input.Length; i++){									//detract base resources that are needed to create new one 
			resources.AddResource(gl.ResourcesList[index].Recipe.input[i]);
		}

		resources.AddResource(index, 1);																		//add new resource to the pile

		produced[index]++;																						//declare production
	}

}
