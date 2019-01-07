using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Money : MonoBehaviour {
	public int Amount;

	public void Add(int a){
		Amount += a;
	}

	public void Detract(int a){
		Amount -= a;
	}

	public void Set(int a){
		Amount = a;
	}

	public bool Check(int a){
		if(Amount >= a) return true;
		return false;
	}

	public void Transfer(int a, Player_Money other){
		if(!other.Check(a)) return;
		Detract(a);
		other.Add(a);
	}
}
