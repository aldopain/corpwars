using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class System_Time : MonoBehaviour {
	public float DayLength;
	public UnityEvent OnDay;
	public UnityEvent OnMonth;
	public UnityEvent OnYear;

	int _days;
	int YEAR_LENGTH = 12; //in months
	int MONTH_LENGTH = 30; //in days
	
	public int Day{
		get{
			return (_days - ((YEAR_LENGTH * MONTH_LENGTH) * Year)) % MONTH_LENGTH;
		}
	}

	public int Month{
		get{
			return (_days - (YEAR_LENGTH * MONTH_LENGTH) * Year) / MONTH_LENGTH;
		}
	}

	public int Year{
		get{
			return _days/(YEAR_LENGTH * MONTH_LENGTH);
		}
	}

	public int Month_DayCount{
		get{
			return MONTH_LENGTH;
		}
	}

	public int Year_DayCount{
		get{
			return YEAR_LENGTH * MONTH_LENGTH;
		}
	}

	void Start(){
		StartCoroutine(Clock());
	}

	public void TickDay(){
		_days++;
		OnDay.Invoke();

		if(Day == 0) {
			OnMonth.Invoke();
		}
		if(Month == 0 && Day == 0 && Year != 0){
			OnYear.Invoke();
		}
	}

	IEnumerator Clock(){
		while(true){
			yield return new WaitForSeconds(DayLength);
			TickDay();
		}
	}

    public override string ToString()
    {
        return Day + "/" + Month + "/" + Year;
    }
}