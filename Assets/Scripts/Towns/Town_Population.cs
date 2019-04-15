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
		public double Birthrate;
		public double[] HappinessDemands;
		public double[] BirthrateDemands;

		// resources - Actor_Resources.Amount
		public void Update(double[] resources){
			UpdateHappiness(resources);
			UpdateProductivity();
			UpdateHappinessDemands();
		}

		void UpdateProductivity(){
			Productivity = Happiness / (100 * BaseProductivity);
		}

		//Function that would calculate happiness based on available resources
		void UpdateHappiness(double[] resources){
			Happiness = (int) (DemandLevel(resources, HappinessDemands) * 100);
		}

		void UpdateAmount(){
			Amount += (int) (Amount * Birthrate);
		}

		void UpdateBirthrate(double[] resources){
			Birthrate = 0.4 * DemandLevel(resources, HappinessDemands) - 0.2;
		}

		double DemandLevel (double[] resources, double[] demands){
			double d = 0;
			if (demands.Length != resources.Length) Debug.LogWarning("Demands.Length != resources.Length");
			for(int i = 0; i < resources.Length && i < demands.Length; i++){
				d += (resources[i] > demands[i] ? 1 : resources[i] / demands[i]);
			}
			return d / demands.Length;
		}

		//Function, that would calculate HappinessDemands
		void UpdateHappinessDemands(){
			
		}
	}


	public PopClass Poor;
	public PopClass Middle;
	public PopClass Rich;

	void Start(){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		var glResLength = gl.ResourcesList.Length;

		Poor.HappinessDemands = new double[glResLength];
		Poor.BirthrateDemands = new double[glResLength];

		Middle.HappinessDemands = new double[glResLength];
		Middle.BirthrateDemands = new double[glResLength];

		Rich.HappinessDemands = new double[glResLength];
		Rich.BirthrateDemands = new double[glResLength];
	
		for (int i = 0; i < glResLength; i++) {
			Poor.HappinessDemands[i] = Random.Range(0, 100);
			Poor.BirthrateDemands[i] = Random.Range(0, 100);
			
			Middle.HappinessDemands[i] = Random.Range(0, 100);
			Middle.BirthrateDemands[i] = Random.Range(0, 100);
			
			Rich.HappinessDemands[i] = Random.Range(0, 100);
			Rich.BirthrateDemands[i] = Random.Range(0, 100);
		}
	}

	public double GetHappinessDemands(int index){
		return Poor.HappinessDemands[index] + Middle.HappinessDemands[index] + Rich.HappinessDemands[index];
	}
}
