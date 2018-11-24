using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyReporter : MonoBehaviour {
	LocalEconomy from;
	GlobalEconomy to;

	void Awake(){
		from = GetComponent<LocalEconomy>();
		to = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalEconomy>();
		
	}

	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Time>().OnDay.AddListener(Report);	
	}
	
	void Report(){
		from.Debug_Produce();
		from.Debug_Sell();

		//DEBUG
		for(int i = 0; i < 3; i++){
			to.globalInfo[i].produced += from.commodities[i].produced;
			to.globalInfo[i].sold += from.commodities[i].sold;
		}	
	}
}
