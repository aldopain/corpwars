using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NavigationRoute {
	List<NavigationStop> stops;
	int i = 0;

	public NavigationRoute(NavigationNode start, NavigationNode end){
		stops = new List<NavigationStop>();
		AddPointsFromWay(start.GetWayTo(end), false, false);
		stops[0].Trade = true;
		stops[stops.Count - 1].Trade = true;
	}

	public NavigationRoute(NavigationNode start, List<NavigationNode> midpoints, NavigationNode end){
		stops = new List<NavigationStop>();
		AddPointsFromWay(start.GetWayTo(midpoints[0]), false, false);
		stops[stops.Count - 1].Trade = true;
		for (int i = 0; i < midpoints.Count - 1; i++){
			AddPointsFromWay(midpoints[i].GetWayTo(midpoints[i + 1]), true, false);
			stops[stops.Count - 1].Trade = true;
		}
		AddPointsFromWay(midpoints[midpoints.Count - 1].GetWayTo(end), true, false);
		stops[stops.Count - 1].Trade = true;
	}

	private void AddPointsFromWay(NavigationWay way, bool excludeFirst, bool excludeLast){
		int startIndex = excludeFirst ? 1 : 0;
		int endIndex = excludeLast ? way.points.Count - 1 : way.points.Count;
		for(int i = startIndex; i < endIndex; i++){
			stops.Add(new NavigationStop(way.points[i]));
		}
	}

	public void LoopRoute(){
		var buf = new List<NavigationStop>(stops);
		buf.RemoveAt(buf.Count - 1);
		buf.Reverse();
		stops.AddRange(buf);
	}

	public NavigationStop Next(){
		i++;
		if (i % stops.Count == 0)
			return null;
		return stops[i % stops.Count];
	}

	public int IndexOf(NavigationStop p){
		return stops.IndexOf(p);
	}
}