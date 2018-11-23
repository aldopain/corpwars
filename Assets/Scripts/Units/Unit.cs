﻿using System.Collections;
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

	void Start(){
		_movement = GetComponent<UnitMovement>();
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(_manager == null){
				_manager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
			}

			_manager.SetSelectedUnit(this);
		}
	}

	public void Goto(Vector3 point, bool clearNodes = false){
		if(clearNodes){
			_movement.Nodes.Clear();
		}

		_movement.Goto(point);
	}
}
