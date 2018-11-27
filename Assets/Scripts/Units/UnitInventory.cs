using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour {
	public List<Cargo> Inventory;

	public int MaxNumber;
	public float MaxAmount;

	public bool AddCargo(TradeResource.Types type, float Amount){
		if(Inventory.Count < MaxNumber){
			if(Amount > MaxAmount){
				return false;
			}
			Inventory.Add(new Cargo(type, MaxAmount, Amount));
			return true;
		}else{
			return false;
		}
	}

	public bool AddCargo(Cargo c){
		if(Inventory.Count < MaxNumber){
			if(c.GetAmount() < MaxAmount){
				Inventory.Add(c);
				return true;
			}else{
				return false;
			}
		}else{
			return false;
		}
	}

	public bool RemoveCargo(Cargo c){
		if(Inventory.Contains(c)){
			Inventory.Remove(c);
			return true;
		}else{
			return false;
		}
	}
}
