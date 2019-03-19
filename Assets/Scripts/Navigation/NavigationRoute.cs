using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NavigationRoute {
	List<NavigationNode> stops;
	int i = 0;

	public NavigationRoute(NavigationNode start, NavigationNode end){
		stops = new List<NavigationNode>();
		AddPointsFromWay(start.GetWayTo(end), false, false);
	}

	public NavigationRoute(NavigationNode start, List<NavigationNode> midpoints, NavigationNode end){
		stops = new List<NavigationNode>();
		AddPointsFromWay(start.GetWayTo(midpoints[0]), false, false);
		for (int i = 0; i < midpoints.Count - 1; i++){
			AddPointsFromWay(midpoints[i].GetWayTo(midpoints[i + 1]), true, false);
		}
		AddPointsFromWay(midpoints[midpoints.Count - 1].GetWayTo(end), true, false);
	}

	private void AddPointsFromWay(NavigationWay way, bool excludeFirst, bool excludeLast){
		int startIndex = excludeFirst ? 1 : 0;
		int endIndex = excludeLast ? way.points.Count - 1 : way.points.Count;
		for(int i = startIndex; i < endIndex; i++){
			stops.Add(way.points[i]);
		}
	}

	public void LoopRoute(){
		var buf = new List<NavigationNode>(stops);
		buf.RemoveAt(buf.Count - 1);
		buf.Reverse();
		stops.AddRange(buf);
	}

	public NavigationNode Next(){
		i++;
		if (i == stops.Count) {
			i = 0;
			return null;
		}
		return stops[i];
	}

	public int IndexOf(NavigationNode p){
		return stops.IndexOf(p);
	}
}