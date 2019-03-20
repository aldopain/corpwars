using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_Resources : MonoBehaviour {
	[SerializeField]
	double[] Amount;
	public Player_Manager Owner;
	///<summary>
	///Add or remove resource
	///</summary>
	///<param name="index">ID of resource</param>
	///<param name="amount">Amount of resources to add/remove</param>	
	public void AddResource(int index, double amount){
		Amount[index] += amount;
	}

	///<summary>
	///Add resource to this inventory
	///</summary>
	///<param name="input">Resource to add</param>
	public void AddResource(Resource_Input input){
		Amount[input.inputID] += input.amount;
	}

	///<summary>
	///Remove resource from this inventroy
	///</summary>
	///<param name="index">Resource to remove</param>
	public void RemoveResource(Resource_Input input){
		Amount[input.inputID] -= input.amount;
	}

	///<summary>
	///Checks for resource availability
	///</summary>
	///<param name="index">ID of resource</param>
	///<param name="amount">Needed amount</param>
	///<returns>true if enough resources are available</returns>
	public bool CheckResource(int index, double amount){
		if(Amount[index] >= amount) return true;
		return false;
	}

	public double GetResource(int index){
		return Amount[index];
	}

	public double GetAllResources(){
		double res = 0;
		foreach(double d in Amount) res += d;
		return res;
	}

	public bool SafeTransfer(int index, double amount, Actor_Resources other){
		if(CheckResource(index, amount)){
			Transfer(index, amount, other);
			return true;
		}
		return false;
	}

	public void Transfer(int index, double amount, Actor_Resources other){
		AddResource(index, -amount);
		other.AddResource(index, amount);
	}

	public int Debug_GetResourceLength()
	{
			return Amount.Length;
	}
}
