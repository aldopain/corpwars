using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : MonoBehaviour {
	public string name;
	public Player_Relations relations;
	public Player_Money money;
	public Actor_Resources resources;
	public NavigationNode city;
	public List<NavigationNode> tradePosts;

	void Start () {
		resources = city.GetComponent<Actor_Resources>();
		money = GetComponent<Player_Money>();
		relations = GetComponent<Player_Relations>();
	}

	public bool IsEnemy(Player_Manager other) {
		return (relations.GetStatus(other) == Player_Relations.WAR);
	}
}