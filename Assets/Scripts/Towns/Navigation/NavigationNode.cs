using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode : MonoBehaviour {
	public List<NavigationNode> Neighbours;
	List<NavigationWay> ways;
	public Collider[] nearby;

	void Update(){
		nearby = Physics.OverlapSphere(transform.position, 10);
	}

	public float Distance (NavigationNode p) {
		return Vector3.Distance(transform.position, p.transform.position);
	}

}
