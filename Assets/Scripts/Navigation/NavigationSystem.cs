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
		float[] dist = new float[ways.GetLength(0)];
		int[] prev = new int[ways.GetLength(0)];
		bool[] visited = new bool[ways.GetLength(0)];
		List<int> result = new List<int>();

		for (int i = 0; i < dist.Length; i++)
		{
			dist[i] = Single.PositiveInfinity;
			prev[i] = -1;
			visited[i] = false;
		}
		dist[start] = 0;

		while (true)
		{
			float max = Single.PositiveInfinity;
			int current = -1;

			for (int i = 0; i < visited.Length; i++)    //Ищем непосещенную вершину с кратчайшим путем
			{
				if (!visited[i] && dist[i] < max) 
				{
					max = dist[i];
					current = i;
				}
			}

			if(current < 0)
			{
				break;
			}

			visited[current] = true;

			for (int i = 0; i < dist.Length; i++)   //Обновляем все пути из неё
			{
				if (i != current)
				{
					float newDist = dist[current] + ways[current, i];
					if (newDist < dist[i])
					{
						dist[i] = newDist;
						prev[i] = current;
					}
				}

			}
		}
		
		for (int i = end; i >= 0; i = prev[i])
		{
			result.Add(i);
		}
		result.Reverse();

		return result.ToArray();
	}

	private NavigationWay Convert(int[] way) {
		var _way = List<NavigationNode>();
		for(var i = 0; i < way.length; i++) {
			_way.Add(allPoints[way[i]]);
		}
		return new NavigationWay(_way);
	}
}
