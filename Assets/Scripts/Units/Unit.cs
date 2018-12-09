using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class Unit : MonoBehaviour {
	public enum UnitType{
		Trade, Military, Privateer
	}

	public List<Ship> Ships;

	public string OwnerName;
	public float Speed;
	public int Attack;
	public int Defence;
	public int CargoCapacity;
	public int MaintenanceCost;

	private UnitMovement _movement;
	private UnitManager _manager;

	void Awake(){
		_movement = GetComponent<UnitMovement>();
	}

	void Start(){
		_movement.SetSpeed(Speed);
	}

	public void Goto(Vector3 point, bool clearNodes = false){
		if(clearNodes){
			_movement.Stops.Clear();
		}

		_movement.Goto(point);
	}

	public void Maintenance(){
		GameObject.FindGameObjectWithTag("GameController").GetComponent<FactionController>().FindFaction(OwnerName).Money -= MaintenanceCost;
	}

	void OnDestroy(){
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Time>().OnDay.RemoveListener(Maintenance);
	}
}
