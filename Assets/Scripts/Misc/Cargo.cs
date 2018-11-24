using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cargo{
	public enum CargoType{
		Type1, Type2, Type3
	}

	public CargoType CurrentType;
	public float MaxAmount;
	private float Amount;

	public Cargo(CargoType type, float max = Mathf.Infinity, float amount = 0){
		CurrentType = type;
		MaxAmount = max;

		if(Amount > MaxAmount){
			Amount = MaxAmount;
		}
		Amount = amount;
	}

	public void Add(float a){
		if(Amount + a > MaxAmount){
			Amount = MaxAmount;
		}else if(Amount + a < 0){
			Amount = 0;
		}else{
			Amount += a;
		}
	}

	/* Returns:
			if more than max after addition: positive number representing the storage needed to store surplus
			if less than zero after addition: negative number representing amount of cargo needed to detract a from pile
			if can add to the pile: 0
	 */
	public float GetDifference(float a){
		if(Amount + a > MaxAmount){
			return Amount + a - MaxAmount;
		}else if(Amount + a < 0){
			return Amount + a;
		}else{
			return 0;
		}
	}

	public float GetAmount(){
		return Amount;
	}
}
