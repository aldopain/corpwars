using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class NavigationSystem : MonoBehaviour {

	List<NavigationNode> allPoints;
	List<NavigationWay> ways;
	public string tag;

	void Awake () {
		allPoints = new List<NavigationNode>();
		var tmp = GameObject.FindGameObjectsWithTag(tag);
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<NavigationNode>());
		foreach(var _current in allPoints){
			_current.ways = Dijkstra(_current);
		}
	}

	 void BakeWays(){
		allPoints = new List<NavigationNode>();
		var tmp = GameObject.FindGameObjectsWithTag(tag);
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<NavigationNode>());
		foreach(var _current in allPoints){
			_current.ways = Dijkstra(_current);
		}
	}

	List<NavigationWay> Dijkstra(NavigationNode start)
	{
		double[] dist = new double[allPoints.Count];
		bool[] passed = new bool[allPoints.Count];
		int current = allPoints.IndexOf(start);

		for (int i = 0; i < allPoints.Count; i++)
		{
			dist[i] = -1;
			passed[i] = false;
		}
		dist[current] = 0;

		for (int i = 0; i < allPoints.Count; i++)
		{
			current = MinDist(dist, passed);

			for (int j = 0; j < allPoints[current].Neighbours.Count; j++)
			{
				List<int> points = new List<int>();

				for (int k = 0; k < allPoints[current].Neighbours.Count; k++)
				{
					int neighboor = allPoints.IndexOf(allPoints[current].Neighbours[k]);
					if (allPoints[current].Distance(allPoints[neighboor]) > 0 && !passed[neighboor])
					{
						points.Add(neighboor);
					}
				}

				points = Sort(current, points);

				for (int k = 0; k < points.Count; k++)
				{
					double d = allPoints[current].Distance(allPoints[points[k]]);
					if (dist[points[k]] < 0 || d + dist[current] < dist[points[k]])
					{
						dist[points[k]] = d + dist[current];
					}
				}
			}

				passed[current] = true;
		}

		List<List<int>> result = new List<List<int>>();
		for (int i = 0; i < allPoints.Count; i++)
		{
			double weight;

			result.Add(new List<int>());
			result[i].Add(i);
			weight = dist[i];

			while (weight>0)
			{
				result[i].Add(FindNext(result[i], dist));

				weight -= allPoints[result[i].Last()].Distance(allPoints[result[i][result[i].Count - 2]]);
			}
			result[i].Reverse();

		}

		return Convert(result);
	}

	List<NavigationWay> Convert (List<List<int>> p) {
		var tmp = new List<List<NavigationNode>>();
		var res = new List<NavigationWay>();
		foreach (var currentList in p) {
			var cp = new List<NavigationNode>();
			tmp.Add(cp);
			foreach (var currentIndex in currentList) {
				cp.Add(allPoints[currentIndex]);
			}
		}
		foreach (var w in tmp) {
			res.Add(new NavigationWay(w));
		}
		return res;
	}

	int MinDist(double[] dist, bool[] passed)
	{
		int result = -1;

		for (int i = 0; i < dist.Length; i++)
		{
			if (!(dist[i] < 0) && !passed[i] && (result < 0 ||  dist[i] < dist[result]))
				result = i;
		}

		return result;
	}

	List<int> Sort(int current, List<int> points)
	{
		List<int> sorted = new List<int>();

		for (int i = 0; i < points.Count; i++)
		{
			int numb = -1;
			for (int j = 0; j < points.Count; j++)
			{
				if (!sorted.Contains(points[j]) && (numb < 0 || allPoints[current].Distance(allPoints[points[j]]) < allPoints[current].Distance(allPoints[points[numb]])))
				{
					numb = j;
				}
			}
			sorted.Add(points[numb]);
		}

		return sorted;
	}

	int FindNext(List<int> points, double[] dist)
	{
		for(int i=0; i<dist.Length; i++)
		{
			double d = allPoints[i].Distance(allPoints[points.Last()]);
			if (!points.Contains(i) && d > 0)
			{
				if (dist[i] + d == dist[points.Last()])
					return i;
			}
		}

		return -1;
	}
}
