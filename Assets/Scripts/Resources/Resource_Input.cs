using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource_Input{
	public int inputID;
	public double amount;

	public static Resource_Input operator*(Resource_Input a, float f){
		Resource_Input ri = new Resource_Input();
		ri.inputID = a.inputID;
		ri.amount = a.amount * f;
		return ri;
	}

	public static Resource_Input operator*(Resource_Input a, double d){
		Resource_Input ri = new Resource_Input();
		ri.inputID = a.inputID;
		ri.amount = a.amount * d;
		return ri;
	}
}
