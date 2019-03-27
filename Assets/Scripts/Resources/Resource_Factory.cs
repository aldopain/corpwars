using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Factory : MonoBehaviour {
	public Resource_ConvertionRecipe recipe;
	public float inputEfficiency;
	public float outputEfficiency;

	public void TryProduce(Economy_Local economy, float[] resourceInputEfficiency, float[] resourceOutputEfficiency) {
		foreach(Resource_Input input in recipe.input){
			if(!economy.resources.CheckResource(input.inputID, input.amount * resourceInputEfficiency[input.inputID] * inputEfficiency)) return;
		}
		Produce(economy, resourceInputEfficiency, resourceOutputEfficiency);
	}

	public void Produce(Economy_Local economy, float[] resourceInputEfficiency, float[] resourceOutputEfficiency) {
		foreach(var input in recipe.input) {
			economy.resources.RemoveResource(input * resourceInputEfficiency[input.inputID] * inputEfficiency);
		}

		foreach(var output in recipe.output) {
			economy.resources.AddResource(output * resourceOutputEfficiency[output.inputID] * outputEfficiency);
			economy.DeclareProduction(output.inputID, output.amount * resourceOutputEfficiency[output.inputID] * outputEfficiency);
		}
	}
}