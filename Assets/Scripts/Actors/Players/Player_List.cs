using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_List : MonoBehaviour {
	public List<Player_Manager> allPlayers;

	public Player_Manager GetByName(string name){
		foreach (var player in allPlayers)
		{
				if (player.name.Equals(name))
					return player;
		}
		Debug.Log("failed to find player with name = " + name);
		return null;
	}
}