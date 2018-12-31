using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEconomy : MonoBehaviour {
	public List<TradeResource> globalInfo;
	public LocalEconomy[] TradePosts;

	//float[] producedPrevDay = new float[System.Enum.GetValues(typeof(TradeResource.Types)).Length];
	//float[] soldPrevDay = new float[System.Enum.GetValues(typeof(TradeResource.Types)).Length];

	bool updatedPricing;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().OnDay.AddListener(UpdatePricing);
	}
	
	void SetResourcesList(){
		foreach(TradeResource.Types t in System.Enum.GetValues(typeof(TradeResource.Types))){
			globalInfo.Add(new TradeResource(t));
		}
	}



	void UpdatePricing(){
		for(int i = 0; i < System.Enum.GetValues(typeof(TradeResource.Types)).Length; i++){
			globalInfo[i].price -= globalInfo[i].price * ((globalInfo[i].produced) - (globalInfo[i].sold))/100;
			print("Price: " + globalInfo[i].price + "; Produced: " + globalInfo[i].produced + "; Sold: " + globalInfo[i].price);
			Debug.Break();

			globalInfo[i].produced = 0;
			globalInfo[i].bought = 0;
			globalInfo[i].sold = 0;
			globalInfo[i].stock = 0;
		}

		foreach(LocalEconomy p in TradePosts){
			int i = 0;
			foreach(TradeResource c in p.resources){
				c.price = globalInfo[i].price;
				i++;
			}
		}
	}
}
