using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class Unit : MonoBehaviour {
	public float Speed;
	public int Attack;
	public int Defence;
	public int CargoCapacity;

	private UnitMovement _movement;
	private UnitManager _manager;

	void Awake(){
		_movement = GetComponent<UnitMovement>();
	}

	void Start(){
		_movement.SetSpeed(Speed);
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(_manager == null){
				_manager = GameObject.Find("GameController").GetComponent<UnitManager>();
			}

			_manager.SetSelectedUnit(this);
		}
	}

	public void Goto(Vector3 point, bool clearNodes = false){
		if(clearNodes){
			_movement.Stops.Clear();
		}

		_movement.Goto(point);
	}
}
