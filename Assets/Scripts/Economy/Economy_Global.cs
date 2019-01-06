using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy_Global : MonoBehaviour {
	public int[] producedDay;				
	public int[] boughtDay;				
	public int[] soldDay;

	public int[] producedMonth;				
	public int[] boughtMonth;				
	public int[] soldMonth;

	public int[] producedLifetime;				
	public int[] boughtLifetime;				
	public int[] soldLifetime;

	public int[] pricing;

	void Awake(){
		Resource_GlobalList gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
		gl.GetComponent<System_Time>().OnDay.AddListener(UpdateDay);
		gl.GetComponent<System_Time>().OnMonth.AddListener(Clear_Month);

		producedDay = new int[gl.ResourcesList.Length];
		boughtDay = new int[gl.ResourcesList.Length];
		soldDay = new int[gl.ResourcesList.Length];

		producedMonth = new int[gl.ResourcesList.Length];
		boughtMonth = new int[gl.ResourcesList.Length];
		soldMonth = new int[gl.ResourcesList.Length];

		producedMonth = new int[gl.ResourcesList.Length];
		boughtMonth = new int[gl.ResourcesList.Length];
		soldMonth = new int[gl.ResourcesList.Length];
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
