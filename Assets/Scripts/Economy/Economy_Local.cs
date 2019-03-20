using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Local : MonoBehaviour {
	
	public Actor_Resources resources;
	public Town_Population population;
	private Economy_Global globalEconomy;
	///<summary>Produced since last report</summary>
	public double[] produced;				
	///<summary>Produced since last report</summary>
	public double[] bought;				
	///<summary>Produced since last report</summary>
	public double[] sold;					

	public double[] priceModifiers;
	void Awake(){
		resources = GetComponent<Actor_Resources>();
		population = GetComponent<Town_Population>();

		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		
		produced = new double[gl.ResourcesList.Length];
		bought = new double[gl.ResourcesList.Length];
		sold = new double[gl.ResourcesList.Length];

		//DEBUG
		priceModifiers = new double[gl.ResourcesList.Length];
		for(int i = 0; i < priceModifiers.Length;i++){
			priceModifiers[i] = Random.Range(0.1f, 2f);
		}

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
	public bool Trade(int index, double amount, Actor_Resources other) {
		int price = CalculatePrice(index, amount);
		var otherMoney = other.Owner.GetComponent<Player_Money>();
		var ownerMoney = resources.Owner.GetComponent<Player_Money>();
		if (amount < 0) {			//selling to a trader
			amount *= -1;
			if (resources.CheckResource(index, amount) && otherMoney.Check(price)) {
				resources.Transfer(index, amount, other);
				otherMoney.Transfer(price, ownerMoney);
				DeclareTrade(index, -amount);
				return true;
			}
		} else {					//buying from a trader
			if (other.CheckResource(index, amount) && ownerMoney.Check(price)) {
				other.Transfer(index, amount, resources);
				ownerMoney.Transfer(price, otherMoney);
				DeclareTrade(index, amount);
				return true;
			}
		}
		Debug.LogFormat("Transaction failed: Sides: {0}, {1}; Resource: {2}x{3}", name, other.name,amount, GameObject.Find("GameController").GetComponent<Resource_GlobalList>().ResourcesList[index].Name);
		return false;
	}

    /// <summary>
    /// Calculates price of resources for a specified amount
    /// </summary>
    /// <param name="index">Resource</param>
    /// <param name="amount">Amount in transaction</param>
    /// <returns>Price</returns>
	int CalculatePrice (int index, double amount) {
		double K = priceModifiers[index];
		int B = globalEconomy.pricing[index];
		double P = population.GetDemands(index);
		double N = resources.GetResource(index);
		double X = amount;
		int S = (int)(K * B * System.Math.Exp(-((X + 2 * N)/(2 * P))));

		//Debug.LogFormat("K: {0}; B: {1}; P: {2}; N: {3}; X: {4}; S: {5}", K, B, P, N ,X, S);
		return S;
	}

    /// <summary>
    /// Trading between two Actor_Resources (usually between a town and a caravan) 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="other"></param>
    /// <returns>true if trade was successful, otherwise false</returns>
	public bool Trade(Resource_Input input, Actor_Resources other){
		return Trade(input.inputID, input.amount, other);
	}

	public void DeclareProduction(int index, double amount){
		produced[index] +=  System.Math.Abs(amount);
	}

	//if amount > 0, declare resources as bought; else declare resources as sold
	public void DeclareTrade(int index, double amount){
		if(amount > 0) bought[index] += System.Math.Abs(amount);
		if(amount < 0) sold[index] += System.Math.Abs(amount);
	}

    /// <summary>
    /// Clears all declared resources
    /// </summary>
	public void ClearDeclarations(){
		for(int i = 0; i < produced.Length; i++){
			produced[i] = 0;
			bought[i] = 0;
			sold[i] = 0;
		}
	}
}
