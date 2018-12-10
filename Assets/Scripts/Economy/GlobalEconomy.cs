using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEconomy : MonoBehaviour {
	public List<TradeResource> globalInfo;
	public LocalEconomy[] TradePosts;

	float[] producedPrevDay = new float[3];
	float[] soldPrevDay = new float[3];
	// Use this for initialization
	void Start () {
		SetResorcesList();
		GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().OnDay.AddListener(UpdatePricing);
	}
	
	void SetResorcesList(){
		foreach(TradeResource.Types t in System.Enum.GetValues(typeof(TradeResource.Types))){
			globalInfo.Add(new TradeResource(t));
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void UpdatePricing(){
		for(int i = 0; i < 3; i++){
			globalInfo[i].price -= globalInfo[i].price * ((globalInfo[i].produced - producedPrevDay[i]) - (globalInfo[i].sold - soldPrevDay[i]))/100;
			producedPrevDay[i] = globalInfo[i].produced;
			soldPrevDay[i] = globalInfo[i].sold;
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
