using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavigationLine : MonoBehaviour {
	public NavigationNode[] AdjacentNodes;
	private LineRenderer lineRenderer;

	void Start(){
		if(lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
	}

	void Update(){
		UpdateLine();
	}

	void UpdateLine(){
		lineRenderer.SetPosition(0, AdjacentNodes[0].transform.position);
		lineRenderer.SetPosition(1, AdjacentNodes[1].transform.position);	
	}

	public void Init(NavigationNode origin, NavigationNode destination){
		AdjacentNodes = new NavigationNode[2];
		AdjacentNodes[0] = origin;
		AdjacentNodes[1] = destination;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.sharedMaterial = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));

		lineRenderer.startWidth = .1f;
		lineRenderer.endWidth = .1f;
		lineRenderer.startColor = origin.GetComponent<MeshRenderer>().sharedMaterial.color;
		lineRenderer.endColor = destination.GetComponent<MeshRenderer>().sharedMaterial.color;

		name = "PathLine(" + AdjacentNodes[0].name + " - " + AdjacentNodes[1].name + ")";

		UpdateLine();
	}

	void OnDestroy(){
		AdjacentNodes[0].Neighbours.Remove(AdjacentNodes[1]);
		AdjacentNodes[1].Neighbours.Remove(AdjacentNodes[0]);
	}
}
