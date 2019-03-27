using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Production : MonoBehaviour {
	public List<Resource_Factory> factories;

	Economy_Local economy;
	Resource_GlobalList gl;
	public float[] resourceInputEfficiency;
	public float[] resourceOutputEfficiency;

	void Awake() {
		economy = GetComponent<Economy_Local>();
	}

	void Start() {
		GameObject.Find("GameController").GetComponent<System_Time>().OnDay.AddListener(ProduceResources);
		gl = GameObject.Find("GameController").GetComponent<Resource_GlobalList>();
	}

	public void ProduceResources() {
		foreach (var factory in factories) {
			factory.TryProduce(economy, resourceInputEfficiency, resourceOutputEfficiency);
		}
	}
}