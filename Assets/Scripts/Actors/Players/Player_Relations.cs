using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Relations : MonoBehaviour {
	public static int WAR = -1;
	public static int NEUTRAL = 0;
	public static int TRADE_PACT = 2;
	public static int NO_WAR_DECLARE = 3;
	public static int TRADE_NO_WAR = 5;
	Player_Manager pm;
	List<Relations> relations;
	public class Relations {
		public Player_Manager anotherPlayer;
		public int relations;
		public int status;

		public Relations(Player_Manager pm){
			anotherPlayer = pm;
			relations = 100;
			status = NEUTRAL;
		}
	}

	private void Start() {
		pm = GetComponent<Player_Manager>();
		relations = new List<Relations>();
		var pl = GameObject.Find("GameController").GetComponent<Player_List>();
		foreach(var player in pl.allPlayers){
			if(player.name != pm.name)
				relations.Add(new Relations(player));
		}
	}

	private Relations GetByName (string name) {
		foreach (var rel in relations)
			if (rel.anotherPlayer.name.Equals(name)){
				return rel;
			}
		return null;
	}

	public int GetStatus (Player_Manager other){
		var rel = GetByName(other.name);
		if (rel == null)
			return -1;
		return rel.status;
	}

	public int GetRelations (Player_Manager other){
		var rel = GetByName(other.name);
		if (rel == null)
			return -1;
		return rel.relations;
	}

	public bool SetStatus (Player_Manager other, int status){
		var rel = GetByName(other.name);
		if (rel == null || rel.status == status)
			return false;
		if (status == WAR) {
			if (rel.status >= NO_WAR_DECLARE){
				return false;
			} else {
				rel.status = status;
			}
		}
		if (rel.status != WAR && (Mathf.Abs(status) == TRADE_PACT || Mathf.Abs(status) == NO_WAR_DECLARE)){
			rel.status += status;
			return true;
		}
		if (rel.status == WAR) {
			if (status == NEUTRAL){
				rel.status = status;
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	public bool ChangeRelations (Player_Manager other, int a){
		var rel = GetByName(other.name);
		if (rel == null)
			return false;
		rel.relations += a;
		if (a > 200) a = 200;
		if (a < 0) a = 0;
		return true;
	}
}