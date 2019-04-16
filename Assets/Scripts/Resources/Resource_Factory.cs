using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Type : int { Farm, Mine, Factory };

public class Resource_Factory : MonoBehaviour {
	public Resource_ConvertionRecipe recipe;
	public float inputEfficiency;
	public float outputEfficiency;
	public Type type;
	public double exhaustion = 1d;
	double produced;
	public bool invertExhaustion = false;

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

		/*
		  need to refactor - twice counting, can count once
		 	possible resolutions:
			 - add override to DeclareProduction (param: Resource_Input)
			 - add override to AddResource (params: amount, inputId)
		*/
		foreach(var output in recipe.output) {
			var outputCount = output.amount * resourceOutputEfficiency[output.inputID] * outputEfficiency * exhaustion;
			economy.resources.AddResource(output * resourceOutputEfficiency[output.inputID] * outputEfficiency * exhaustion);
			economy.DeclareProduction(output.inputID, outputCount);
			produced += outputCount;
			CalculateExhaustion();
		}
	}

	public void CalculateExhaustion(){
		if (type != Type.Factory) {
			var sign = invertExhaustion ? 1 : -1;
			exhaustion = 1 + sign * Math.Atan(produced) * 0.1;
		}
	}
}