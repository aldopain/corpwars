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
			economy.Produce(i);
		}
	}
}
