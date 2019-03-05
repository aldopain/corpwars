using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class also should consume resources
// _as I think_
public class Town_Population : MonoBehaviour {
	[System.Serializable]
	public struct PopClass{
		public int Amount;
		public int BaseProductivity;
		[Range(0,100)]
		public int Productivity;
		[Range(0, 200)]
		public int Happiness;
		public double[] Demands;

		// resources - Actor_Resources.Amount
		public void Update(double[] resources){
			UpdateHappiness(resources);
			UpdateProductivity();
			UpdateDemands();
		}

		void UpdateProductivity(){
			Productivity = Happiness / (100 * BaseProductivity);
		}

		//Function that would calculate happiness based on available resources
		void UpdateHappiness(double[] resources){
			Happiness = (int) (DemandLevel(resources) * 100);
		}

		double DemandLevel (double[] resources){
			double d = 0;
			if (Demands.Length != resources.Length) Debug.LogWarning("Demands.Length != resources.Length");
			for(int i = 0; i < resources.Length && i < Demands.Length; i++){
				d += (resources[i] > Demands[i] ? 1 : resources[i] / Demands[i]);
			}
			return d / Demands.Length;
		}

		//Function, that would calculate demands
		void UpdateDemands(){
			
		}
	}


	public PopClass Poor;
	public PopClass Middle;
	public PopClass Rich;

	void Start(){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		var glResLength = gl.ResourcesList.Length;

		Poor.Demands = new double[glResLength];
		for(int i = 0; i < Poor.Demands.Length;i++){
			Poor.Demands[i] = Random.Range(0, 100);
		}

		Middle.Demands = new double[glResLength];
		for(int i = 0; i < Middle.Demands.Length;i++){
			Middle.Demands[i] = Random.Range(0, 100);
		}

		Middle.Demands = new double[glResLength];
		for(int i = 0; i < Middle.Demands.Length;i++){
			Middle.Demands[i] = Random.Range(0, 100);
		}
	}

	public double GetDemands(int index){
		return Poor.Demands[index] + Middle.Demands[index] + Rich.Demands[index];
	}
}
