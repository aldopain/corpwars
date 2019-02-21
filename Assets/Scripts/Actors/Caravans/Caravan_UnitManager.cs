using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_UnitManager : MonoBehaviour {
    public List<Ship_BaseInfo> ShipList;
	
	void Start () {
		SetCaravanSpeed();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetCaravanSpeed(){
		float min = Mathf.Infinity;
		double combinedInvSize = 0;
		
		foreach(Ship_BaseInfo info in ShipList){
			if(info.Speed < min) min = info.Speed;
			combinedInvSize += info.InventorySize;
		}	


		//if(GetComponent<Actor_Resources>().GetAllResources() == 0) return min;

		return min * (1 - (float)(GetComponent<Actor_Resources>().GetAllResources()/combinedInvSize));
	}

	public void SetCaravanSpeed(float s){
		GetComponent<Actor_CaravanMovement>().agent.speed = s;
	}

	public void SetCaravanSpeed(){
		GetComponent<Actor_CaravanMovement>().agent.speed = GetCaravanSpeed();
	}
}
