using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Production : MonoBehaviour {
	public int[] resourceID;
	public float[] inputModifier;
	public float[] outputModifier;

	Economy_Local economy;

	void Awake(){
		economy = GetComponent<Economy_Local>();
	}

	void Start(){
		GameObject.Find("GameController").GetComponent<System_Time>().OnDay.AddListener(ProduceResources);
	}

	public void ProduceResources(){
		foreach(int i in resourceID){
			Produce(i);
		}
	}

	void Produce(int index){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();					//get list of all resources
		Resource_ConvertionRecipe recipe = gl.ResourcesList[index].Recipe;
	
		foreach(Resource_Input input in recipe.input){											//check if town has all components
			if(!economy.resources.CheckResource(input.inputID, input.amount * inputModifier[input.inputID])) return;
		}

		for(int i = 0; i < recipe.input.Length; i++){											//detract base resources that are needed to create new one 
			economy.resources.RemoveResource(recipe.input[i] * inputModifier[recipe.input[i].inputID]);
		}

        for(int i = 0; i < recipe.output.Length; i++)                                           //add new resource to the pile
        {
            economy.resources.AddResource(recipe.output[i] * outputModifier[recipe.output[i].inputID]);
        }
		                    																		

		economy.DeclareProduction(index, 1);																			//declare production
	}
}
