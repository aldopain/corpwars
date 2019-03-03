using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Production : MonoBehaviour {
	public int[] resourceID;
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
		
		foreach(Resource_Input input in gl.ResourcesList[index].Recipe.input){											//check if town has all components
			if(!economy.resources.CheckResource(input.inputID, input.amount)) return;
		}

		for(int i = 0; i < gl.ResourcesList[index].Recipe.input.Length; i++){											//detract base resources that are needed to create new one 
			economy.resources.RemoveResource(gl.ResourcesList[index].Recipe.input[i]);
		}

        for(int i = 0; i < gl.ResourcesList[index].Recipe.output.Length; i++)                                           //add new resource to the pile
        {
            economy.resources.AddResource(gl.ResourcesList[index].Recipe.output[i]);
        }
		                    																		

		economy.DeclareProduction(index, 1);																			//declare production
	}
}
