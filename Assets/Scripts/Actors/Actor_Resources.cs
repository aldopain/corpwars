using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_Resources : MonoBehaviour {
	[SerializeField]
	int[] Amount;

	public void AddResource(int index, int amount){
		Amount[index] += amount;
	}

	public void AddResource(Resource_Input input){
		Amount[input.inputID] += input.amount;
	}

	public void RemoveResource(Resource_Input input){
		Amount[input.inputID] -= input.amount;
	}

	public bool CheckResource(int index, int amount){
		print("Checking resources of " + name + "; index: " + index + "; amount: " + amount);

		if(Amount[index] >= amount) return true;
		return false;
	}
}
