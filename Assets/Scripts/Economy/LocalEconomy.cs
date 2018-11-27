using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEconomy : MonoBehaviour {
	public List<TradeResource> resources;

	// Use this for initialization
	void Start () {
		SetResorcesList();
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
}
