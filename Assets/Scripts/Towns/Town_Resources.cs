using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Resources : MonoBehaviour {
	[SerializeField]
	int[] Amount;

	public void AddResource(int index, int amount){
		Amount[index] += amount;
	}

	public void AddResource(Resource_ConvertionRecipe.Input input){
		Amount[input.inputID] -= input.amount;
	}

	public bool CheckResource(int index, int amount){
		if(Amount[index] >= amount) return true;
		return false;
	}
}
