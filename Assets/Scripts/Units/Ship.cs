using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship{
	public enum ShipType{
		Trade, Military, Privateer
	}

	public ShipType CurrentType;
	public int Cost;
	public int Maintenance;

	public Ship(ShipType t){
		CurrentType = t;
	}
}
