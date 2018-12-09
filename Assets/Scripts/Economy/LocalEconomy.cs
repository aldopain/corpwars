using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEconomy : MonoBehaviour {
	public List<TradeResource> resources;
	public List<ConversionRecipe> recipes;
	public string OwnerName;

	// Use this for initialization
	void Start () {
		SetResorcesList();
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Time>().OnDay.AddListener(ConvertResources);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetResorcesList(){
		foreach(TradeResource.Types t in System.Enum.GetValues(typeof(TradeResource.Types))){
			resources.Add(new TradeResource(t));
		}
	}

	TradeResource FindResource(TradeResource.Types t){
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

	public void ChangeOwner(string newOwner){
		OwnerName = newOwner;
		GetComponent<MeshRenderer>().sharedMaterial.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<FactionController>().FindFaction(OwnerName).FactionColour;
	}
}
