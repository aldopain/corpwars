using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Manager : Player_Trade {
	public NavigationNode city;
	public List<NavigationNode> tradePosts;
	public Player_Relations relations;

	void Start () {
		resources = city.GetComponent<Actor_Resources>();
		money = GetComponent<Player_Money>();
		relations = GetComponent<Player_Relations>();
		events = gm.GetComponent<System_EventPool>();
	}

	public bool IsEnemy(Player_Manager other) {
		return (relations.GetStatus(other) == Player_Relations.WAR);
	}

	public void TransferTradePost(NavigationNode post, Player_Manager other) {
		if (tradePosts.Contains(post)) {
			tradePosts.Remove(post);
			other.tradePosts.Add(post);
			post.resources.Owner = other;
		}
	}
}