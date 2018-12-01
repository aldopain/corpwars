using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationSystem : MonoBehaviour {

	List<NavigationNode> allPoints;
	List<NavigationWay> ways;

	// Use this for initialization
	void Start () {
		allPoints = new List<NavigationNode>();
		var tmp = GameObject.FindGameObjectsWithTag("TradePost");
		foreach (var current in tmp)
			allPoints.Add(current.GetComponent<NavigationNode>());
	}

	void Dijkstra (NavigationNode s) {
		var tmpWays = new List<NavigationWay>();
		foreach ( var current in allPoints) {
			if (!current.Equals(s))
				if (s.Neighbours.Contains(current)) {
					var tmp = new List<NavigationNode>();
				} else
					tmpWays.Add (new NavigationWay(s, current));

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
