using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopulationController{
	[System.Serializable]
	public struct PopDemand{
		public TradeResource.Types Type;
		public int Amount;
	}

	[System.Serializable]
	public struct PopClass{
		public int Amount;
		public int BaseProductivity;
		[Range(0,100)]
		public int Productivity;
		[Range(0, 200)]
		public int Happiness;
		public PopDemand[] Demands;

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
			foreach(PopDemand d in Demands){
				//Placeholder	
			}
		}
	}


	public PopClass Poor;
	public PopClass Middle;
	public PopClass Rich;
	[HideInInspector]
	public LocalEconomy AttachedEconomy;
}
