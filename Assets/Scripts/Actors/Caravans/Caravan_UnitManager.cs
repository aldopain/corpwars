using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_UnitManager : MonoBehaviour {
  	public List<Ship_BaseInfo> ShipList;
	private List<Ship_BaseInfo> _shipList;
	public float SpeedModifier = 1f;
	void Start () {
		SetCaravanSpeed();
        CopyShipList();
	}
	
    void CopyShipList()
    {
        for(int i = 0; i < ShipList.Count; i++)
        {
            ShipList[i] = Instantiate(ShipList[i]);
        }
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

		return min * (1 - (float)(GetComponent<Actor_Resources>().GetAllResources()/combinedInvSize)) * SpeedModifier;
	}

	public bool IsAlive(){
		foreach (var ship in ShipList)
			if (ship.Health > 0) return true;
		return false;
	}

	public void SetCaravanSpeed(float s){
		GetComponent<Actor_CaravanMovement>().agent.speed = s;
	}

	public void SetCaravanSpeed(){
		GetComponent<Actor_CaravanMovement>().agent.speed = GetCaravanSpeed();
	}
}
