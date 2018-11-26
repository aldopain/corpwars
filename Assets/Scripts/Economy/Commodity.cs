using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Commodity{
	public Cargo cargo;
	public float price;
	public float produced;
	public float bought;
	public float sold;
	public string name;
	public string description;
	public Sprite icon;
}
