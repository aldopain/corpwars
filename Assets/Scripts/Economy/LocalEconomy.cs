using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEconomy : MonoBehaviour {
	public List<TradeResource> resources;
	public List<ConversionRecipe> recipes;
	public PopulationController population;
	public string OwnerName;
	
	void Awake(){
		population.AttachedEconomy = this;
	}

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().OnDay.AddListener(ConvertResources);
		GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().OnDay.AddListener(UpdatePopulation);
	}
	
	public void UpdatePopulation(){
		population.Poor.Update();
		population.Middle.Update();
		population.Rich.Update();
	} 

	public void SetResourcesList(){
		foreach(TradeResource.Types t in System.Enum.GetValues(typeof(TradeResource.Types))){
			resources.Add(new TradeResource(t));
		}
	}

	public TradeResource FindResource(TradeResource.Types t){
		foreach(TradeResource tr in resources){
			if(tr.CurrentType == t){
				return tr;
			}
		}
		return null;
	}

	public void Add(Cargo c){
		FindResource(c.CurrentType).stock += c.GetAmount(); 
	}

	public void Remove(Cargo c){
		FindResource(c.CurrentType).stock -= c.GetAmount(); 
	}

	public void ConvertResources(){
		for(int i = 0; i < recipes.Count; i++){
			for(int j = 0; j < recipes[i].input.Length; i++){
				FindResource(recipes[i].input[j]).stock -= recipes[i].inputAmount[j];
			}

			FindResource(recipes[i].output).stock += recipes[i].outputAmount;
		}
	}

	public void ProduceResources(){

	}

	public void ChangeOwner(string newOwner){
		OwnerName = newOwner;
		GetComponent<MeshRenderer>().sharedMaterial.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<FactionController>().FindFaction(OwnerName).FactionColour;
	}
}
