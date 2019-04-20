using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Religion : MonoBehaviour {
	public double[] Demands;
	private Town_Population pops;
	public int max;

	void UpdateMinHappiness(double[] resources) {
		var min = (int) (pops.Poor.DemandLevel(resources, Demands) * max);
		pops.Poor.MinHappiness = min;
		pops.Middle.MinHappiness = min;
		pops.Rich.MinHappiness = min;
	}

	void UpdateMax(int holyPointsCount, int allHolyPointsCount, bool isWarWithDev){
		max = (int) (((double) holyPointsCount / allHolyPointsCount) * 20 * (isWarWithDev ? 2 : 1));
	}
}
