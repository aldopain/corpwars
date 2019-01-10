using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Global : MonoBehaviour {
	public double[] producedDay;				
	public double[] boughtDay;				
	public double[] soldDay;

	public double[] producedMonth;				
	public double[] boughtMonth;				
	public double[] soldMonth;

	public double[] producedLifetime;				
	public double[] boughtLifetime;				
	public double[] soldLifetime;

	public int[] pricing;

	void Awake(){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		gl.GetComponent<System_Time>().OnDay.AddListener(UpdateDay);
		gl.GetComponent<System_Time>().OnMonth.AddListener(Clear_Month);

		producedDay = new double[gl.ResourcesList.Length];
		boughtDay = new double[gl.ResourcesList.Length];
		soldDay = new double[gl.ResourcesList.Length];

		producedMonth = new double[gl.ResourcesList.Length];
		boughtMonth = new double[gl.ResourcesList.Length];
		soldMonth = new double[gl.ResourcesList.Length];

		producedLifetime = new double[gl.ResourcesList.Length];
		boughtLifetime = new double[gl.ResourcesList.Length];
		soldLifetime = new double[gl.ResourcesList.Length];

		pricing = new int[gl.ResourcesList.Length];
		for(int i = 0; i < pricing.Length;i++){
			pricing[i] = Random.Range(0, 100);
		}
	}

	void Start(){

	}

	// Update is called once per day
	void UpdateDay(){
		UpdatePricing();
		Clear_Day();
	}

	public void UpdatePricing(){
		for(int i = 0; i < pricing.Length; i++){
			
		}
	}

	public void Clear_Day(){
		for(int i = 0; i < producedDay.Length; i++){
			producedDay[i] = 0;
			boughtDay[i] = 0;
			soldDay[i] = 0;
		}
	}

	public void Clear_Month(){
		for(int i = 0; i < producedDay.Length; i++){
			producedMonth[i] = 0;
			boughtMonth[i] = 0;
			soldMonth[i] = 0;
		}
	}
}
