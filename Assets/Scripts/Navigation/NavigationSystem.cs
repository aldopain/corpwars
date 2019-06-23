using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class NavigationSystem : MonoBehaviour {

	List<NavigationNode> allPoints;
	List<NavigationWay> ways;
	public string tagToSearch;

	void Awake () {
		allPoints = new List<NavigationNode>();
		var tmp = GameObject.FindGameObjectsWithTag(tagToSearch);
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<NavigationNode>());
	}

	public NavigationWay GetWay(NavigationNode a, NavigationNode b) {
		var matrix = GetMatrix();
		var way = Dijkstra(matrix, allPoints.IndexOf(a), allPoints.IndexOf(b));
		return Convert(way);
	}

	private float[,] GetMatrix() {
		var matrix = new int[allPoints.Count, allPoints.Count];
		for (var i = 0; i < allPoints.Count; i++) {
			for (var j = 0; j < allPoints.Count; j++) {
				if (j == i) {
					matrix[i, j] = 0;
				} else {
					matrix[i, j] = allPoints[i].SafeDistance(allPoints[j]);
				}
			}
		}
	}

	private int[] Dijkstra(float[,] matrix, int a, int b) {

	}

	private NavigationWay Convert(int[] way) {
		var _way = List<NavigationNode>();
		for(var i = 0; i < way.length; i++) {
			_way.Add(allPoints[way[i]]);
		}
		return new NavigationWay(_way);
	}
}
