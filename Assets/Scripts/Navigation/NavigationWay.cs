using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationWay : ScriptableObject {
	NavigationNode start;
	NavigationNode destination;
	List<NavigationNode> points;
	float distance = 0;

	public NavigationWay (NavigationNode s, NavigationNode d){
		start = s;
		destination = d;
	}

	public NavigationWay (NavigationNode s, NavigationNode d, float dist){
		start = s;
		destination = d;
		distance = dist;
	}

	public NavigationWay (List<NavigationNode> p) {
		start = p[0];
		destination = p[p.Count - 1];
		SetPoints(p);
	}

	void Distance () {
		distance = Distance (points);
	}

	public float Distance (List<NavigationNode> p) {
		float d = 0;
		for (int i = 1; i < p.Count; i++)
			d += points[i].Distance(points[i - 1]);
		return d;
	}

	bool IsStartInPoint (NavigationNode p) {
		return p.Equals(start);
	}

	bool IsDestinationInPoint (NavigationNode p) {
		return p.Equals(destination);
	}

	void SetPoints (List<NavigationNode> p) {
		points = new List<NavigationNode>();
		points.AddRange(p);
		Distance();
	}
	
}
