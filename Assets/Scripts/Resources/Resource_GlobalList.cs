using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_GlobalList : MonoBehaviour {
	Resource[] ResourcesList;

	public Resource GetById(int id){
		return ResourcesList[id];
	}
}
