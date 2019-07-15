﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation_PathFinder : MonoBehaviour
{
    List<Navigation_Node> allPoints;
	public string tagToSearch;
    public List<Navigation_Way> ways;

	void Awake () {
		allPoints = new List<Navigation_Node>();
		var tmp = GameObject.FindGameObjectsWithTag(tagToSearch);
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<Navigation_Node>());
	}

    void Start () {
        Test();
    }

    void Test () {
        ways = new List<Navigation_Way>();
        for(var i = 0; i < allPoints.Count; i++)
            for(var j = 0; j < allPoints.Count; j++) {
                if(i != j)
                    print("From " + allPoints[i].name + " to " + allPoints[j].name + "\n" + GetWay(allPoints[i], allPoints[j]).ToString());
            }
    }

	public Navigation_Way GetWay(Navigation_Node a, Navigation_Node b) {
		var matrix = GetMatrix();
		var way = Dijkstra(matrix, allPoints.IndexOf(a), allPoints.IndexOf(b));
		return Convert(way);
	}

	private float[,] GetMatrix() {
		var matrix = new float[allPoints.Count, allPoints.Count];
		for (var i = 0; i < allPoints.Count; i++) {
			for (var j = 0; j < allPoints.Count; j++) {
				if (j == i) {
					matrix[i, j] = 0;
				} else {
					matrix[i, j] = allPoints[i].SafeDistance(allPoints[j]);
				}
			}
		}
		return matrix;
	}

	private int[] Dijkstra(float[,] ways, int start, int end) {
		float[] dist = new float[ways.GetLength(0)];
		int[] prev = new int[ways.GetLength(0)];
		bool[] visited = new bool[ways.GetLength(0)];
		List<int> result = new List<int>();

		for (int i = 0; i < dist.Length; i++)
		{
			dist[i] = Mathf.Infinity;
			prev[i] = -1;
			visited[i] = false;
		}
		dist[start] = 0;

		while (true)
		{
			float max = Mathf.Infinity;
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

	private Navigation_Way Convert(int[] way) {
		var _way = new List<Navigation_Node>();
		for(var i = 0; i < way.Length; i++) {
			_way.Add(allPoints[way[i]]);
		}
		return new Navigation_Way(_way);
	}
}