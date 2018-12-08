using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour {
	public List<Faction> factions;

	public void AddFaction(Faction f){
		factions.Add(f);
	}

	public void RemoveFaction(Faction f, Faction removedBy){
		removedBy.Money += f.Money;
		removedBy.TownsOwned.AddRange(f.TownsOwned); 
	}

	public Faction FindFaction(string Name){
		foreach(Faction f in factions){
			if(f.Name == Name) return f;
		}

		return null;
	}
}
