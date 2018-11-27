using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradeResource{
	public enum Types{
		Type1,
		Type2,
		Type3
	}

	//Logic 
	public Types CurrentType;
	public float price;
	public float stock;

	public float produced;
	public float bought;
	public float sold;

	//Visuals
	public string Name;
	public string Description;
	public Sprite Icon;

	public TradeResource(Types t){
		CurrentType = t;
	}
	public void AdjustPrice(){
		price -= price * (produced - sold)/100;
	}
}
