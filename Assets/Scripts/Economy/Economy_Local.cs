using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Local : MonoBehaviour {
	
	public Actor_Resources resources;
	private Economy_Global globalEconomy;
	///<summary>Produced since last report</summary>
	public int[] produced;				
	///<summary>Produced since last report</summary>
	public int[] bought;				
	///<summary>Produced since last report</summary>
	public int[] sold;					

	public int[] prices;
	public int[] baseValues;
	void Awake(){
		resources = GetComponent<Actor_Resources>();

		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		
		produced = new int[gl.ResourcesList.Length];
		bought = new int[gl.ResourcesList.Length];
		sold = new int[gl.ResourcesList.Length];

		prices = new int[gl.ResourcesList.Length];
		baseValues = new int[gl.ResourcesList.Length];

		gl.GetComponent<System_Time>().OnDay.AddListener(UpdateDay);
		globalEconomy = gl.GetComponent<Economy_Global>();
	}

	// Update is called once per day
	void UpdateDay(){

	}

	///
	///<summary>
	///Trading between two Actor_Resources (usually between a town and a caravan) 
	///Positive amount of traded resource means that town is buying from a trader; negative means town is selling to a trader
	///</summary>
	///
	///<returns>true if trade was successful, otherwise false</returns>
	///<param name = "index">ID of traded resource</param>
	///<param name = "amount">amount of traded resource</param>
	///<param name = "other">buyer/seller that is involved with a trade</param>
	public bool Trade(int index, int amount, Actor_Resources other){
		if(amount < 0){			//selling to a trader
			amount *= -1;
			if(resources.CheckResource(index, amount)){
				resources.AddResource(index, -amount);
				other.AddResource(index, amount);
				DeclareTrade(index, -amount);
				return true;
			}
		}else{					//buying from a trader
			if(other.CheckResource(index, amount)){
				resources.AddResource(index, amount);
				other.AddResource(index, -amount);
				DeclareTrade(index, amount);
				return true;
			}
		}
		return false;
	}

	public bool Trade(Resource_Input input, Actor_Resources other){
		return Trade(input.inputID, input.amount, other);
	}

	public void DeclareProduction(int index, int amount){
		produced[index] += Mathf.Abs(amount);
	}

	//if amount > 0, declare resources as bought; else declare resources as sold
	public void DeclareTrade(int index, int amount){
		if(amount > 0) bought[index] += Mathf.Abs(amount);
		if(amount < 0) sold[index] += Mathf.Abs(amount);
	}

	public void ClearDeclarations(){
		for(int i = 0; i < produced.Length; i++){
			produced[i] = 0;
			bought[i] = 0;
			sold[i] = 0;
		}

	}
}
