using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode : MonoBehaviour {
	public List<NavigationNode> Neighbours;
	public Collider[] nearby;

	void Update(){
		nearby = Physics.OverlapSphere(transform.position, 10);
	}

}
