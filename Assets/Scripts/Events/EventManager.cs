using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	int DaysToWeeklyEvent;
	int DaysToMonthlyEvent;
	int DaysToAnnualEvent;

	void Start () {
		GetComponent<TimeManager>().OnDay.AddListener(TickDays);	//Event Manager must be placed on GameController
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TickDays(){
		DaysToWeeklyEvent--;
		DaysToMonthlyEvent--;
		DaysToAnnualEvent--;
	}

	void GenerateNextDate_Weekly(){
		
	}

	void GenerateNextDate_Monthly(){
		
	}

	void GenerateNextDate_Annual(){
		
	}
}
