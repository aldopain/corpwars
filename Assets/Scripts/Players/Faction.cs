using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction{
	//Visuals
	public string Name;
	public Color FactionColour;

	//Economy
	public int Money;
	public List<LocalEconomy> TownsOwned;

	public Faction(string _name, Color _colour, LocalEconomy startingTown, int _money = 0){
		Name = _name;
		FactionColour = _colour;
		TownsOwned = new List<LocalEconomy>();
		TownsOwned.Add(startingTown);
		Money = _money;
	}	
}
