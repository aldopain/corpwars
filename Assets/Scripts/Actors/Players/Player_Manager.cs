using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour {
	public string name;
	Player_Relations relations;
	Player_Money money;

	void Start () {
		money = GetComponent<Player_Money>();
		relations = GetComponent<Player_Relations>();
	}

	public bool IsEnemy(Player_Manager other) {
		return (relations.GetStatus(other) == Player_Relations.WAR);
	}
}
