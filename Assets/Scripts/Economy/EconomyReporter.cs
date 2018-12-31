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
		GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().OnDay.AddListener(Report);
		Report();	
	}
	
	void Report(){
		for(int i = 0; i < System.Enum.GetNames(typeof(TradeResource.Types)).Length; i++){
			to.globalInfo[i].produced += from.resources[i].produced;
			to.globalInfo[i].sold += from.resources[i].sold;
			to.globalInfo[i].bought += from.resources[i].bought;
			to.globalInfo[i].stock += from.resources[i].stock;
		}	
	}
}
