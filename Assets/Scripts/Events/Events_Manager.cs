using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using UnityEditor;

public class Events_Manager : MonoBehaviour {
	public List<Events_Base> MonthlyEvents;
	[Range(0,1)]
	public float DatePickerError;

	private System_Time time;
	private int DaysToMonthlyEvent;
	public Events_Logic Logic;


	void Awake(){
		time = GetComponent<System_Time>();
		Logic = GetComponent<Events_Logic>();
	}

	// Use this for initialization
	void Start () {
		time.OnDay.AddListener(TickDays);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) ParseActions(MonthlyEvents[0].Buttons[0].Action);
	}

	public void TickDays(){
		DaysToMonthlyEvent--;

		if(DaysToMonthlyEvent <= 0){
			InvokeEvent(MonthlyEvents);
			NextDate_Monthly();
		}
	}
	
	public void NextDate_Monthly(){
		DaysToMonthlyEvent = time.Month_DayCount + Random.Range((int)(-time.Month_DayCount * DatePickerError), (int)(time.Month_DayCount * DatePickerError));
	}

	public void InvokeEvent(List<Events_Base> events){
		//Placeholder
		//int index = Random.Range(0, events.Count);
		
	}

	public void ParseActions(string actions){
		string[] actionList = actions.Split('\n');
		foreach(string a in actionList){
			parseAction(a);
		}
	}

	private void parseAction(string action){
		string[] parameters = getParameters(action);
		MethodInfo method;
		if(parameters.Length == 1){
			method = Logic.GetType().GetMethod(parameters[0]);
			method.Invoke(Logic, null);
		}else{
			object[] args = new object[] { parameters.Skip(1).Take(parameters.Length-1).ToArray()};
			method = Logic.GetType().GetMethod(parameters[0]);
			method.Invoke(Logic, args);
		}		
	}

	private string[] getParameters(string action){
		int parametersIndex = action.IndexOf(':');
		if(parametersIndex == -1){
			return new string[]{action};
		}else{
			string functionName = action.Substring(0, parametersIndex);
			action = action.Substring(parametersIndex+1);
			List<string> parameters = new List<string>();
			parameters.Add(functionName);
			parameters.AddRange(action.Split(','));
			return parameters.ToArray();
		}
	}
}
