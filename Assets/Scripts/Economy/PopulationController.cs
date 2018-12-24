using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopulationController{
	[System.Serializable]
	public struct PopClass{
		public int Amount;
		public int BaseProductivity;
		[Range(0,100)]
		public int Productivity;
		[Range(0, 200)]
		public int Happiness;

		public void Update(){
			UpdateHappiness();
			UpdateProductivity();
		}

		void UpdateProductivity(){
			Productivity = Happiness / (100 * BaseProductivity);
		}

		void UpdateHappiness(){

		}
	}

	
	public PopClass Poor;
	public PopClass Middle;
	public PopClass Rich;
}
