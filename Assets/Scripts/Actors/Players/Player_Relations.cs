using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Relations : MonoBehaviour {
	public static int NEUTRAL = 0;
	public static int WAR = 1;
	public static int TRADE_PACT = 2;
	public static int NO_WAR_DECLARE = 3;
	public static int TRADE_NO_WAR = 4;
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
}