using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caravan_UnitManager : MonoBehaviour {
  	public List<Ship_BaseInfo> ShipList;
	public float SpeedModifier = 1f;
	public double PowerRating = 0d;
	NavMeshAgent agent;
	public SystemEvent_CreateCaravan ev;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		SetCaravanSpeed();
        CopyShipList();
		ev.Invoke(gameObject);
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

	/*
		other.PowerRating - PowerRating is in [-10...10]
		add 10 to go in [0...20]
		fightDifficulty should be in [0...10], so / 2
	 */
	public double CountFightDifficulty(Caravan_UnitManager other){
		return (other.PowerRating - PowerRating + 10) / 2;
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
		agent.speed = s;
	}

	public void SetCaravanSpeed(){
		if (agent != null)
			agent.speed = GetCaravanSpeed();
	}

	public double UpdatePowerRating(){
		var summ = 0d;
		foreach (var ship in ShipList)
			summ += ship.Health + ship.Attack + ship.Defence;
		// 600, because it's maximum summ for one ship
		summ /= 600;
		PowerRating = summ;
		return PowerRating;
	}
}
