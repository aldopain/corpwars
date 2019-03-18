using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationStop {
	public bool Trade = false;
	public NavigationNode Point;

	public NavigationStop(NavigationNode p){
		Point = p;
	}

	public NavigationStop(NavigationNode p, bool trade){
		Point = p;
		Trade = trade;
	}
}
