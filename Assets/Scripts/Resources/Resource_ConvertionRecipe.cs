using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource_ConvertionRecipe{
	[System.Serializable]
	public struct Input{
		public int inputID;
		public int amount;
	}

	public Input[] input;
}
