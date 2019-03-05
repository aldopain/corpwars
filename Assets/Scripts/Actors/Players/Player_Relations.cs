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
	public struct Relations {
		Player_Manager anotherPlayer;
		int relations;
		int status;

		public Relations(Player_Manager pm){
			anotherPlayer = pm;
			relations = 100;
			status = NEUTRAL;
		}
	}

	private void Start() {
		pm = GetComponent<Player_Manager>();
		var pl = GameObject.Find("GameManager").GetComponent<Player_List>();
		foreach(var player in pl.allPlayers){
			if(player.name != pm.name)
				relations.Add(new Relations(player));
		}
	}
}