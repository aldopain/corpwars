using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MageRace : Player_Trade {
	System_Time time; 
	void Start() {
		resources = GetComponent<Actor_Resources>();
		money = GetComponent<Player_Money>();
		events = gm.GetComponent<System_EventPool>();
		time = gm.GetComponent<System_Time>();
		time.OnMonth.AddListener(RefreshResources);
	}

	void RefreshResources(){
		resources.Randomize();
	}
}