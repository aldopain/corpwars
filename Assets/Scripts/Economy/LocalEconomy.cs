using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEconomy : MonoBehaviour {
	public List<Commodity> commodities;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Debug_Produce(){
		foreach(Commodity c in commodities){
			c.produced = Random.Range(0, 10);
			c.cargo.Add(c.produced);
		}
	}

	public void Debug_Sell(){
		foreach(Commodity c in commodities){
			c.sold = Random.Range(0, 10);
			c.sold += c.cargo.GetDifference(c.sold);
			c.cargo.Add(-c.sold);
		}
	}

	Commodity FindCommodity(Cargo.CargoType t){
		foreach(Commodity com in commodities){
			if(com.cargo.CurrentType == t){
				return com;
			}
		}
		return null;
	}

	public void Add(Cargo c){
		FindCommodity(c.CurrentType).cargo.Add(c.GetAmount());
	}

	public void Remove(Cargo c){
		FindCommodity(c.CurrentType).cargo.Add(-c.GetAmount());
	}
}
