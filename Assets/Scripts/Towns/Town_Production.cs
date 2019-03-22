using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Production : MonoBehaviour {
	public List<Resource_Factory> factories;

	Economy_Local economy;
	Resource_GlobalList gl;

	void Awake() {
		economy = GetComponent<Economy_Local>();
	}

	void Start() {
		GameObject.Find("GameController").GetComponent<System_Time>().OnDay.AddListener(ProduceResources);
		gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
	}

	public void ProduceResources() {
		foreach (int i in resourceID){
			Produce(i);
		}
	}

	void Produce(int index) {
		Resource_ConvertionRecipe recipe = gl.ResourcesList[index].Recipe;

		foreach(Resource_Input input in recipe.input) {
			if(!economy.resources.CheckResource(input.inputID, input.amount * inputModifier[input.inputID])) return;
		}

		economy.DeclareProduction(index, 1);																			//declare production
	}
}
