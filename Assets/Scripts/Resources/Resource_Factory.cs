using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_Factory : MonoBehaviour {
	public Resource_ConvertionRecipe recipe;
	public float inputEfficiency;
	public float outputEfficiency;

	public void Produce(Local_Economy economy) {
		for(int i = 0; i < recipe.input.Length; i++) {
			economy.resources.RemoveResource(recipe.input[i] * inputEfficiency);
		}

		for(int i = 0; i < recipe.output.Length; i++) {
			economy.resources.AddResource(recipe.output[i] * outputEfficiency);
		}
	}
}
