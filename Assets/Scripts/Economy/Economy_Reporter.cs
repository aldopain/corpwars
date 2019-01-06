using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Reporter : MonoBehaviour {
	Economy_Local local;
	Economy_Global global;
	void Awake(){
		local = GetComponent<Economy_Local>();
		global = GameObject.Find("GameController").GetComponent<Economy_Global>();
	}

	void Start () {
		GameObject.Find("GameController").GetComponent<System_Time>().OnDay.AddListener(Report);	
	}

	public void Report(){
		print(name + " reporting");
		for(int i = 0; i < global.producedDay.Length; i++){
			global.producedDay[i] += local.produced[i];
			global.producedMonth[i] += local.produced[i];
			global.producedLifetime[i] += local.produced[i];

			global.boughtDay[i] += local.bought[i];
			global.boughtMonth[i] += local.bought[i];
			global.boughtLifetime[i] += local.bought[i];

			global.soldDay[i] += local.sold[i];
			global.soldMonth[i] += local.sold[i];
			global.soldLifetime[i] += local.sold[i];
		}

		local.ClearDeclarations();
	}
}
