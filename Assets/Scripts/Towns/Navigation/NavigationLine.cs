using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationLine : MonoBehaviour {
	public NavigationNode[] AdjacentNodes;
	public LineRenderer LinePrefab;

	void Start () {
		if(AdjacentNodes.Length > 2){
			System.Array.Resize<NavigationNode>(ref AdjacentNodes, 2);
		}

		SetNeighbours();
		DrawLine();
	}
	
	void SetNeighbours(){
		AdjacentNodes[0].Neighbours.Add(AdjacentNodes[1]);
		AdjacentNodes[1].Neighbours.Add(AdjacentNodes[0]);
	}

	void DrawLine(){
		LineRenderer line = Instantiate(LinePrefab.gameObject, Vector3.zero, new Quaternion(0,0,0,0)).GetComponent<LineRenderer>();

		line.SetPosition(0, AdjacentNodes[0].transform.position);
		line.SetPosition(1, AdjacentNodes[1].transform.position);

		line.name = "PathLine(" + AdjacentNodes[0].name + " - " + AdjacentNodes[1].name + ")";
	}
}
