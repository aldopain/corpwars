using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Population : MonoBehaviour {
	[System.Serializable]
	public struct PopClass{
		public int Amount;
		public int BaseProductivity;
		[Range(0,100)]
		public int Productivity;
		[Range(0, 200)]
		public int Happiness;
		public int[] Demands;

		public void Update(){
			UpdateHappiness();
			UpdateProductivity();
			UpdateDemands();
		}

		void UpdateProductivity(){
			Productivity = Happiness / (100 * BaseProductivity);
		}

		//Function that would calculate happiness based on available resources
		void UpdateHappiness(){
			//Placeholder
		}

		//Function, that would calculate demands
		void UpdateDemands(){

		}
	}


	public PopClass Poor;
	public PopClass Middle;
	public PopClass Rich;
}
