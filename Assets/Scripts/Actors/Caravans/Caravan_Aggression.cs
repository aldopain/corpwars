using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Aggro : MonoBehaviour {

	Collider coll;

	public int aggro;
	public static int SNEAK = 0;
	public static int NEUTRAL = 1;
	public static int AGGRESSIVE = 2;
	public static int KILLER = 3;

	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<CapsuleCollider>();
		aggro = NEUTRAL;
	}

	public void SetAggro(int a){
		if (a == SNEAK || a == NEUTRAL || a == AGGRESSIVE || a == KILLER)
			aggro = a;
	}

	// true here should be replaced with check (enemy/not)
	void ChooseBehaviour(GameObject go){
		if(aggro == KILLER || (aggro == AGGRESSIVE && true)){
			Caravan_Fight.Start(gameObject, go);
		} else if (aggro == SNEAK && true) {
			// RUN, FOREST, RUN
		}
		// if neutral then dont give a fuck
	}
	
	void OnTriggerEnter(Collider other) {
			if (other.gameObject.tag == "Caravan"){
				
			}
    }

}
