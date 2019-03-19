using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Aggro : MonoBehaviour {

	Player_Manager Owner;
	public static int SNEAK = 0;
	public static int NEUTRAL = 1;
	public static int AGGRESSIVE = 2;
	public static int KILLER = 3;
	public int aggro = NEUTRAL;
	public bool IsInFight = false;

	// Use this for initialization
	void Start () {
		Owner = gameObject.GetComponent<Actor_Resources>().Owner;
	}

	public void SetAggro(int a){
		if (a == SNEAK || a == NEUTRAL || a == AGGRESSIVE || a == KILLER)
			aggro = a;
	}

	List<Collider> Filter(Collider[] colliders){
		var retVal = new List<Collider>();
		foreach(var coll in colliders) {
			if (coll.gameObject.tag == "Caravan" && !coll.gameObject.name.Equals(gameObject.name)){
				var otherOwner = coll.GetComponent<Actor_Resources>().Owner;
				if (aggro == KILLER || (aggro == AGGRESSIVE && Owner.IsEnemy(otherOwner)) && !coll.GetComponent<Caravan_Aggro>().IsInFight)
					retVal.Add(coll);
			}
		}
		return retVal;
	}

	void Update(){
		if (!IsInFight && aggro != NEUTRAL) {
			var nearCaravans = Filter(Physics.OverlapSphere(transform.position, 1f));
			if (nearCaravans.Count > 0) {
				print("FIGHT");
				StartCoroutine(Caravan_Fight.Start(gameObject, nearCaravans[0].gameObject));
			}
		}
	}
}
